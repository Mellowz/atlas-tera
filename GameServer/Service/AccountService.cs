using GameServer.Model.Account;
using GameServer.Model.Mappings.Accounts;
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
            _SessionFactory = NHibernateHelper.CreateMssqlSessionFactory();
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
                var accountDto = session
                    .CreateCriteria(typeof(AccountDto))
                    .Add(Restrictions.Eq("Name", AccountName))
                    .Add(Restrictions.Eq("Token", Token))
                    .UniqueResult<AccountDto>();

                if(accountDto.IsExists())
                {
                    connection.Account = new Account(accountDto);
                    connection.Players = PlayerService.LoadPlayerList(connection.Account);

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
