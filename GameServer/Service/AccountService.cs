using GameServer.Model.Account;
using GameServer.Network;
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
                    .UniqueResult<Account>();

                if(account != null)
                {
                    Logger.Debug($"Name: {account.Name}");
                    Logger.Debug($"Password: {account.Password}");
                    Logger.Debug($"AccountLevel: {account.AccountLevel}");
                    Logger.Debug($"Token: {account.Token}");
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
