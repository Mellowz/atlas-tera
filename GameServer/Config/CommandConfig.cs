namespace GameServer.Config
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CommandConfig : Config
    {
        /// <summary>
        /// Config Variable
        /// </summary>
        public char CommandPrefix { get { return this.GetString("CommandPrefix", "!")[0]; } set { this.Set("CommandPrefix", value); } }

        /// <summary>
        /// 
        /// </summary>
        private static readonly CommandConfig _instance = new CommandConfig();

        /// <summary>
        /// 
        /// </summary>
        public static CommandConfig Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private CommandConfig() : base("Commands")
        {
        }
    }
}
