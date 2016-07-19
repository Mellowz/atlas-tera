using GameServer.Service;

namespace GameServer.Network.Recv
{
    /// <summary>
    /// 
    /// </summary>
    public class C_STR_EVALUATE_LIST : ARecvPacket
    {
        /// <summary>
        /// 
        /// </summary>
        protected string EvaluateStr;

        /// <summary>
        /// 
        /// </summary>
        public override void Read()
        {
            ReadH();
            ReadH();

            ReadD();
            ReadD();

            ReadH();
            ReadD();

            EvaluateStr = ReadS();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Process()
        {
            PlayerService.CheckEvaluateStr(Connection, EvaluateStr);
        }
    }
}
