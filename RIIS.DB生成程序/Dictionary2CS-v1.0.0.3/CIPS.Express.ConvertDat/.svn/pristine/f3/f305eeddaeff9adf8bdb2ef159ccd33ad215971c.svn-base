using System;
using System.Collections.Generic;
using System.Text;

namespace CIPS.Express.ConvertDat
{
    /// <summary>
    /// 
    /// </summary>
    public class MainInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public class Infomation
        {
            /// <summary>
            /// 
            /// </summary>
            public DateTime timeCIPSRC = new DateTime(1999, 1, 1);
            /// <summary>
            /// 
            /// </summary>
            public DateTime timeCIPSPC = new DateTime(1999, 1, 1);
            /// <summary>
            /// 
            /// </summary>
            public DateTime timeCIPSPL = new DateTime(1999, 1, 1);
            /// <summary>
            /// 
            /// </summary>
            public bool toChart = false;
            /// <summary>
            /// 
            /// </summary>
            public bool running = false;
            /// <summary>
            /// 
            /// </summary>
            public short plansum = 0;
            /// <summary>
            /// 
            /// </summary>
            public short pushedPlan = 0;
            /// <summary>
            /// 
            /// </summary>
            public short finishedPlan = 0;
            /// <summary>
            /// 
            /// </summary>
            public short draftPlan = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytes(Infomation data)
        {
            byte[] b = new byte[2 + 1 + 6 * 3 + 2 * 4];
            b[0] = Command.Cmd_MainInfo;
            b[1] = Command.DataType_MainTable;
            if (data.toChart)
                b[2] |= 1;
            if (data.running)
                b[2] |= 2;
            int off = 3;
            BitConverter.GetBytes(data.plansum).CopyTo(b, off);
            off += 2;
            BitConverter.GetBytes(data.finishedPlan).CopyTo(b, off);
            off += 2;
            BitConverter.GetBytes(data.pushedPlan).CopyTo(b, off);
            off += 2;
            BitConverter.GetBytes(data.draftPlan).CopyTo(b, off);
            off += 2;
            Tools.Time2Byte(data.timeCIPSRC).CopyTo(b, off);
            off += 6;
            Tools.Time2Byte(data.timeCIPSPC).CopyTo(b, off);
            off += 6;
            Tools.Time2Byte(data.timeCIPSPL).CopyTo(b, off);
            off += 6;
            return b;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Infomation ToStruct(byte[] b)
        {
            Infomation inf = new Infomation();
            if (b[1] == Command.DataType_MainTable && b[0] == Command.Cmd_MainInfo)
            {
                inf.toChart = (b[2] & 1) == 1;
                inf.running = (b[2] & 2) == 2;
                int off = 3;
                inf.plansum = BitConverter.ToInt16(b, off);
                off += 2;
                inf.finishedPlan = BitConverter.ToInt16(b, off);
                off += 2;
                inf.pushedPlan = BitConverter.ToInt16(b, off);
                off += 2;
                inf.draftPlan = BitConverter.ToInt16(b, off);
                off += 2;
                inf.timeCIPSRC = Tools.Byte2Time(b, off);
                off += 6;
                inf.timeCIPSPC = Tools.Byte2Time(b, off);
                off += 6;
                inf.timeCIPSPL = Tools.Byte2Time(b, off);
                off += 6;
            }
            return inf;
        }
    }
}
