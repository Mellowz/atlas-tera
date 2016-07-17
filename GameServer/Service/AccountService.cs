using GameServer.Model.Account;
using GameServer.Network;
using GameServer.Network.Send;
using GameServer.Utility;
using GameServer.Utility.Helpers;
using NHibernate;
using NHibernate.Criterion;
using NLog;

namespace GameServer.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountService
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
        static AccountService()
        {
            _SessionFactory = NHibernateHelper.CreateSessionFactory();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="AccountName"></param>
        /// <param name="Token"></param>
        public static void TryAuthorize(Connection connection, string AccountName, string Token)
        {
            using (ISession session = _SessionFactory.OpenSession())
            {
                var account = session
                    .CreateCriteria(typeof(Account))
                    .Add(Restrictions.Eq("Name", AccountName))
                    .Add(Restrictions.Eq("Token", Token))
                    .UniqueResult<Account>();

                if(account.IsExists())
                {
                    connection.Account = account;
                    // todo preload players store in account
                    // connection.Account.Players = PlayerService.LoadPlayerByAccountId(account.Id);

                    new S_LOADING_SCREEN_CONTROL_INFO().Send(connection);
                    new S_REMAIN_PLAY_TIME().Send(connection);
                    new S_LOGIN_ARBITER().Send(connection);
                    new S_LOGIN_ACCOUNT_INFO().Send(connection);
                }
                else
                {
                    Logger.Warn($"Account {AccountName} is not exists");
                    connection.Close();
                }
            }
        }
    }
}
