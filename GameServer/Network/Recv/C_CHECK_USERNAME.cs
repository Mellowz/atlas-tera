using GameServer.Service;

namespace GameServer.Network.Recv
{
    /// <summary>
    /// 
    /// </summary>
    public class C_CHECK_USERNAME : ARecvPacket
    {
        /// <summary>
        /// 
        /// </summary>
        protected string Name;

        /// <summary>
        /// 
        /// </summary>
        public override void Read()
        {
            ReadH(); // Unk
            Name = ReadS();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Process()
        {
            PlayerService.CheckPlayerName(Connection, Name);
        }
    }
}
