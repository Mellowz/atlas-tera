using NLog;

namespace GameServer.Network.Recv
{
    /// <summary>
    /// 
    /// </summary>
    public class C_HARDWARE_INFO : ARecvPacket
    {
        /// <summary>
        /// Logger for this class
        /// </summary>
        protected static readonly new Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        protected string HardWareOS;

        /// <summary>
        /// 
        /// </summary>
        protected string HardWareCPU;

        /// <summary>
        /// 
        /// </summary>
        protected string HardWareGPU;

        /// <summary>
        /// 
        /// </summary>
        public override void Read()
        {
            ReadB(51);

            HardWareOS = ReadS();
            HardWareCPU = ReadS();
            HardWareGPU = ReadS();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Process()
        {
            
        }
    }
}
