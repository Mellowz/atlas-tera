using GameServer.Network.Recv;
using GameServer.Network.Send;
using System;
using System.Collections.Generic;

namespace GameServer.Network
{
    public class Opcodes
    {
        public static Dictionary<short, Type> Recv = new Dictionary<short, Type>();
        public static Dictionary<Type, short> Send = new Dictionary<Type, short>();

        public static int Version = 4503;

        public static void Init()
        {
            Recv.Add(unchecked((short) 0x4DBC), typeof(C_CHECK_VERSION)); //all revs
            Recv.Add(unchecked((short) 0x99D4), typeof(C_LOGIN_ARBITER)); //4503 EU


            Send.Add(typeof(S_CHECK_VERSION), unchecked((short)0x4DBD)); //all revs
        }
    }
}
