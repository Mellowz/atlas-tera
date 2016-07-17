namespace GameServer.Network.Recv
{
    /// <summary>
    /// 
    /// </summary>
    public class C_SET_VISIBLE_RANGE : ARecvPacket
    {
        /// <summary>
        /// 
        /// </summary>
        protected int Range;

        /// <summary>
        /// 
        /// </summary>
        public override void Read()
        {
            Range = ReadD();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Process()
        {
            
        }
    }
}
