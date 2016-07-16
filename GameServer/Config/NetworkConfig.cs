using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class NetworkConfig : Config
    {
        /// <summary>
        /// Config Variable
        /// </summary>
        public int Port { get { return this.GetInt("Port", 11101); } set { this.Set("Port", value); } }
        public bool EnableIPv6 { get { return this.GetBoolean("EnableIPv6", false); } set { this.Set("EnableIPv6", value); } }

        /// <summary>
        /// 
        /// </summary>
        private static readonly NetworkConfig _instance = new NetworkConfig();

        /// <summary>
        /// 
        /// </summary>
        public static NetworkConfig Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private NetworkConfig() : base("Network")
        {
        }
    }
}
