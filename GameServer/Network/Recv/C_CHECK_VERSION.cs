using GameServer.Network.Send;

namespace GameServer.Network.Recv
{
    public class C_CHECK_VERSION : ARecvPacket
    {
        /// <summary>
        /// 
        /// </summary>
        protected int ClientRevision;

        /// <summary>
        /// 
        /// </summary>
        public override void Read()
        {
            ReadH();
            ReadH();
            ReadH();
            ReadH();
            ClientRevision = ReadD();
            ReadD();
            ReadD();
            ReadD();
            ReadD();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Process()
        {
            new S_CHECK_VERSION(ClientRevision == VersionInfo.Tera.RequiredClientRevision)
                .Send(Connection);
        }
    }
}
