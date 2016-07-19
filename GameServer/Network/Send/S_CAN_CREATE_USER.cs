namespace GameServer.Network.Send
{
    /// <summary>
    /// 
    /// </summary>
    public class S_CAN_CREATE_USER : ASendPacket
    {
        /// <summary>
        /// 
        /// </summary>
        protected bool CanCreatePlayer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canCreatePlayer"></param>
        public S_CAN_CREATE_USER(bool cancreate)
        {
            CanCreatePlayer = cancreate;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Write()
        {
            WriteC((byte)(CanCreatePlayer ? 1 : 0));
        }
    }
}
