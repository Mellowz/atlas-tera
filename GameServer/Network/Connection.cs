using GameServer.Model.Account;
using GameServer.Network.Crypt;
using GameServer.Utility;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GameServer.Network
{
    /// <summary>
    /// 
    /// </summary>
    public class Connection : IDisposable
    {
        /// <summary>
        /// Logger for this class
        /// </summary>
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// List of Connection
        /// </summary>
        public static List<Connection> Connections = new List<Connection>();

        /// <summary>
        /// 
        /// </summary>
        protected EndPoint m_Address;
        /// <summary>
        /// 
        /// </summary>
        protected TcpClient m_Client;
        /// <summary>
        /// 
        /// </summary>
        protected NetworkStream m_Stream;
        /// <summary>
        /// 
        /// </summary>
        protected byte[] m_Buffer;

        /// <summary>
        /// 
        /// </summary>
        protected int State;

        /// <summary>
        /// 
        /// </summary>
        public Account Account;

        /// <summary>
        /// Crypto session
        /// </summary>
        public Session Session;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcpClient"></param>
        public Connection(TcpClient tcpClient)
        {
            m_Client = tcpClient;
            m_Stream = tcpClient.GetStream();
            m_Address = tcpClient.Client.RemoteEndPoint;

            Session = new Session();

            Connections.Add(this);

            new Thread(new ThreadStart(InitKey)).Start();
            new Thread(new ThreadStart(ReadMessage)).Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitKey()
        {
            Send(new byte[] { 1, 0, 0, 0 });
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReadMessage()
        {
            try
            {
                if (m_Stream == null || !m_Stream.CanRead)
                    return;

                m_Buffer = new byte[4096];
                m_Stream.BeginRead(m_Buffer, 0, m_Buffer.Length, new AsyncCallback(OnReceiveCallbackStatic), (object)null);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "ReadMessage Exception");
                //close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        private void OnReceiveCallbackStatic(IAsyncResult ar)
        {
            try
            {
                int length = m_Stream.EndRead(ar);
                if (length <= 0)
                    return;

                if (State == 0)
                {
                    Buffer.BlockCopy(m_Buffer, 0, Session.ClientKey1, 0, 128);
                    Send(Session.ServerKey1);
                    State++;
                    new Thread(new ThreadStart(ReadMessage)).Start();
                    return;
                }

                if (State == 1)
                {
                    Buffer.BlockCopy(m_Buffer, 0, Session.ClientKey2, 0, 128);
                    Send(Session.ServerKey2);
                    Session.Init();
                    State++;
                    new Thread(new ThreadStart(ReadMessage)).Start();
                    return;
                }

                byte[] data = new byte[length];
                Buffer.BlockCopy(m_Buffer, 0, data, 0, length);
                Session.Decrypt(ref data);

                using (MemoryStream stream = new MemoryStream(data))
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    while(stream.Position != stream.Length)
                    {
                        var packet = new NetPacket();
                        packet.Length = reader.ReadInt16();
                        packet.Opcode = reader.ReadInt16();
                        packet.Data = reader.ReadBytes(packet.Length - 4);
                        HandlePacket(packet);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "{0} was closed by force", m_Address);
                //close();
            }

            new Thread(new ThreadStart(ReadMessage)).Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packet"></param>
        public void HandlePacket(NetPacket packet)
        {
            if (Opcodes.Recv.ContainsKey(packet.Opcode))
            {
                ((ARecvPacket)Activator.CreateInstance(Opcodes.Recv[packet.Opcode])).Process(this, packet);
            }
            else
            {
                string opCodeLittleEndianHex = BitConverter.GetBytes(packet.Opcode).ToHex();
                Logger.Debug("Unknown Packet Opcode: 0x{0}{1} [{2}]",
                                     opCodeLittleEndianHex.Substring(2),
                                     opCodeLittleEndianHex.Substring(0, 2),
                                     packet.Data.Length);

                Logger.Debug("Data:\n{0}", packet.Data.FormatHex());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buff"></param>
        public void Send(byte[] buff)
        {
            try
            {
                //Logger.Debug("Send: {0}", buff.FormatHex());
                m_Stream.BeginWrite(buff, 0, buff.Length, new AsyncCallback(EndSendCallBackStatic), m_Stream);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "Send");
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ar"></param>
        private void EndSendCallBackStatic(IAsyncResult ar)
        {
            try
            {
                m_Stream.EndWrite(ar);
                m_Stream.Flush();
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "EndSendCallBackStatic");
            }
        }

        public void Close()
        {
            m_Client.Client.Disconnect(true);
            m_Client.Close();
            m_Stream.Close();
            Disconnected(this);
            Connections.Remove(this);
        }

        public void Dispose()
        {
            m_Client.Dispose();
            m_Stream.Dispose();
            m_Address = null;
            m_Buffer = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public delegate void OnDisconnect(Connection connection);
        public event OnDisconnect Disconnected;
    }
}
