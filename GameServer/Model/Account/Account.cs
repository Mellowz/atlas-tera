using System.Collections.Generic;

namespace GameServer.Model.Account
{
    /// <summary>
    /// Account Model Class
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Account ID
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Account Name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Account Password
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// Account Level
        /// </summary>
        public virtual AccountLevel AccountLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int MaxPlayers { get; set; }

        /// <summary>
        /// Account Remaining Play Time
        /// </summary>
        public virtual int RemainingPlayTime { get; set; }

        /// <summary>
        /// Account Token
        /// </summary>
        public virtual string Token { get; set; }
    }
}
