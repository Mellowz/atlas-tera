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
        public virtual int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int AccountId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Race Race { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Gender Gender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual PlayerClass Class { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual int MapId { get; set; }

        /// <summary>
        /// Position of Player Character
        /// </summary>
        public virtual float X { get; set; }
        public virtual float Y { get; set; }
        public virtual float Z { get; set; }
        public virtual float Heading { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual byte[] Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual byte[] Detail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual byte[] Detail2 { get; set; }
    }
}
