namespace GameServer
{
    /// <summary>
    /// Supported Versions Info.
    /// </summary>
    /// <remarks>Put anything related to versions here.</remarks>
    public static class VersionInfo
    {
        /// <summary>
        /// Main assembly versions info.
        /// </summary>
        public static class Assembly
        {
            /// <summary>
            /// Main assemblies version.
            /// </summary>
            public const string Version = "45.03.04-ls";
        }

        /// <summary>
        /// Tera versions info.
        /// </summary>
        public static class Tera
        {
            /// <summary>
            /// Required client version.
            /// </summary>
            public const int RequiredClientRevision = 303439;

            /// <summary>
            /// Required client region
            /// </summary>
            public static string Region = "EU";
        }
    }
}
