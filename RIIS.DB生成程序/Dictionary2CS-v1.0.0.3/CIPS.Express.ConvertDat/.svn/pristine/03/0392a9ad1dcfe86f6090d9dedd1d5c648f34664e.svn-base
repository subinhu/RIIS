using System;
using System.Collections.Generic;
using System.Text;

namespace CIPS.Express.ConvertDat
{
    public class YH5
    {
        public class TICKET
        {
            public byte[] bData = new byte[8];
            public short[] isData = new short[8];
            public int[] iData = new int[4];
            public string[] sData = new string[15];
            public DateTime[] tData = new DateTime[3];
            public ClRow Row = null;
            public TICKET(ClTable ct)
            {
                Init(ct.NewRow());
            }
            public TICKET(ClRow cr)
            {
                Init(cr);
            }
            public TICKET()
            {
                int i;
                for (i = 0; i < bData.Length; i++)
                    bData[i] = 0;
                for (i = 0; i < sData.Length; i++)
                    sData[i] = "";
                for (i = 0; i < tData.Length; i++)
                    tData[i] = new DateTime(1999, 1, 1);
            }
            void Init(ClRow cr)
            {
                ClTable ct = new ClTable("");
                ct.AddColumn("I_CAR_CO", typeof(uint));
                ct.AddColumn("F_LEN", typeof(byte));
                ct.AddColumn("I_GRO_ID", typeof(short));
                ct.AddColumn("B_CAR_CHARA", typeof(long));
                ct.AddColumn("E_CAR_CHARA", typeof(byte));
                ct.AddColumn("C_GOODS_NA", typeof(string));
                ct.AddColumn("C_STA_NA_ARR", typeof(string));
                ct.AddColumn("C_STA_NA_DEP", typeof(string));
                ct.AddColumn("I_CAR_ID", typeof(int));
                ct.AddColumn("E_OIL_TYPE", typeof(byte));
                ct.AddColumn("C_CONSIGNOR", typeof(string));
                ct.AddColumn("C_CONSIGNEE", typeof(string));
                ct.AddColumn("C_CAR_TYPE", typeof(string));
                ct.AddColumn("C_REMARK", typeof(string));
                ct.AddColumn("C_CANVAS_ID", typeof(string));
                ct.AddColumn("C_CANVAS_ID1", typeof(string));
                ct.AddColumn("C_CANVAS_ID2", typeof(string));
                ct.AddColumn("F_WEGH_OWN", typeof(short));
                ct.AddColumn("F_WEGH_LOAD", typeof(short));
                ct.AddColumn("D_TRA_ARR", typeof(DateTime));
                ct.AddColumn("C_TRA_NUM_ARR", typeof(string));
                ct.AddColumn("I_LINE_ID", typeof(short));
                ct.AddColumn("B_CAR_STATE", typeof(short));
                ct.AddColumn("B_CAR_CUSTOM", typeof(byte));
                ct.AddColumn("C_PL_CO_RATIFY", typeof(string));
                ct.AddColumn("I_CAR_NUM_RATIFY", typeof(short));
                ct.AddColumn("I_CAR_NUM_CLOAD", typeof(short));
                ct.AddColumn("E_PL_TYPE_FRT", typeof(byte));
                ct.AddColumn("I_BATCH_NUM", typeof(byte));
                ct.AddColumn("I_WORK_NUM", typeof(byte));
                ct.AddColumn("C_OP_LOAD_CHECK", typeof(string));
                ct.AddColumn("E_WORK_RANGE", typeof(short));
                ct.AddColumn("I_GOODS_NUM", typeof(short));
                ct.AddColumn("C_BOOTH_ID", typeof(string));
                ct.AddColumn("C_INV_CO", typeof(string));
                Row = ct.NewRow();

                foreach (string s in ct.Columns)
                {
                    if (cr[s] != null)
                        Row[s] = cr[s];
                }
                int i;
                for (i = 0; i < bData.Length; i++)
                    bData[i] = 0;
                for (i = 0; i < sData.Length; i++)
                    sData[i] = "";
                for (i = 0; i < tData.Length; i++)
                    tData[i] = new DateTime(1999, 1, 1);
            }

