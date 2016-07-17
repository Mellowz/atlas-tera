using GameServer.Utility;
using NLog;
using System;
using System.IO;
using System.Text;

namespace GameServer.Network
{
    /// <summary>
    /// Abstract Recieve Packet
    /// </summary>
    public abstract class ARecvPacket
    {
        /// <summary>
        /// Logger for this class
        /// </summary>
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        public BinaryReader Reader;

        /// <summary>
        /// 
        /// </summary>
        public Connection Connection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="packet"></param>
        public void Process(Connection connection, NetPacket packet)
        {
            Connection = connection;

            using (Reader = new BinaryReader(new MemoryStream(packet.Data)))
            {
                Read();
            }

            try
            {
                Process();
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, "ARecvPacket");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract void Read();

        /// <summary>
        /// 
        /// </summary>
        public abstract void Process();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected int ReadD()
        {
            try
            {
                return Reader.ReadInt32();
            }
            catch (Exception)
            {
                Logger.Warn("Missing D for: {0}", GetType());
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected int ReadC()
        {
            try
            {
                return Reader.ReadByte() & 0xFF;
            }
            catch (Exception)
            {
                Logger.Warn("Missing C for: {0}", GetType());
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected int ReadH()
        {
            try
            {
                return Reader.ReadInt16() & 0xFFFF;
            }
            catch (Exception)
            {
                Logger.Warn("Missing H for: {0}", GetType());
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected double ReadDf()
        {
            try
            {
                return Reader.ReadDouble();
            }
            catch (Exception)
            {
                Logger.Warn("Missing DF for: {0}", GetType());
            }
            return 0;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected float ReadF()
        {
            try
            {
                return Reader.ReadSingle();
            }
            catch (Exception)
            {
                Logger.Warn("Missing F for: {0}", GetType());
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected long ReadQ()
        {
            try
            {
                return Reader.ReadInt64();
            }
            catch (Exception)
            {
                Logger.Warn("Missing Q for: {0}", GetType());
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected String ReadS()
        {
            Encoding encoding = Encoding.Unicode;
            String result = "";
            try
            {
                short ch;
                while ((ch = Reader.ReadInt16()) != 0)
                    result += encoding.GetString(BitConverter.GetBytes(ch));
            }
            catch (Exception)
            {
                Logger.Warn("Missing S for: {0}", GetType());
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        protected byte[] ReadB(int length)
        {
            byte[] result = new byte[length];
            try
            {
                Reader.Read(result, 0, length);
            }
            catch (Exception)
            {
                Logger.Warn("Missing byte[] for: {0}", GetType());
            }
            return result;
        }
    }

    /// <summary>
    /// Abstract Send Packet
    /// </summary>
    public abstract class ASendPacket
    {
        /// <summary>
        /// Logger for this class
        /// </summary>
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        protected byte[] Data;

        /// <summary>
        /// 
        /// </summary>
        protected object WriteLock = new object();

        /// <summary>
        /// 
        /// </summary>
        internal Connection Connection;

        /// <summary>
        /// 
        /// </summary>
        private BinaryWriter writer;

        /*public void Send(Player player)
        {
            Send(player.Connection);
        }

        public void Send(params Player[] players)
        {
            for (int i = 0; i < players.Length; i++)
                Send(players[i].Connection);
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="states"></param>
        public void Send(params Connection[] states)
        {
            for (int i = 0; i < states.Length; i++)
                Send(states[i]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void Send(Connection state)
        {
            if (state == null)
                return;

            Connection = state;

            if (!Opcodes.Send.ContainsKey(GetType()))
            {
                Logger.Warn($"UNKNOWN packet opcode: {GetType().Name}");
                return;
            }


            lock (WriteLock)
            {
                if (Data == null)
                {
                    try
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            writer = new BinaryWriter(stream, new UTF8Encoding());
                            WriteH(0); //Reserved for length
                            WriteH(Opcodes.Send[GetType()]);
                            Write();
                            
                            Data = stream.ToArray();
                            BitConverter.GetBytes((short)Data.Length).CopyTo(Data, 0);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn($"Can't write packet: {GetType().Name}");
                        Logger.Warn(ex, "ASendPacket");
                        return;
                    }
                }
            }

            Logger.Debug($"PushPacket {GetType().Name}\r\n{Data.FormatHex()}");
            state.PushPacket(Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        public abstract void Write();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="val"></param>
        protected void WriteD(int val)
        {
            writer.Write(val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="val"></param>
        protected void WriteH(short val)
        {
            writer.Write(val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="val"></param>
        protected void WriteC(byte val)
        {
            writer.Write(val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="val"></param>
        protected void WriteDf(double val)
        {
            writer.Write(val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="val"></param>
        protected void WriteF(float val)
        {
            writer.Write(val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="val"></param>
        protected void WriteQ(long val)
        {
            writer.Write(val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="text"></param>
        protected void WriteS(String text)
        {
            if (text == null)
            {
                writer.Write((short)0);
            }
            else
            {
                Encoding encoding = Encoding.Unicode;
                writer.Write(encoding.GetBytes(text));
                writer.Write((short)0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="hex"></param>
        protected void WriteB(string hex)
        {
            writer.Write(hex.ToBytes());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="data"></param>
        protected void WriteB(byte[] data)
        {
            writer.Write(data);
        }

        /*protected void WriteUid(BinaryWriter writer, Uid uid)
        {
            if (uid == null)
            {
                writer.Write(0L);
                return;
            }

            writer.Write(uid.UID);
            writer.Write(UidFactory.GetFamily(uid).GetHashCode());
        }*/

        /*protected void WriteStats(BinaryWriter writer, Player player)
        {
            CreatureBaseStats baseStats = player.GameStats; // Communication.Global.StatsService.InitStats(Player)

            #region Base stats
            WriteD(writer, baseStats.Power - player.EffectsImpact.ChangeOfPower);
            WriteD(writer, baseStats.Endurance - player.EffectsImpact.ChangeOfEndurance);
            WriteH(writer, (short)(baseStats.ImpactFactor - player.EffectsImpact.ChangeOfImpactFactor));
            WriteH(writer, (short)(baseStats.BalanceFactor - player.EffectsImpact.ChangeOfBalanceFactor));
            WriteH(writer, (short)(baseStats.Movement - player.EffectsImpact.ChangeOfMovement));
            WriteH(writer, (short)(baseStats.AttackSpeed - player.EffectsImpact.ChangeOfAttackSpeed));

            // Crit. stats
            WriteF(writer, baseStats.CritChanse);
            WriteF(writer, baseStats.CritResist);
            WriteH(writer, 0);
            WriteC(writer, 0);
            WriteC(writer, 0x40); // Crit how much times?

            WriteD(writer, baseStats.Attack); //min attack
            WriteD(writer, baseStats.Attack); //max attack
            WriteD(writer, baseStats.Defense);
            WriteH(writer, (short)baseStats.Impact);
            WriteH(writer, (short)baseStats.Balance);

            WriteF(writer, baseStats.WeakeningResist); // Weakening
            WriteF(writer, baseStats.PeriodicResist); // Periodic
            WriteF(writer, baseStats.StunResist); // Stun
            #endregion

            #region Additional stats
            WriteD(writer, player.EffectsImpact.ChangeOfPower);
            WriteD(writer, player.EffectsImpact.ChangeOfEndurance);
            WriteH(writer, player.EffectsImpact.ChangeOfImpactFactor);
            WriteH(writer, player.EffectsImpact.ChangeOfBalanceFactor);

            WriteH(writer, player.EffectsImpact.ChangeOfMovement);
            WriteH(writer, player.EffectsImpact.ChangeOfAttackSpeed);

            WriteF(writer, player.GameStats.CritChanse - baseStats.CritChanse);
            WriteF(writer, player.GameStats.CritResist - baseStats.CritResist);
            WriteD(writer, 0);

            WriteD(writer, player.GameStats.Attack - baseStats.Attack); //min attack
            WriteD(writer, player.GameStats.Attack - baseStats.Attack); //max attack
            WriteD(writer, player.GameStats.Defense - baseStats.Defense);

            WriteH(writer, (short)(player.GameStats.Impact - baseStats.Impact));
            WriteH(writer, (short)(player.GameStats.Balance - baseStats.Balance));

            WriteF(writer, player.GameStats.WeakeningResist - baseStats.WeakeningResist); // Weakening
            WriteF(writer, player.GameStats.PeriodicResist - baseStats.PeriodicResist); // Periodic
            WriteF(writer, player.GameStats.StunResist - baseStats.StunResist); // Stun
            #endregion
        }

        protected void WriteGatherStats(BinaryWriter writer, Player player)
        {
            WriteH(writer, player.PlayerCraftStats.GetGatherStat(TypeName.Energy));
            WriteH(writer, player.PlayerCraftStats.GetGatherStat(TypeName.Herb));
            WriteH(writer, 0); //unk, mb bughunting
            WriteH(writer, player.PlayerCraftStats.GetGatherStat(TypeName.Mine));
        }*/
    }
}
