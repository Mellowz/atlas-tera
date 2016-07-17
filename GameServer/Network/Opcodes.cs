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
            Recv.Add(unchecked((short)0x4DBC), typeof(C_CHECK_VERSION)); //all revs
            Recv.Add(unchecked((short)0x99D4), typeof(C_LOGIN_ARBITER)); //4503 EU
            Recv.Add(unchecked((short)0x93C5), typeof(C_SET_VISIBLE_RANGE)); //4503 EU
            Recv.Add(unchecked((short)0xA872), typeof(C_GET_USER_LIST)); //4503 EU


            Send.Add(typeof(S_CHECK_VERSION), unchecked((short)0x4DBD)); //all revs
            Send.Add(typeof(S_LOGIN_ARBITER), unchecked((short)0x95D7)); //4503 EU
            Send.Add(typeof(S_LOADING_SCREEN_CONTROL_INFO), unchecked((short)0x9685)); //4503 EU
            Send.Add(typeof(S_REMAIN_PLAY_TIME), unchecked((short)0xF26A)); //4503 EU
            Send.Add(typeof(S_LOGIN_ACCOUNT_INFO), unchecked((short)0xE09D)); //4503 EU   
            Send.Add(typeof(S_LOAD_CLIENT_ACCOUNT_SETTING), unchecked((short)0xA967)); //4503 EU    
            Send.Add(typeof(S_GET_USER_LIST), unchecked((short)0x8A1A)); //4503 EU    
        }
    }
}
