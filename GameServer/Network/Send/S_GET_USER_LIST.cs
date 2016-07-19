using GameServer.Model.Player;
using System.IO;

namespace GameServer.Network.Send
{
    /// <summary>
    /// 
    /// </summary>
    public class S_GET_USER_LIST : ASendPacket
    {
        /// <summary>
        /// 
        /// </summary>
        public S_GET_USER_LIST()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override void Write()
        {
            WriteH((short)Connection.Players.Count); // character count
            WriteH((short)(Connection.Players.Count == 0 ? 0 : 35));

            WriteB(new byte[5]);

            WriteD(Connection.Account.MaxPlayers); // max character for account
            WriteD(1);
            WriteH(0);

            for (int i = 0; i < Connection.Players.Count; i++)
            {
                Player player = Connection.Players[i];

                short check1 = (short)writer.BaseStream.Position;
                WriteH(check1); //Check1
                WriteH(0); //Check2
                WriteH(0); //Name shift
                WriteH(0); //Details shift
                WriteH((short)player.PlayerData.Detail.Length); //Details length

                WriteD(player.Id); //PlayerId
                WriteD(player.Gender.GetHashCode()); //Gender
                WriteD(player.Race.GetHashCode()); //Race
                WriteD(player.Class.GetHashCode()); //Class
                WriteD(1); //Level

                WriteB("260B000087050000"); //A0860100A0860100
                WriteB(new byte[12]); // ZoneDatas
                WriteD(0); // lastonline int
                WriteB("00000000008F480900000000006AD376B0"); //Unk
                //WriteB("000000A0860100A0860100000000000000 00000000 00008F7E 00000000 0000000F B9090000 00000001 91DDB1"); //New character, play start video
                WriteD(0); //Item (hands)
                WriteD(0); //Item (earing1?)
                WriteD(0); //Item (earing2?)
                WriteD(0); //Item (body)
                WriteD(0); //Item (gloves)
                WriteD(0); //Item (boots)
                WriteD(0); //Item (ring1)
                WriteD(0); //Item (ring2)
                WriteD(0); //Item ?
                WriteD(0); //Item ?
                WriteD(0); //Item ?

                WriteB(player.PlayerData.Data);
                WriteC(0); //Offline?
                WriteB("0000000000000000000000000089E66EB0"); //???
                WriteB(new byte[48]);

                WriteD(0); // color (hands)
                WriteD(0); // color (body)
                WriteD(0); // color (gloves)
                WriteD(0); // color (boots)

                WriteB(new byte[28]); //16 bytes possible colors

                WriteD(0); //Rested (current)
                WriteD(10000); //Rested (max)

                WriteC(1);
                WriteC((byte)(/*player.Exp == 0 ? 1 : 0*/ 1)); //Intro video flag

                WriteD(0); //Now start only in Island of Dawn
                //WriteD(player.Exp == 0 ? 1 : 0); //Prolog or IslandOfDawn dialog window

                writer.Seek(check1 + 4, SeekOrigin.Begin);
                WriteH((short)writer.BaseStream.Length); //Name shift
                writer.Seek(0, SeekOrigin.End);

                WriteS(player.Name);

                writer.Seek(check1 + 6, SeekOrigin.Begin);
                WriteH((short)writer.BaseStream.Length); //Details shift
                writer.Seek(0, SeekOrigin.End);

                WriteB(player.PlayerData.Detail);

                if (i != Connection.Players.Count - 1)
                {
                    writer.Seek(check1 + 2, SeekOrigin.Begin);
                    WriteH((short)writer.BaseStream.Length); //Check2
                    writer.Seek(0, SeekOrigin.End);
                }
            }

            WriteD(40);
            WriteD(0);
            WriteD(24);
        }
    }
}
