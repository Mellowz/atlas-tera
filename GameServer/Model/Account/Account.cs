using GameServer.Model.Mappings.Accounts;

namespace GameServer.Model.Account
{
    /// <summary>
    /// Account Model Class
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 
        /// </summary>
        private AccountDto _AccountDto;

        /// <summary>
        /// 
        /// </summary>
        public Account(AccountDto dto)
        {
            _AccountDto = dto;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get { return _AccountDto.Id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _AccountDto.Name; }
            set { _AccountDto.Name = value; }
        }

        /// <summary>
        /// Account Password
        /// Read Only
        /// </summary>
        public string Password
        {
            get { return _AccountDto.Password; }
        }

        /// <summary>
        /// 
        /// </summary>
        public AccountLevel AccountLevel
        {
            get { return _AccountDto.AccountLevel; }
            set { _AccountDto.AccountLevel = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaxPlayers
        {
            get { return _AccountDto.MaxPlayers; }
            set { _AccountDto.MaxPlayers = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int RemainingPlayTime
        {
            get { return _AccountDto.RemainingPlayTime; }
            set { _AccountDto.RemainingPlayTime = value; }
        }

        /// <summary>
        /// Account Token, Guid Type
        /// Generate only login from a web server side
        /// </summary>
        public string Token
        {
            get { return _AccountDto.Token; }
        }
    }
}
