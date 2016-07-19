namespace GameServer.Network.Send
{
    /// <summary>
    /// 
    /// </summary>
    public class S_STR_EVALUATE_LIST : ASendPacket
    {
        /// <summary>
        /// 
        /// </summary>
        protected string EvaluateStr;

        /// <summary>
        /// 
        /// </summary>
        protected bool IsValid;

        /// <summary>
        /// 
        /// </summary>
        public S_STR_EVALUATE_LIST(bool valid, string str)
        {
            IsValid = valid;
            EvaluateStr = str;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Write()
        {
            WriteH(1);
            WriteH(8);
            WriteD(8);
            WriteH(22);
            WriteH(1);
            WriteD(0);
            WriteH(0);
            WriteS(EvaluateStr);
        }
    }
}
