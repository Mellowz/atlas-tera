namespace GameServer.Network.Send
{
    /// <summary>
    /// 
    /// </summary>
    public class S_LOAD_CLIENT_ACCOUNT_SETTING : ASendPacket
    {
        /// <summary>
        /// 
        /// </summary>
        public S_LOAD_CLIENT_ACCOUNT_SETTING()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public override void Write()
        {
            WriteD(0);
        }
    }
}
