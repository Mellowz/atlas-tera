using GameServer.Config;
using GameServer.Utility;
using NLog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace GameServer.Network
{
    /// <summary>
    /// Network Listener Service
    /// </summary>
    public class NetListener : IDisposable
    {
        /// <summary>
        /// Logger for this class
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Tcp Listener Instance
        /// </summary>
        protected TcpListener m_Listener;

        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<string, long> ConnectionsTime = new Dictionary<string, long>();

        /// <summary>
        /// Run Network Listener
        /// </summary>
        public void Run()
        {
            try
            {
                var ipaddr = (NetworkConfig.Instance.EnableIPv6) ? IPAddress.IPv6Any : IPAddress.Any;
                m_Listener = new TcpListener(ipaddr, NetworkConfig.Instance.Port);
                m_Listener.Start();
                Logger.Info("Network listening clients at {0}:{1}...", ((IPEndPoint)m_Listener.LocalEndpoint).Address, NetworkConfig.Instance.Port);
                m_Listener.BeginAcceptTcpClient(new AsyncCallback(BeginAcceptTcpClient), m_Listener);
            }
            catch(Exception e)
            {
                Logger.Error(e);
            }
        }

        /// <summary>
        /// Network Accepting Client
        /// </summary>
        /// <param name="ar"></param>
        private void BeginAcceptTcpClient(IAsyncResult ar)
        {
            var tcpClient = m_Listener.EndAcceptTcpClient(ar);
            string ip = Regex.Match(tcpClient.Client.RemoteEndPoint.ToString(), "([0-9]+).([0-9]+).([0-9]+).([0-9]+)").Value;

            if (ip == "0.0.0.0") // for web ping server status
                return;

            Logger.Info("Client Connected!");

            if (ConnectionsTime.ContainsKey(ip))
            {
                if (Funcs.GetCurrentMilliseconds() - ConnectionsTime[ip] < 2000)
                {
                    /*Process.Start("cmd",
                                  "/c netsh advfirewall firewall add rule name=\"AutoBAN (" + ip +
                                  ")\" protocol=TCP dir=in remoteip=" + ip + " action=block");
                    ConnectionsTime.Remove(ip);*/
                    Logger.Info("TcpServer: FloodAttack prevent! Ip " + ip + " added to firewall");
                    return;
                }
                ConnectionsTime[ip] = Funcs.GetCurrentMilliseconds();
            }
            else
                ConnectionsTime.Add(ip, Funcs.GetCurrentMilliseconds());

            var con = new Connection(tcpClient);
            con.Disconnected += Con_Disconnected;
        }

        private void Con_Disconnected(Connection connection)
        {
            Logger.Info("Connection Disconnected.");
            connection.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            
        }
    }
}
