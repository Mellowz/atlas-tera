using GameServer.Model.Mappings.Players;
using GameServer.Model.World;

namespace GameServer.Model.Player
{
    /// <summary>
    /// Player Character Instance
    /// </summary>
    public class Player
    {
        /// <summary>
        /// 
        /// </summary>
        private PlayerDto _PlayerDto;

        /// <summary>
        /// 
        /// </summary>
        private Position _Position;

        /// <summary>
        /// 
        /// </summary>
        private PlayerData _PlayerData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        public Player(PlayerDto dto)
        {
            _PlayerDto = dto;
            _Position = new Position()
            {
                MapId = dto.MapId,
                X = dto.X,
                Y = dto.Y,
                Z = dto.Z,
                Heading = dto.Heading
            };
            _PlayerData = new PlayerData()
            {
                Data = dto.Data,
                Detail = dto.Detail,
                Detail2 = dto.Detail2
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get { return _PlayerDto.Id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AccountId
        {
            get { return _PlayerDto.AccountId; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _PlayerDto.Name; }
            set { _PlayerDto.Name = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Race Race
        {
            get { return _PlayerDto.Race; }
            set { _PlayerDto.Race = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Gender Gender
        {
            get { return _PlayerDto.Gender; }
            set { _PlayerDto.Gender = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PlayerClass Class
        {
            get { return _PlayerDto.Class; }
            set { _PlayerDto.Class = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Position Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public PlayerData PlayerData
        {
            get { return _PlayerData; }
            set { _PlayerData = value; }
        }
    }
}