            #region ByteData
            public bool loadbyHuman//0
            {
                get
                {
                    ushort v = Convert.ToUInt16(Row["B_CAR_CUSTOM"]);
                    return (v & 1) != 0;
                }
                set
                {
                    ushort v = Convert.ToUInt16(Row["B_CAR_CUSTOM"]);
                    if (value)
                        v |= 1;
                    else
                        v &= 0xfe;
                    bData[0] = (byte)v;
                    Row["B_CAR_CUSTOM"] = v;
                }
            }
            public bool loadbyMachine//0
            {
                get
                {
                    ushort v = Convert.ToUInt16(Row["B_CAR_CUSTOM"]);
                    return (v & 2) != 0;
                }
                set
                {
                    ushort v = Convert.ToUInt16(Row["B_CAR_CUSTOM"]);
                    if (value)
                        v |= 2;
                    else
                        v &= 0xfd;
                    bData[0] = (byte)v;
                    Row["B_CAR_CUSTOM"] = v;
                }
            }
            public bool IsYH5//0
            {
                get
                {
                    ushort v = Convert.ToUInt16(Row["B_CAR_CUSTOM"]);
                    return (v & 0x80) != 0;
                }
                set
                {
                    ushort v = Convert.ToUInt16(Row["B_CAR_CUSTOM"]);
                    if (value)
                        v |= 0x80;
                    else
                        v &= 0x7f;
                    bData[0] = (byte)v;
                    Row["B_CAR_CUSTOM"] = v;
                }
            }
            public bool loadCommon//0
            {
                get
                {
                    ushort v = Convert.ToUInt16(Row["B_CAR_CUSTOM"]);
                    return (v & 4) != 0;
                }
                set
                {
                    ushort v = Convert.ToUInt16(Row["B_CAR_CUSTOM"]);
                    if (value)
                        v |= 4;
                    else
                        v &= 0xfb;
                    bData[0] = (byte)v;
                    Row["B_CAR_CUSTOM"] = v;
                }
            }
            public byte plantype
            {
                get
                {
                    return Convert.ToByte(Row["E_PL_TYPE_FRT"]);
                }
                set
                {
                    bData[1] = value;
                    Row["E_PL_TYPE_FRT"] = value;
                }
            }
            public byte oiltype//2
            {
                get
                {
                    return Convert.ToByte(Row["E_OIL_TYPE"]);
                }
                set
                {
                    bData[2] = value;
                    Row["E_OIL_TYPE"] = value;
                }
            }
            public byte echara//3
            {
                get
                {
                    return Convert.ToByte(Row["E_CAR_CHARA"]);
                }
                set
                {
                    bData[3] = value;
                    Row["E_CAR_CHARA"] = value;
                }
            }
            public float length//4
            {
                get
                {
                    return Convert.ToByte(Row["F_LEN"]) / 10f;
                }
                set
                {
                    bData[4] = (byte)(value * 10 + .5);
                    Row["F_LEN"] = (byte)(value * 10 + .5);
                }
            }
            public byte state//5
            {
                get
                {
                    return Convert.ToByte(Row["B_CAR_STATE"]);
                }
                set
                {
                    bData[5] = value;
                    Row["B_CAR_STATE"] = value;
                }
            }
            public byte loadBatchNum//6
            {
                get
                {
                    return Convert.ToByte(Row["I_BATCH_NUM"]);
                }
                set
                {
                    bData[6] = value;
                    Row["I_BATCH_NUM"] = value;
                }
            }
            public byte loadWorkNum//7
            {
                get
                {
                    return Convert.ToByte(Row["I_WORK_NUM"]);
                }
                set
                {
                    bData[7] = value;
                    Row["I_WORK_NUM"] = value;
                }
            }
            #endregion

