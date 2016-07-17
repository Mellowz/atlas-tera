namespace GameServer.Network.Send
{
    /// <summary>
    /// 
    /// </summary>
    public class S_REMAIN_PLAY_TIME : ASendPacket
    {
        /// <summary>
        /// 
        /// </summary>
        public S_REMAIN_PLAY_TIME()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public override void Write()
        {
            WriteD(Connection.Account.RemainingPlayTime);
            WriteD(0);
        }
    }
}
