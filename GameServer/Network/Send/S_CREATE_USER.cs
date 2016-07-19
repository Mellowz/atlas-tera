namespace GameServer.Network.Send
{
    /// <summary>
    /// 
    /// </summary>
    public class S_CREATE_USER : ASendPacket
    {
        /// <summary>
        /// 
        /// </summary>
        protected bool Result;

        /// <summary>
        /// 
        /// </summary>
        public S_CREATE_USER(bool result)
        {
            Result = result;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Write()
        {
            WriteC((byte)(Result ? 1 : 0));
        }
    }
}
