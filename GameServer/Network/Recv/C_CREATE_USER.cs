using GameServer.Model.Mappings.Players;
using GameServer.Model.Player;
using GameServer.Service;

namespace GameServer.Network.Recv
{
    /// <summary>
    /// 
    /// </summary>
    public class C_CREATE_USER : ARecvPacket
    {
        /// <summary>
        /// 
        /// </summary>
        protected PlayerDto Player = new PlayerDto();

        /// <summary>
        /// 
        /// </summary>
        public override void Read()
        {
            int nameShift = ReadH()-4;   // 40 - 4 = 36 // Name Shift
            int detailShift = ReadH()-4; // 52 - 4 = 48 // details Shift
            int detailLength = ReadH(); // 32
            int unkShift = ReadH()-4; // 84 - 4 = 80
            int unkLength = ReadH(); // 64

            Player.Gender = (Gender)ReadD(); // Gender 0,1
            Player.Race = (Race)ReadH(); // Race
            Player.Class = (PlayerClass)ReadD(); // Class Job
            Player.Data = ReadB(12);
            ReadD();
            Player.Name = ReadS();
            Player.Detail = ReadB(detailLength);
            Player.Detail2 = ReadB(unkLength);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Process()
        {
            PlayerService.CreatePlayer(Connection, Player);
        }
    }
}