            #region ShortData
            public short number//0
            {
                get
                {
                    return Convert.ToInt16(Row["I_CAR_NUM_RATIFY"]);
                }
                set
                {
                    isData[0] = value;
                    Row["I_CAR_NUM_RATIFY"] = value;
                }
            }
            public short dir//1
            {
                get
                {
                    return Convert.ToInt16(Row["I_GRO_ID"]);
                }
                set
                {
                    isData[1] = value;
                    Row["I_GRO_ID"] = value;
                }
            }
            public short trackid//2
            {
                get
                {
                    return Convert.ToInt16(Row["I_LINE_ID"]);
                }
                set
                {
                    isData[2] = value;
                    Row["I_LINE_ID"] = value;
                }
            }
            public short areaid//3
            {
                get
                {
                    return Convert.ToInt16(Row["E_WORK_RANGE"]);
                }
                set
                {
                    isData[3] = value;
                    Row["E_WORK_RANGE"] = value;
                }
            }
            public float ownwegh//4
            {
                get
                {
                    return Convert.ToInt16(Row["F_WEGH_OWN"]) / 10f;
                }
                set
                {
                    isData[4] = (short)(value * 10 + .5);
                    Row["F_WEGH_OWN"] = (short)(value * 10 + .5);
                }
            }
            public float loadwegh//5
            {
                get
                {
                    return Convert.ToInt16(Row["F_WEGH_LOAD"]) / 10f;
                }
                set
                {
                    isData[5] = (short)(value * 10 + .5);
                    Row["F_WEGH_LOAD"] = (short)(value * 10 + .5);
                }
            }
            public short loaddedcount//6
            {
                get
                {
                    return Convert.ToInt16(Row["I_CAR_NUM_CLOAD"]);
                }
                set
                {
                    isData[6] = value;
                    Row["I_CAR_NUM_CLOAD"] = value;
                }
            }
            public short goodscount//7
            {
                get
                {
                    return Convert.ToInt16(Row["I_GOODS_NUM"]);
                }
                set
                {
                    isData[7] = value;
                    Row["I_GOODS_NUM"] = value;
                }
            }

            #endregion

            #region IntData
            public int id//0
            {
                get
                {
                    return Convert.ToInt32(Row["I_CAR_ID"]);
                }
                set
                {
                    iData[0] = value;
                    Row["I_CAR_ID"] = value;
                }
            }
            public uint carnum//1
            {
                get
                {
                    return Convert.ToUInt32(Row["I_CAR_CO"]);
                }
                set
                {
                    iData[1] = (int)value;
                    Row["I_CAR_CO"] = value;
                }
            }
            public int carid//2
            {
                get
                {
                    return id;
                }
                set
                {
                    id = value;
                }
            }
            public uint bchara//3
            {
                get
                {
                    return (uint)Convert.ToInt64(Row["B_CAR_CHARA"]);
                }
                set
                {
                    iData[3] = (int)value;
                    Row["B_CAR_CHARA"] = value;
                }
            }
            #endregion

