using System;
using GameServer.Network;
using NHibernate;
using NLog;
using GameServer.Network.Send;

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
        /// <param name="connection"></param>
        public static void ShowPlayerList(Connection connection)
        {
            new S_GET_USER_LIST().Send(connection);
        }
    }
}
