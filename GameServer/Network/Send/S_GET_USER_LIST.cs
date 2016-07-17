namespace GameServer.Network.Send
{
    /// <summary>
    /// 
    /// </summary>
    public class S_GET_USER_LIST : ASendPacket
    {
        /// <summary>
        /// 
        /// </summary>
        public S_GET_USER_LIST()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override void Write()
        {
            WriteH(0); // size of player

            WriteH(0); // player offset
            WriteD(0);
            WriteC(0);
            WriteD(2); // max player per account
            WriteD(1);
            WriteH(0);
            WriteD(40);
            WriteD(0);
            WriteD(24);
        }
    }
}