            #region StringData
            public string goods//0
            {
                get
                {
                    return Convert.ToString(Row["C_GOODS_NA"]);
                }
                set
                {
                    sData[0] = value;
                    Row["C_GOODS_NA"] = value;
                }
            }
            public string station//1
            {
                get
                {
                    return Convert.ToString(Row["C_STA_NA_ARR"]);
                }
                set
                {
                    sData[1] = value;
                    Row["C_STA_NA_ARR"] = value;
                }
            }
            public string stationdep//2
            {
                get
                {
                    return Convert.ToString(Row["C_STA_NA_DEP"]);
                }
                set
                {
                    sData[2] = value;
                    Row["C_STA_NA_DEP"] = value;
                }
            }
            public string trainnumarr//3
            {
                get
                {
                    return Convert.ToString(Row["C_TRA_NUM_ARR"]);
                }
                set
                {
                    sData[3] = value;
                    Row["C_TRA_NUM_ARR"] = value;
                }
            }
            public string cartype//4
            {
                get
                {
                    return Convert.ToString(Row["C_CAR_TYPE"]);
                }
                set
                {
                    sData[4] = value;
                    Row["C_CAR_TYPE"] = value;
                }
            }
            public string note//5
            {
                get
                {
                    return Convert.ToString(Row["C_REMARK"]);
                }
                set
                {
                    sData[5] = value;
                    Row["C_REMARK"] = value;
                }
            }
            public string sender//6
            {
                get
                {
                    return Convert.ToString(Row["C_CONSIGNOR"]);
                }
                set
                {
                    sData[6] = value;
                    Row["C_CONSIGNOR"] = value;
                }
            }
            public string receiver//7
            {
                get
                {
                    return Convert.ToString(Row["C_CONSIGNEE"]);
                }
                set
                {
                    sData[7] = value;
                    Row["C_CONSIGNEE"] = value;
                }
            }
            public string cover1//8
            {
                get
                {
                    return Convert.ToString(Row["C_CANVAS_ID"]);
                }
                set
                {
                    sData[8] = value;
                    Row["C_CANVAS_ID"] = value;
                }
            }
            public string cover2//9
            {
                get
                {
                    return Convert.ToString(Row["C_CANVAS_ID1"]);
                }
                set
                {
                    sData[9] = value;
                    Row["C_CANVAS_ID1"] = value;
                }
            }
            public string cover3//10
            {
                get
                {
                    return Convert.ToString(Row["C_CANVAS_ID2"]);
                }
                set
                {
                    sData[10] = value;
                    Row["C_CANVAS_ID2"] = value;
                }
            }
            public string plannum//11
            {
                get
                {
                    return Convert.ToString(Row["C_PL_CO_RATIFY"]);
                }
                set
                {
                    sData[11] = value;
                    Row["C_PL_CO_RATIFY"] = value;
                }
            }
            public string loadMonitor//12
            {
                get
                {
                    return Convert.ToString(Row["C_OP_LOAD_CHECK"]);
                }
                set
                {
                    sData[12] = value;
                    Row["C_OP_LOAD_CHECK"] = value;
                }
            }
            public string boothIDs
            {
                get
                {
                    return Convert.ToString(Row["C_BOOTH_ID"]);
                }
                set
                {
                    sData[13] = value;
                    Row["C_BOOTH_ID"] = value;
                }
            }
            public string ticketnumber
            {
                get
                {
                    return Convert.ToString(Row["C_INV_CO"]);
                }
                set
                {
                    sData[14] = value;
                    Row["C_INV_CO"] = value;
                }
            }
            #endregion

            //public string[] covers = { "", "", "" };
            #region TimeData
            public DateTime startTime//0
            {
                get
                {
                    return Convert.ToDateTime(Row[""]);
                }
                set
                {
                    tData[0] = value;
                    Row[""] = value;
                }
            }
            public DateTime endTime//1
            {
                get
                {
                    return Convert.ToDateTime(Row[""]);
                }
                set
                {
                    tData[0] = value;
                    Row[""] = value;
                }
            }
            public DateTime arrTime//2
            {
                get
                {
                    return Convert.ToDateTime(Row[""]);
                }
                set
                {
                    tData[0] = value;
                    Row[""] = value;
                }
            }
            #endregion

