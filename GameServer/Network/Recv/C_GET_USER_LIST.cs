﻿using GameServer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.Recv
{
    /// <summary>
    /// 
    /// </summary>
    public class C_GET_USER_LIST : ARecvPacket
    {
        /// <summary>
        /// 
        /// </summary>
        public override void Read()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override void Process()
        {
            PlayerService.ShowPlayerList(Connection);
        }
    }
}
