using GameServer.Network;
using NLog;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace GameServer
{
    /// <summary>
    /// 
    /// </summary>
    public class GameServer
    {
        /// <summary>
        /// Logger for this class
        /// </summary>
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Network Listener Instance
        /// </summary>
        public static NetListener netListener = new NetListener();

        /// <summary>
        /// Used for uptime calculations.
        /// </summary>
        public static readonly DateTime StartupTime = DateTime.Now; // used for uptime calculations.

        /// <summary>
        /// GameServer thread.
        /// </summary>
        protected static Thread GameServerThread;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Title = "Atlas Tera :: GameServer";

            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler; // Watch for any unhandled exceptions.
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            Console.ForegroundColor = ConsoleColor.Green;
            PrintLicense(); // print license text.
            Console.ResetColor(); // reset color back to default.

            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------");

            Logger.Info("GameServer v{0} warming-up..", Assembly.GetExecutingAssembly().GetName().Version);
            Logger.Info("Required client Revision: {0}.", VersionInfo.Tera.RequiredClientRevision);

            StartupServers(); // startup the servers

            Process.GetCurrentProcess().WaitForExit();
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;

            if (e.IsTerminating)
                Logger.Fatal(ex, "GameServer terminating because of unhandled exception.");
            else
                Logger.Error(ex);

            Console.ReadLine();
        }

        #region server startup managment

        /// <summary>
        /// Startup Server 
        /// </summary>
        private static void StartupServers()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Opcodes.Init();
            Logger.Info($"Init packet: Recv[{Opcodes.Recv.Count}], Send[{Opcodes.Send.Count}]");

            Connection.SendAllThread.Start();
            // todo load data

            GameServerThread = new Thread(netListener.Run) { IsBackground = true, CurrentCulture = CultureInfo.InvariantCulture };
            GameServerThread.Start();

            sw.Stop();
            Thread.Sleep(500);
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("               Server start in {0}", (sw.ElapsedMilliseconds / 1000.0).ToString("0.00s"));
            Console.WriteLine("-----------------------------------------------------------");
        }

        /// <summary>
        /// Prints a copyright banner.
        /// </summary>
        private static void PrintLicense()
        {
            Console.WriteLine("Copyright (C) 2016 - 2017, Atlas Tera Emulator project");
            Console.WriteLine("Atlas Tera Emulator comes with ABSOLUTELY NO WARRANTY.");
            Console.WriteLine("This is free software, and you are welcome to redistribute it under certain conditions; see the LICENSE file for details.");
            Console.WriteLine();
        }

        #endregion
    }
}