            //public byte covcnt
            //{
            //    get
            //    {
            //        return (byte)covers.Length;
            //    }
            //    set
            //    {
            //        string[] cvs = new string[value];
            //        for (int i = 0; i < value; i++)
            //        {
            //            if (i < covers.Length)
            //                cvs[i] = covers[i];
            //            else
            //                cvs[i] = "";
            //        }
            //        covers = cvs;
            //    }
            //}
            //public bool Planed
            //{
            //    get
            //    {
            //        return (state & 1) != 0;
            //    }
            //    set
            //    {
            //        if (value)
            //            state |= 1;
            //        else
            //            state &= 0xfe;
            //    }
            //}
            //public bool Loaded
            //{
            //    get
            //    {
            //        return (state & 2) != 0;
            //    }
            //    set
            //    {
            //        if (value)
            //            state |= 2;
            //        else
            //            state &= 0xfd;
            //    }
            //}
            public string CarNum
            {
                get
                {
                    return CAR.CARNUM(carnum);
                    //return ((int)(carnum & 0xffffff)).ToString("0000000");
                }
                set
                {
                    int i;
                    string s = "";
                    foreach (char c in value)
                    {
                        if (c >= '0' && c <= '9')
                            s += c.ToString();
                    }
                    if (s.Length == 0)
                        i = 0;
                    else
                        i = Convert.ToInt32(s);
                    carnum &= 0x7f000000;
                    carnum |= (uint)(i & 0xffffff);
                    if (s.Length < 7)
                    {
                        carnum |= 0x80000000;
                        carnum |= (uint)((s.Length) << 21);
                    }
                }
            }
            public short ver
            {
                get
                {
                    return CAR.CARVER(carnum);
                }
                set
                {
                    carnum &= 0x80ffffff;
                    carnum |= (uint)((value & 0x7f) << 24);
                }
            }
            //public string CarNum
            //{
            //    get
            //    {
            //        return ((int)(carnum & 0xffffff)).ToString("0000000");
            //    }
            //    set
            //    {
            //        uint cn = 0;
            //        try
            //        {
            //            cn = Convert.ToUInt32(value);
            //        }
            //        catch
            //        {
            //        }
            //        carnum &= 0xffffff;
            //        carnum |= cn;
            //    }
            //}
            //public int ver
            //{
            //    get
            //    {
            //        return (int)((carnum >> 24) & 0xff);
            //    }
            //    set
            //    {
            //        uint v = (uint)value;
            //        v = (v << 24) & 0xff000000;
            //        carnum |= v;
            //    }
            //}
            public override string ToString()
            {
                if (IsYH5)
                    return plannum + " " + cartype;
                else
                    return CarNum + " " + cartype;
            }
            public void CopyFrom(TICKET t)
            {
                int i;
                bData = new byte[t.bData.Length];
                for (i = 0; i < bData.Length; i++)
                    bData[i] = t.bData[i];
                isData = new short[t.isData.Length];
                for (i = 0; i < isData.Length; i++)
                    isData[i] = t.isData[i];
                iData = new int[t.iData.Length];
                for (i = 0; i < iData.Length; i++)
                    iData[i] = t.iData[i];
                tData = new DateTime[t.tData.Length];
                for (i = 0; i < tData.Length; i++)
                    tData[i] = t.tData[i];
                sData = new string[t.sData.Length];
                for (i = 0; i < sData.Length; i++)
                    sData[i] = t.sData[i];
            }
            public bool Equal(TICKET t)
            {
                int i;
                for (i = 0; i < bData.Length; i++)
                {
                    if (bData[i] != t.bData[i])
                        return false;
                }
                for (i = 0; i < isData.Length; i++)
                {
                    if (isData[i] != t.isData[i])
                        return false;
                }
                for (i = 0; i < iData.Length; i++)
                {
                    if (iData[i] != t.iData[i])
                        return false;
                }
                for (i = 0; i < tData.Length; i++)
                {
                    if (tData[i] != t.tData[i])
                        return false;
                }
                for (i = 0; i < sData.Length; i++)
                {
                    if (sData[i] != t.sData[i])
                        return false;
                }
                return true;
            }
        }

