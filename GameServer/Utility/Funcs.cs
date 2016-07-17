using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public static class Funcs
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly Random Randomizer = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Random Random()
        {
            return Randomizer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetRoundedUtc()
        {
            // ReSharper disable PossibleLossOfFraction
            return (int)Math.Round((double)(GetCurrentMilliseconds() / 1000));
            // ReSharper restore PossibleLossOfFraction
        }

        /// <summary>
        /// 
        /// </summary>
        private static readonly DateTime StaticDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentMilliseconds()
        {
            return (long)(DateTime.UtcNow - StaticDate).TotalMilliseconds;
        }

        /// <summary>
        /// 
        /// </summary>
        private static readonly string[] Baths;

        /// <summary>
        /// 
        /// </summary>
        static Funcs()
        {
            Baths = new string[256];
            for (int i = 0; i < 256; i++)
                Baths[i] = String.Format("{0:X2}", i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string ToHex(this byte[] array)
        {
            StringBuilder builder = new StringBuilder(array.Length * 2);

            for (int i = 0; i < array.Length; i++)
                builder.Append(Baths[array[i]]);

            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string FormatHex(this byte[] data)
        {
            StringBuilder builder = new StringBuilder(data.Length * 4);

            int count = 0;
            int pass = 1;
            foreach (byte b in data)
            {
                if (count == 0)
                    builder.AppendFormat("{0,-6}\t", "[" + (pass - 1) * 16 + "]");

                count++;
                builder.Append(b.ToString("X2"));
                if (count == 4 || count == 8 || count == 12)
                    builder.Append(" ");
                if (count == 16)
                {
                    builder.Append("\t");
                    for (int i = (pass * count) - 16; i < (pass * count); i++)
                    {
                        char c = (char)data[i];
                        if (c > 0x1f && c < 0x80)
                            builder.Append(c);
                        else
                            builder.Append(".");
                    }
                    builder.Append("\r\n");
                    count = 0;
                    pass++;
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this String hexString)
        {
            try
            {
                byte[] result = new byte[hexString.Length / 2];

                for (int index = 0; index < result.Length; index++)
                {
                    string byteValue = hexString.Substring(index * 2, 2);
                    result[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                }

                return result;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid hex string: {0}", hexString);
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chance"></param>
        /// <returns></returns>
        public static bool IsLuck(byte chance)
        {
            if (chance >= 100)
                return true;

            if (chance <= 0)
                return false;

            return new Random().Next(0, 100) <= chance;
        }

        /// <summary>
        /// Check object is Exsist
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsExists(this object obj)
        {
            return (obj != null);
        }
    }
}
