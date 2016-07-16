namespace GameServer.Network
{
    public class NetPacket
    {
        public int Length { get; set; }

        public short Opcode { get; set; }

        public byte[] Data { get; set; }
    }
}
