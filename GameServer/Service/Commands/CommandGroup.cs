using GameServer.Network;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameServer.Service.Commands
{
    public class CommandGroup
    {
        /// <summary>
        /// Logger for this class
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        public CommandGroupAttribute Attributes { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<CommandAttribute, MethodInfo> _commands = new Dictionary<CommandAttribute, MethodInfo>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributes"></param>
        public void Register(CommandGroupAttribute attributes)
        {
            this.Attributes = attributes;
            this.RegisterDefaultCommand();
            this.RegisterCommands();
        }

        /// <summary>
        /// 
        /// </summary>
        private void RegisterCommands()
        {
            foreach (var method in this.GetType().GetMethods())
            {
                object[] attributes = method.GetCustomAttributes(typeof(CommandAttribute), true);
                if (attributes.Length == 0) continue;

                var attribute = (CommandAttribute)attributes[0];
                if (attribute is DefaultCommand) continue;

                if (!this._commands.ContainsKey(attribute))
                    this._commands.Add(attribute, method);
                else
                    Logger.Warn("There exists an already registered command '{0}'.", attribute.Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void RegisterDefaultCommand()
        {
            foreach (var method in this.GetType().GetMethods())
            {
                object[] attributes = method.GetCustomAttributes(typeof(DefaultCommand), true);
                if (attributes.Length == 0) continue;
                if (method.Name.ToLower() == "fallback") continue;

                this._commands.Add(new DefaultCommand(this.Attributes.MinUserLevel), method);
                return;
            }

            // set the fallback command if we couldn't find a defined DefaultCommand.
            this._commands.Add(new DefaultCommand(this.Attributes.MinUserLevel), this.GetType().GetMethod("Fallback"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="invokerCon"></param>
        /// <returns></returns>
        public virtual string Handle(string parameters, Connection invokerCon = null)
        {
            // check if the user has enough privileges to access command group.
            // check if the user has enough privileges to invoke the command.
            if (invokerCon != null && this.Attributes.MinUserLevel > invokerCon.Account.AccountLevel)
                return "You don't have enough privileges to invoke that command.";

            string[] @params = null;
            CommandAttribute target = null;

            if (parameters == string.Empty)
                target = this.GetDefaultSubcommand();
            else
            {
                @params = parameters.Split(' ');
                target = this.GetSubcommand(@params[0]) ?? this.GetDefaultSubcommand();

                if (target != this.GetDefaultSubcommand())
                    @params = @params.Skip(1).ToArray();
            }

            // check if the user has enough privileges to invoke the command.
            if (invokerCon != null && target.MinUserLevel > invokerCon.Account.AccountLevel)
                return "You don't have enough privileges to invoke that command.";

            return (string)this._commands[target].Invoke(this, new object[] { @params, invokerCon });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string GetHelp(string command)
        {
            foreach (var pair in this._commands)
            {
                if (command != pair.Key.Name) continue;
                return pair.Key.Help;
            }

            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="params"></param>
        /// <param name="invokerCon"></param>
        /// <returns></returns>
        [DefaultCommand]
        public virtual string Fallback(string[] @params = null, Connection invokerCon = null)
        {
            var output = "Available subcommands: ";
            foreach (var pair in this._commands)
            {
                if (pair.Key.Name.Trim() == string.Empty) continue; // skip fallback command.
                if (invokerCon != null && pair.Key.MinUserLevel > invokerCon.Account.AccountLevel) continue;
                output += pair.Key.Name + ", ";
            }

            return output.Substring(0, output.Length - 2) + ".";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected CommandAttribute GetDefaultSubcommand()
        {
            return this._commands.Keys.First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected CommandAttribute GetSubcommand(string name)
        {
            return this._commands.Keys.FirstOrDefault(command => command.Name == name);
        }
    }
}
