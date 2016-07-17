namespace GameServer.Network.Send
{
    /// <summary>
    /// 
    /// </summary>
    public class S_LOGIN_ACCOUNT_INFO : ASendPacket
    {
        /// <summary>
        /// 
        /// </summary>
        public S_LOGIN_ACCOUNT_INFO()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public override void Write()
        {
            WriteH(14);
            WriteH(unchecked((short)0x1D90));
            WriteH(0x0044);
            WriteH(0);
            WriteH(0);
            WriteS("PlanetDB_18");
        }
    }
}
