using GameServer.Model.Account;
using GameServer.Model.Player;
using GameServer.Network;
using GameServer.Network.Send;
using GameServer.Utility;
using GameServer.Utility.Helpers;
using NHibernate;
using NHibernate.Criterion;
using NLog;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameServer.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerService
    {
        /// <summary>
        /// Logger for this class
        /// </summary>
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Nhibernate Database Connection Instance
        /// </summary>
        static ISessionFactory _SessionFactory;

        /// <summary>
        /// 
        /// </summary>
        static PlayerService()
        {
            _SessionFactory = NHibernateHelper.CreateSessionFactory();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static List<Player> LoadPlayerList(Account account)
        {
            using (ISession session = _SessionFactory.OpenSession())
            {
                var list = (List<Player>)session
                            .QueryOver<Player>()
                            .Where(x => x.AccountId == account.Id)
                            .List();
                Logger.Trace($"Load Character ({list.Count}) from acc.ID {account.Id}");
                return list;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public static void CanCreatePlayer(Connection connection)
        {
            new S_CAN_CREATE_USER((connection.Players.Count < connection.Account.MaxPlayers))
                .Send(connection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="evaluateStr"></param>
        public static void CheckEvaluateStr(Connection connection, string evaluateStr)
        {
            var result = Regex.IsMatch(evaluateStr, @"^[a-zA-Z0-9]*$");
            new S_STR_EVALUATE_LIST(result, evaluateStr)
                .Send(connection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="evaluateName"></param>
        public static void CheckPlayerName(Connection connection, string name)
        {
            using (ISession session = _SessionFactory.OpenSession())
            {
                var player = session
                    .CreateCriteria(typeof(Player))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Player>();

                new S_CHECK_USERNAME(!player.IsExists())
                    .Send(connection);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="Player"></param>
        public static void CreatePlayer(Connection connection, Player Player)
        {
            Player.AccountId = connection.Account.Id;
            Player.MapId = 13;
            Player.X = 93492.0F;
            Player.Y = -88216.0F;
            Player.Z = -4523.0F;
            Player.Heading = unchecked((short)0x8000);

            using (var session = _SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.Save(Player);
                tx.Commit();
                new S_CREATE_USER(true).Send(connection);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public static void ShowPlayerList(Connection connection)
        {
            new S_GET_USER_LIST()
                .Send(connection);
            new S_LOAD_CLIENT_ACCOUNT_SETTING()
                .Send(connection);
        }
    }
}