        const int off_cmd = 0;
        const int off_datatype = off_cmd + 1;
        const int off_sum = off_datatype + 1;
        const int off_packagehead = off_sum + 2;
        const int off_id = 0;
        const int off_number = off_id + 4;
        const int off_carid = off_number + 2;
        const int off_carnum = off_carid + 4;
        const int off_echara = off_carnum + 4;
        const int off_bchara = off_echara + 1;
        const int off_tid = off_bchara + 4;
        const int off_area = off_tid + 2;
        const int off_state = off_area + 2;
        const int off_starttime = off_state + 1;
        const int off_endtime = off_starttime + 6;
        const int off_arrtime= off_endtime + 6;
        const int off_ownwegh = off_arrtime + 6;
        const int off_loadwegh = off_ownwegh + 1;
        const int off_oiltype = off_loadwegh + 1;
        const int off_length = off_oiltype + 1;
        const int off_dir = off_length + 1;
        const int off_covcnt = off_dir + 2;
        const int off_item = off_covcnt + 1;
        public static byte[] ToBytes(TICKET[] ts, byte mode)
        {
            int l;
            l = off_packagehead;
            foreach (TICKET t in ts)
            {
                l += 1 + t.bData.Length;
                l += 1 + t.isData.Length * 2;
                l += 1 + t.iData.Length * 4;
                l += 1 + t.tData.Length * 6;
                l++;
                foreach (string s in t.sData)
                    l += System.Text.Encoding.Default.GetByteCount(s) + 1;
            }
            byte[] b = new byte[l];
            b[off_datatype] = mode;
            BitConverter.GetBytes((short)ts.Length).CopyTo(b, off_sum);
            l = off_packagehead;
            foreach (TICKET t in ts)
            {
                b[l] = (byte)t.bData.Length;
                l++;
                t.bData.CopyTo(b, l);
                l += t.bData.Length;
                b[l] = (byte)t.isData.Length;
                l++;
                foreach (short a in t.isData)
                {
                    BitConverter.GetBytes(a).CopyTo(b, l);
                    l += 2;
                }
                b[l] = (byte)t.iData.Length;
                l++;
                foreach (int a in t.iData)
                {
                    BitConverter.GetBytes(a).CopyTo(b, l);
                    l += 4;
                }
                b[l] = (byte)t.tData.Length;
                l++;
                foreach (DateTime a in t.tData)
                {
                    Tools.Time2Byte(a).CopyTo(b, l);
                    l += 6;
                }
                b[l] = (byte)t.sData.Length;
                l++;
                foreach (string a in t.sData)
                {
                    Tools.String2Byte(a, b, ref l);
                }
            }
            return b;
        }
        public static TICKET[] FromBytes(byte[] b)
        {
            try
            {
                TICKET[] ts = new TICKET[BitConverter.ToInt16(b, off_sum)];
                int i, j, l;
                l = off_packagehead;
                for (i = 0; i < ts.Length; i++)
                {
                    ts[i] = new TICKET();
                    ts[i].bData = new byte[b[l]];
                    l++;
                    for (j = 0; j < ts[i].bData.Length; j++)
                    {
                        ts[i].bData[j] = b[l];
                        l++;
                    }
                    ts[i].isData = new short[b[l]];
                    l++;
                    for (j = 0; j < ts[i].isData.Length; j++)
                    {
                        ts[i].isData[j] = BitConverter.ToInt16(b, l);
                        l += 2;
                    }
                    ts[i].iData = new int[b[l]];
                    l++;
                    for (j = 0; j < ts[i].iData.Length; j++)
                    {
                        ts[i].iData[j] = BitConverter.ToInt32(b, l);
                        l += 4;
                    }
                    ts[i].tData = new DateTime[b[l]];
                    l++;
                    for (j = 0; j < ts[i].tData.Length; j++)
                    {
                        ts[i].tData[j] = Tools.Byte2Time(b, l);
                        l += 6;
                    }
                    ts[i].sData = new string[b[l]];
                    l++;
                    for (j = 0; j < ts[i].sData.Length; j++)
                    {
                        ts[i].sData[j] = Tools.Byte2String(b, ref l);
                    }
                }
                return ts;
            }
            catch
            {
                return new TICKET[0];
            }
        }
        /*
        public static byte[] ToBytes(TICKET[] ts, byte mode)
        {
            int i, l;
            l = off_packagehead;
            foreach(TICKET t in ts)
            {
                l += off_item;
                l += System.Text.Encoding.Default.GetByteCount(t.goods) + 1;
                l += System.Text.Encoding.Default.GetByteCount(t.station) + 1;
                l += System.Text.Encoding.Default.GetByteCount(t.cartype) + 1;
                l += System.Text.Encoding.Default.GetByteCount(t.note) + 1;
                l += System.Text.Encoding.Default.GetByteCount(t.sender) + 1;
                l += System.Text.Encoding.Default.GetByteCount(t.receiver) + 1;
                l += System.Text.Encoding.Default.GetByteCount(t.stationdep) + 1;
                l += System.Text.Encoding.Default.GetByteCount(t.trainnumarr) + 1;
                for (i = 0; i < t.covcnt; i++)
                    l += System.Text.Encoding.Default.GetByteCount(t.covers[i]) + 1;
            }
            byte[] b = new byte[l];
            b[off_datatype] = mode;
            BitConverter.GetBytes((short)ts.Length).CopyTo(b, off_sum);
            l = off_packagehead;
            foreach (TICKET t in ts)
            {
                BitConverter.GetBytes(t.id).CopyTo(b, l + off_id);
                BitConverter.GetBytes(t.number).CopyTo(b, l + off_number);
                BitConverter.GetBytes(t.carid).CopyTo(b, l + off_carid);
                BitConverter.GetBytes(t.carnum).CopyTo(b, l + off_carnum);
                b[l + off_echara] = t.echara;
                BitConverter.GetBytes(t.bchara).CopyTo(b, l + off_bchara);
                BitConverter.GetBytes(t.trackid).CopyTo(b, l + off_tid);
                BitConverter.GetBytes(t.areaid).CopyTo(b, l + off_area);
                b[l + off_state] = t.state;
                Tools.Time2Byte(t.startTime).CopyTo(b, l + off_starttime);
                Tools.Time2Byte(t.endTime).CopyTo(b, l + off_endtime);
                Tools.Time2Byte(t.arrTime).CopyTo(b, l + off_arrtime);
                b[l + off_ownwegh] = t.ownwegh;
                b[l + off_loadwegh] = t.loadwegh;
                b[l + off_oiltype] = t.oiltype;
                b[l + off_length] = (byte)(t.length * 10);
                BitConverter.GetBytes(t.dir).CopyTo(b, l + off_dir);
                b[l + off_covcnt] = t.covcnt;
                l += off_item;
                Tools.String2Byte(t.goods, b, ref l);
                Tools.String2Byte(t.station, b, ref l);
                Tools.String2Byte(t.cartype, b, ref l);
                Tools.String2Byte(t.note, b, ref l);
                Tools.String2Byte(t.sender, b, ref l);
                Tools.String2Byte(t.receiver, b, ref l);
                Tools.String2Byte(t.stationdep, b, ref l);
                Tools.String2Byte(t.trainnumarr, b, ref l);
                for (i = 0; i < t.covcnt; i++)
                    Tools.String2Byte(t.covers[i], b, ref l);
            }
            return b;
        }
        public static TICKET[] FromBytes(byte[] b)
        {
            TICKET[] ts = new TICKET[BitConverter.ToInt16(b, off_sum)];
            int i, j, l;
            l = off_packagehead;
            for (i = 0; i < ts.Length; i++)
            {
                ts[i] = new TICKET();
                ts[i].id = BitConverter.ToInt32(b, l + off_id);
                ts[i].number = BitConverter.ToInt16(b, l + off_number);
                ts[i].carid = BitConverter.ToInt32(b, l + off_carid);
                ts[i].carnum = BitConverter.ToUInt32(b, l + off_carnum);
                ts[i].echara = b[l + off_echara];
                ts[i].bchara = BitConverter.ToUInt32(b, l + off_bchara);
                ts[i].trackid = BitConverter.ToInt16(b, l + off_tid);
                ts[i].areaid = BitConverter.ToInt16(b, l + off_area);
                ts[i].state = b[l + off_state];
                ts[i].startTime = Tools.Byte2Time(b, l + off_starttime);
                ts[i].endTime = Tools.Byte2Time(b, l + off_endtime);
                ts[i].arrTime = Tools.Byte2Time(b, l + off_arrtime);
                ts[i].ownwegh = b[l + off_ownwegh];
                ts[i].loadwegh = b[l + off_loadwegh];
                ts[i].oiltype = b[l + off_oiltype];
                ts[i].length = b[l + off_length] / 10f;
                ts[i].dir = BitConverter.ToInt16(b, l + off_dir);
                ts[i].covcnt = b[l + off_covcnt];
                l += off_item;
                ts[i].goods = Tools.Byte2String(b, ref l);
                ts[i].station = Tools.Byte2String(b, ref l);
                ts[i].cartype = Tools.Byte2String(b, ref l);
                ts[i].note = Tools.Byte2String(b, ref l);
                ts[i].sender = Tools.Byte2String(b, ref l);
                ts[i].receiver = Tools.Byte2String(b, ref l);
                ts[i].stationdep = Tools.Byte2String(b, ref l);
                ts[i].trainnumarr = Tools.Byte2String(b, ref l);
                for(j=0;j<ts[i].covcnt;j++)
                    ts[i].covers[j] = Tools.Byte2String(b, ref l);
            }
            return ts;
        }
        */
    }
}
