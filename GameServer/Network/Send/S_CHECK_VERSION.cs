using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.Send
{
    public class S_CHECK_VERSION : ASendPacket
    {
        protected bool CheckOk = false;

        public S_CHECK_VERSION(bool checkOk)
        {
            CheckOk = checkOk;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteC(writer, (byte)(CheckOk ? 1 : 0));
        }
    }
}
