namespace GameServer.Network.Send
{
    /// <summary>
    /// 
    /// </summary>
    public class S_LOADING_SCREEN_CONTROL_INFO : ASendPacket
    {
        /// <summary>
        /// 
        /// </summary>
        public S_LOADING_SCREEN_CONTROL_INFO()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public override void Write()
        {
            WriteC(1);
        }
    }
}
