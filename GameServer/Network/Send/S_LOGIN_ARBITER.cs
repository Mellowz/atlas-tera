namespace GameServer.Network.Send
{
    /// <summary>
    /// 
    /// </summary>
    public class S_LOGIN_ARBITER : ASendPacket
    {
        /// <summary>
        /// 
        /// </summary>
        public S_LOGIN_ARBITER()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public override void Write()
        {
            WriteH(1);
            WriteH(0);
            WriteD(0);
            WriteH(0);
            WriteD(6);
            WriteH(1);
            WriteH(0);
            WriteC(0);
        }
    }
}
