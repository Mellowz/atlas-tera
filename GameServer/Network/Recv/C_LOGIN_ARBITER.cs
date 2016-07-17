using GameServer.Service;
using System.Text;

namespace GameServer.Network.Recv
{
    /// <summary>
    /// 
    /// </summary>
    public class C_LOGIN_ARBITER : ARecvPacket
    {
        protected string AccountName;
        protected string Token;

        /// <summary>
        /// 
        /// </summary>
        public override void Read()
        {
            ReadH(); //unk1
            ReadH(); //unk2
            int length = ReadH();
            ReadB(5); //unk3
            ReadD(); //unk4
            ReadD(); //unk5
            AccountName = ReadS();
            Token = Encoding.ASCII.GetString(ReadB(length));
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Process()
        {
            AccountService.TryAuthorize(Connection, AccountName, Token);
        }
    }
}
