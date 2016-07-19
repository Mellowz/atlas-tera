namespace GameServer.Network.Send
{
    public class S_CHECK_USERNAME : ASendPacket
    {
        /// <summary>
        /// 
        /// </summary>
        protected bool Success;

        /// <summary>
        /// 
        /// </summary>
        public S_CHECK_USERNAME(bool success)
        {
            Success = success;
        }

        public override void Write()
        {
            WriteC((byte)(Success ? 1 : 0));
        }
    }
}
