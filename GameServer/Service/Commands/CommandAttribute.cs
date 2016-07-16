using GameServer.Model.Account;
using System;

namespace GameServer.Service.Commands
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandGroupAttribute : Attribute
    {
        /// <summary>
        /// Command group's name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Help text for command group.
        /// </summary>
        public string Help { get; private set; }

        /// <summary>
        /// Minimum user level required to invoke the command.
        /// </summary>
        public AccountLevel MinUserLevel { get; private set; }

        public CommandGroupAttribute(string name, string help, AccountLevel minUserLevel = AccountLevel.User)
        {
            this.Name = name.ToLower();
            this.Help = help;
            this.MinUserLevel = minUserLevel;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// Command's name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Help text for command.
        /// </summary>
        public string Help { get; private set; }

        /// <summary>
        /// Minimum user level required to invoke the command.
        /// </summary>
        public AccountLevel MinUserLevel { get; private set; }

        public CommandAttribute(string command, string help, AccountLevel minUserLevel = AccountLevel.User)
        {
            this.Name = command.ToLower();
            this.Help = help;
            this.MinUserLevel = minUserLevel;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class DefaultCommand : CommandAttribute
    {
        public DefaultCommand(AccountLevel minUserLevel = AccountLevel.User)
            : base("", "", minUserLevel)
        {
        }
    }
}
