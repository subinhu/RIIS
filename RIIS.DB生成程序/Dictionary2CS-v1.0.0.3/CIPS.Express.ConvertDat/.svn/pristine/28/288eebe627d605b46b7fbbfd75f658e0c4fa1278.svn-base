using System;
using System.Data;
using System.Collections.Generic;

namespace CIPS.Express.ConvertDat
{
	/// <summary>
	/// 确报目录集合
	/// </summary>
	public class ConsList
	{
        /// <summary>
        /// TDCS模式
        /// </summary>
        public byte TdcsMode = 0;
        /// <summary>
        /// 确报目录数组
        /// </summary>
        public Cons[] cons = new Cons[0];
        /// <summary>
        /// 确报目录
        /// </summary>
        public class Cons
        {
            /// <summary>
            /// 车次
            /// </summary>
            public string trainnum = "";
            /// <summary>
            /// 确报ID
            /// </summary>
            public uint consid = 0;
            /// <summary>
            /// 顺序
            /// </summary>
            public short no = 0;
            /// <summary>
            /// 状态
            /// </summary>
            public uint state = 0;
            /// <summary>
            /// 线路
            /// </summary>
            public short tid;
            /// <summary>
            /// 时刻
            /// </summary>
            public DateTime time;
            /// <summary>
            /// 辆数
            /// </summary>
            public short carcount = 0;
            /// <summary>
            /// 换长
            /// </summary>
            public float length = 0;
            /// <summary>
            /// 总重
            /// </summary>
            public short weight = 0;
            /// <summary>
            /// 机车
            /// </summary>
            public short locom = 0;
            /// <summary>
            /// B_CONS_STATE
            /// </summary>
            public uint b_state = 0;
            /// <summary>
            /// 到站
            /// </summary>
            public string station = "";
            /// <summary>
            /// 到站ID
            /// </summary>
            public short stationID = 0;
            /// <summary>
            /// 非列车
            /// </summary>
            public bool NonTrain
            {
                get
                {
                    return (b_state & 0x80000000) != 0;
                }
                set
                {
                    if (value)
                        b_state |= 0x80000000;
                    else
                        b_state &= 0x7fffffff;
                }
            }
            /// <summary>
            /// 重载ToString
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return trainnum;
            }
        }
        /// <summary>
        /// 确报集合元素
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Cons this[int i]
        {
            get
            {
                if (i >= 0 && i < cons.Length)
                    return cons[i];
                return null;
            }
        }
        /// <summary>
        /// 元素数
        /// </summary>
        public int Length
        {
            get
            {
                return cons.Length;
            }
        }
	}
    /// <summary>
    /// 确报目录序列化
    /// </summary>
	public class ConvertConsListDat
	{
		const int off_cmd=0;
		const int off_datatype=off_cmd+1;
        const int off_tdcs = off_datatype + 1;
        const int off_packagehead = off_tdcs + 1;//头总长
        //const int off_sum = off_tdcs + 1;
        const int off_sum = 0;
        const int off_conshead = off_sum + 2;	//头长
		const int off_consid=0;				//
		const int off_tn=off_consid+4;		//
		const int off_state=off_tn+10;		//
		const int off_no=off_state+4;		//
		const int off_time=off_no+2;		//
        const int off_tid = off_time + 6;
        const int off_ccnt = off_tid + 2;
        const int off_len = off_ccnt + 2;
        const int off_wegh = off_len + 2;
        const int off_locom = off_wegh + 2;
        const int off_bstate = off_locom + 2;
        const int off_stationid = off_bstate + 4;
        const int off_station = off_stationid + 2;
        const int off_nouse = off_station + 8;
        const int off_item = off_nouse + 10;		//
        /// <summary>
        /// 序列化（过期）
        /// </summary>
        /// <param name="cons"></param>
        /// <param name="datatype"></param>
        /// <param name="TdcsMode"></param>
        /// <returns></returns>
        public static byte[] Struct2Buf(List<ConsList.Cons> cons, int datatype, byte TdcsMode)
        {
            short cnt = (short)cons.Count;
            byte[] buf = new byte[off_packagehead + off_conshead + cnt * off_item];
            if (datatype == 0)
                buf[off_datatype] = Command.DataType_ArrConsList;
            else
                buf[off_datatype] = Command.DataType_DepConsList;
            buf[off_tdcs] = TdcsMode;
            int i;
            int l = off_packagehead;
            BitConverter.GetBytes(cnt).CopyTo(buf, l + off_sum);
            l += off_conshead;
            for (i = 0; i < cnt; i++)
            {
                Tools.String2Byte(cons[i].trainnum, 10).CopyTo(buf, l + off_tn);
                BitConverter.GetBytes(cons[i].consid).CopyTo(buf, l + off_consid);
                BitConverter.GetBytes(cons[i].state).CopyTo(buf, l + off_state);
                Tools.Time2Byte(cons[i].time).CopyTo(buf, l + off_time);
                BitConverter.GetBytes((short)(i + 1)).CopyTo(buf, l + off_no);
                BitConverter.GetBytes(cons[i].tid).CopyTo(buf, l + off_tid);
                BitConverter.GetBytes(cons[i].carcount).CopyTo(buf, l + off_ccnt);
                BitConverter.GetBytes((short)(cons[i].length * 10 + .5)).CopyTo(buf, l + off_len);
                BitConverter.GetBytes(cons[i].weight).CopyTo(buf, l + off_wegh);
                BitConverter.GetBytes(cons[i].b_state).CopyTo(buf, l + off_bstate);
                BitConverter.GetBytes(cons[i].stationID).CopyTo(buf, l + off_stationid);
                Tools.String2Byte(cons[i].station, 8).CopyTo(buf, l + off_station);
                l += off_item;
            }
            return buf;
        }

        /// <summary>
        /// 表序列化（过期）
        /// </summary>
        /// <param name="drs"></param>
        /// <param name="datatype"></param>
        /// <param name="TdcsMode"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static byte[] Table2Buf(DataRow[] drs, int datatype, byte TdcsMode, DataSet ds)
        {
            string[] tn ={ "C_TRA_NUM_ARR", "C_TRA_NUM_DEP" };
            string[] id ={ "I_CONS_ID", "I_CONS_ID" };
            string[] st ={ "E_ST_CONS", "E_ST_DEPCONS" };
            string[] tm ={ "D_TRA_DEP", "D_TRA_DEP" };
            string[] lid ={ "I_LINE_ID", "I_LINE_ID" };
            string[] ccnt ={ "I_CAR_NUM", "I_CAR_NUM" };
            string[] len ={ "F_LEN_TRA", "F_LEN_TRA" };
            string[] weig ={ "F_WEGH_TRA", "F_WEGH_TRA" };
            string[] sta ={ "C_STA_CO_ARR", "C_STA_CO_DEP" };
            short cnt = (short)drs.Length;
            byte[] buf = new byte[off_packagehead + off_conshead + cnt * off_item];
            buf[off_tdcs] = TdcsMode;
            if (datatype == 0)
                buf[off_datatype] = Command.DataType_ArrConsList;
            else
                buf[off_datatype] = Command.DataType_DepConsList;
            int i;
            int l = off_packagehead;
            BitConverter.GetBytes(cnt).CopyTo(buf, l + off_sum);
            l += off_conshead;
            for (i = 0; i < cnt; i++)
            {
                Tools.String2Byte(drs[i][tn[datatype]].ToString().Trim(), 10).CopyTo(buf, l + off_tn);
                BitConverter.GetBytes(Convert.ToUInt32(drs[i][id[datatype]])).CopyTo(buf, l + off_consid);
                BitConverter.GetBytes(Convert.ToUInt32(drs[i][st[datatype]])).CopyTo(buf, l + off_state);
                Tools.Time2Byte(Convert.ToDateTime(drs[i][tm[datatype]])).CopyTo(buf, l + off_time);
                BitConverter.GetBytes((short)(i + 1)).CopyTo(buf, l + off_no);
                BitConverter.GetBytes(Convert.ToInt16(drs[i][lid[datatype]])).CopyTo(buf, l + off_tid);
                BitConverter.GetBytes(Convert.ToInt16(drs[i][ccnt[datatype]])).CopyTo(buf, l + off_ccnt);
                BitConverter.GetBytes((short)(Convert.ToSingle(drs[i][len[datatype]]) * 10)).CopyTo(buf, l + off_len);
                BitConverter.GetBytes((short)(Convert.ToSingle(drs[i][weig[datatype]]) + .5)).CopyTo(buf, l + off_wegh);
                BitConverter.GetBytes(Convert.ToUInt32(drs[i]["B_SYM_CHECK"])).CopyTo(buf, l + off_bstate);
                DataRow[] _drs;
                _drs = ds.Tables["DC_RA_YARD"].Select("C_RANGE_FORSH = '" + drs[i][sta[datatype]].ToString().Trim() + "' and E_RANGE_LEVEL = " + CIPS.DB.E_RANGE_LEVEL.STATION.ToString());
                if (_drs.Length > 0)
                    BitConverter.GetBytes(Convert.ToInt16(_drs[0]["I_RANGE_ID"])).CopyTo(buf, l + off_stationid);
                _drs = ds.Tables["DC_TD_STA"].Select("C_STA_CO = '" + drs[i][sta[datatype]].ToString().Trim() + "'");
                if (_drs.Length > 0)
                {
                    _drs = ds.Tables["DC_TD_GRO"].Select("I_GRO_ID =" + _drs[0]["I_GRO_ID"].ToString().Trim());
                    if (_drs.Length > 0)
                        Tools.String2Byte(_drs[0]["C_GRO_FORSH"].ToString().Trim(), 8).CopyTo(buf, l + off_station);
                }
                l += off_item;
            }
            return buf;
        }
        /// <summary>
        /// 表-结构（过期）
        /// </summary>
        /// <param name="drs"></param>
        /// <param name="datatype"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<ConsList.Cons> Table2Struct(DataRow[] drs, int datatype, DataSet ds)
        {
            string[] tn ={ "C_TRA_NUM_ARR", "C_TRA_NUM_DEP" };
            string[] id ={ "I_CONS_ID", "I_CONS_ID" };
            string[] st ={ "E_ST_CONS", "E_ST_DEPCONS" };
            string[] tm ={ "D_TRA_DEP", "D_TRA_DEP" };
            string[] lid ={ "I_LINE_ID", "I_LINE_ID" };
            string[] ccnt ={ "I_CAR_NUM", "I_CAR_NUM" };
            string[] len ={ "F_LEN_TRA", "F_LEN_TRA" };
            string[] weig ={ "F_WEGH_TRA", "F_WEGH_TRA" };
            string[] sta ={ "C_STA_CO_ARR", "C_STA_CO_DEP" };
            short cnt = (short)drs.Length;
            int i;
            List<ConsList.Cons> cons = new List<ConsList.Cons>();
            ConsList.Cons c;
            for (i = 0; i < cnt; i++)
            {
                c = new ConsList.Cons();

                c.trainnum = drs[i][tn[datatype]].ToString().Trim();
                c.consid = Convert.ToUInt32(drs[i][id[datatype]]);
                c.state = Convert.ToUInt32(drs[i][st[datatype]]);
                c.time = Convert.ToDateTime(drs[i][tm[datatype]]);
                c.tid = Convert.ToInt16(drs[i][lid[datatype]]);
                c.carcount = Convert.ToInt16(drs[i][ccnt[datatype]]);
                c.length = Convert.ToSingle(drs[i][len[datatype]]);
                c.weight = (short)(Convert.ToSingle(drs[i][weig[datatype]]) + .5);
                c.b_state = Convert.ToUInt32(drs[i]["B_SYM_CHECK"]);
                DataRow[] _drs = ds.Tables["DC_TD_STA"].Select("C_STA_CO = '" + drs[i][sta[datatype]].ToString().Trim() + "'");
                if (_drs.Length > 0)
                {
                    _drs = ds.Tables["DC_TD_GRO"].Select("I_GRO_ID =" + _drs[0]["I_GRO_ID"].ToString().Trim());
                    if (_drs.Length > 0)
                        c.station = _drs[0]["C_GRO_FORSH"].ToString().Trim();
                }
                _drs = ds.Tables["DC_RA_YARD"].Select("C_RANGE_FORSH = '" + drs[i][sta[datatype]].ToString().Trim() + "' and E_RANGE_LEVEL = " + CIPS.DB.E_RANGE_LEVEL.STATION.ToString());
                if (_drs.Length > 0)
                    c.stationID = Convert.ToInt16(_drs[0]["I_RANGE_ID"]);
                cons.Add(c);
            }
            return cons;
        }
        /// <summary>
        /// 反序列化（过期）
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static ConsList Buf2Struct(byte[] buf)
		{
            ConsList list = new ConsList();
            try
            {
                int l = off_packagehead;
                list.TdcsMode = buf[off_tdcs];
                //list.TdcsMode = buf[0];
                int cnt = BitConverter.ToInt16(buf, l + off_sum);
                int i;
                list.cons = new ConsList.Cons[cnt];
                l += off_conshead;
                for (i = 0; i < cnt; i++)
                {
                    list.cons[i] = new ConsList.Cons();
                    list.cons[i].consid = BitConverter.ToUInt32(buf, l + off_consid);
                    list.cons[i].state = BitConverter.ToUInt32(buf, l + off_state);
                    list.cons[i].no = BitConverter.ToInt16(buf, l + off_no);
                    list.cons[i].trainnum = System.Text.Encoding.Default.GetString(buf, l + off_tn, 10).Trim('\0').Trim();
                    list.cons[i].time = Tools.Byte2Time(buf, l + off_time);
                    list.cons[i].tid = BitConverter.ToInt16(buf, l + off_tid);
                    list.cons[i].carcount = BitConverter.ToInt16(buf, l + off_ccnt);
                    list.cons[i].length = BitConverter.ToInt16(buf, l + off_len) / 10f;
                    list.cons[i].weight = BitConverter.ToInt16(buf, l + off_wegh);
                    list.cons[i].b_state = BitConverter.ToUInt32(buf, l + off_bstate);
                    list.cons[i].stationID = BitConverter.ToInt16(buf, l + off_stationid);
                    list.cons[i].station = System.Text.Encoding.Default.GetString(buf, l + off_station, 8).Trim('\0').Trim();
                    l += off_item;
                }
            }
            catch { }
			return list;
        }



        #region 新广播
        static ClTable CreateTable(string tablename)
        {
            ClTable ct = new ClTable(tablename);
            ct.AddColumn("I_CONS_ID", typeof(int));
            ct.AddColumn("C_TRA_NUM", typeof(string));
            ct.AddColumn("E_ST_CONS", typeof(byte));
            ct.AddColumn("B_SYM_CHECK", typeof(uint));
            ct.AddColumn("D_TRA", typeof(DateTime));
            ct.AddColumn("I_LINE_ID_ARRDEP", typeof(short));
            ct.AddColumn("I_CONS_ORDER", typeof(short));
            ct.AddColumn("I_CAR_NUM", typeof(byte));
            ct.AddColumn("F_LEN_TRA", typeof(float));
            ct.AddColumn("F_WEGH_TRA", typeof(float));
            ct.AddColumn("C_STA_CO", typeof(string));
            ct.AddColumn("C_LOCOM_CO1", typeof(string));
            ct.AddColumn("C_LOCOM_CO2", typeof(string));
            ct.AddColumn("I_STATION_ID", typeof(short));
            return ct;
        }
        static ClTable CreateInfoTable(byte tdcsmode)
        {
            ClTable ct = new ClTable("CONSINFO");
            ct.AddColumn("C_NAME", typeof(string));
            ct.AddColumn("C_VALUE", typeof(string));
            ClRow cr = ct.NewRow();
            cr["C_NAME"] = "TDCSMODE";
            cr["C_VALUE"] = tdcsmode.ToString();
            ct.Rows.Add(cr);
            return ct;
        }
        /// <summary>
        /// 表-》结构
        /// </summary>
        /// <param name="ct">确报目录表</param>
        /// <param name="ctinfo">含有TDCSMODE等内容的表</param>
        /// <returns></returns>
        public static ConsList ClTable2ConsList(ClTable ct,ClTable ctinfo)
        {
            ConsList list = new ConsList();
            try
            {
                list.TdcsMode = Convert.ToByte(ctinfo.Rows[0]["C_VALUE"]);
                list.cons = new ConsList.Cons[ct.Rows.Count];
                ClRow cr;
                int i_I_CONS_ID = ct.GetColumnIndex("I_CONS_ID");
                int i_E_ST_CONS = ct.GetColumnIndex("E_ST_CONS");
                int i_I_CONS_ORDER = ct.GetColumnIndex("I_CONS_ORDER");
                int i_C_TRA_NUM = ct.GetColumnIndex("C_TRA_NUM");
                int i_D_TRA = ct.GetColumnIndex("D_TRA");
                int i_I_LINE_ID_ARRDEP = ct.GetColumnIndex("I_LINE_ID_ARRDEP");
                int i_I_CAR_NUM = ct.GetColumnIndex("I_CAR_NUM");
                int i_F_LEN_TRA = ct.GetColumnIndex("F_LEN_TRA");
                int i_F_WEGH_TRA = ct.GetColumnIndex("F_WEGH_TRA");
                int i_B_SYM_CHECK = ct.GetColumnIndex("B_SYM_CHECK");
                int i_I_STATION_ID = ct.GetColumnIndex("I_STATION_ID");
                int i_C_STA_CO = ct.GetColumnIndex("C_STA_CO");
                for (int i = 0; i < list.cons.Length; i++)
                {
                    cr = ct.Rows[i];
                    list.cons[i] = new ConsList.Cons();
                    list.cons[i].consid = Convert.ToUInt32(cr[i_I_CONS_ID]);
                    list.cons[i].state = Convert.ToUInt32(cr[i_E_ST_CONS]);
                    list.cons[i].no = Convert.ToInt16(cr[i_I_CONS_ORDER]);
                    list.cons[i].trainnum = cr[i_C_TRA_NUM].ToString();
                    list.cons[i].time = Convert.ToDateTime(cr[i_D_TRA]);
                    list.cons[i].tid = Convert.ToInt16(cr[i_I_LINE_ID_ARRDEP]);
                    list.cons[i].carcount = Convert.ToInt16(cr[i_I_CAR_NUM]);
                    list.cons[i].length = Convert.ToSingle(cr[i_F_LEN_TRA]);
                    list.cons[i].weight = (short)(Convert.ToSingle(cr[i_F_WEGH_TRA]) + .5);
                    list.cons[i].b_state = Convert.ToUInt32(cr[i_B_SYM_CHECK]);
                    list.cons[i].stationID = Convert.ToInt16(cr[i_I_STATION_ID]);
                    list.cons[i].station = Convert.ToString(cr[i_C_STA_CO]);
                }
            }
            catch { }
            return list;
        }
        #region 列车表 for cips
        static byte GetArrConsST(DataRow dr,DataTable dtTraText,out int YardId)
        {
            YardId = 0;
            int E_CONS_STATE = Convert.ToInt32(dr["E_CONS_STATE"]);
            int B_SYM_CHECK = Convert.ToInt32(dr["B_SYM_CHECK"]);
            int E_PL_STATE = Convert.ToInt32(dr["E_PL_STATE"]);
            DateTime D_FA_NOTIFY = new DateTime(1900,1,1,12,0,0);
            if (dtTraText == null)
                D_FA_NOTIFY = Convert.ToDateTime(dr["D_FA_NOTIFY"]);
            else
            {
                DataRow[] drs = dtTraText.Select("I_TRA_ID =" + Convert.ToInt32(dr["I_TRA_ID"]),"I_TRA_IN_SEQ");
                if (drs.Length > 0)
                {
                    if ((int)dr["E_SYM_ARRDEP"] == CIPS.DB.E_SYM_ARRDEP.END_ARR)
                    {
                        D_FA_NOTIFY = Convert.ToDateTime(drs[drs.Length - 1]["D_FA_NOTIFY_ARR"]);
                        YardId = Convert.ToInt32(drs[drs.Length - 1]["I_YARD_ID"]);
                    }
                    else
                        D_FA_NOTIFY = Convert.ToDateTime(drs[0]["D_FA_NOTIFY_DEP"]);
                }
                else
                    D_FA_NOTIFY = Convert.ToDateTime(dr["D_FA_NOTIFY"]);
            }
            byte st = 0;
            if (E_CONS_STATE <= CIPS.DB.E_CONS_STATE.CONS)
            {
                //1.计划 (没有报点＋报文状态：初始值，车流，自动配报)
                if (D_FA_NOTIFY.Year < 2000)
                    st = CIPS.DB.E_ST_CONS.PLAN;
                //2.报点无报 (报点＋报文状态：初始值，车流，自动配报)
                else
                    st = CIPS.DB.E_ST_CONS.NOCONS_NOTIFY;
            }
            if (E_CONS_STATE == CIPS.DB.E_CONS_STATE.MANUAL)
            {
                //3.有报无报点　(无报点＋报文状态：人工配报)
                if (D_FA_NOTIFY.Year < 2000)
                    st = CIPS.DB.E_ST_CONS.CONS_NONOTIFY;
                //4.有报报点　(报点＋报文状态：人工配报)
                else
                    st = CIPS.DB.E_ST_CONS.CONS_NOTIFY;
            }
            //5.车号核对　(核准比特码：车号核对)
            if ((B_SYM_CHECK & CIPS.DB.B_SYM_CHECK.MANUAL) == CIPS.DB.B_SYM_CHECK.MANUAL)
                st = CIPS.DB.E_ST_CONS.CAR_NOCHECK; ;
            //6.有列无商　(核准比特码：列检结束＋无商检结束)
            if (((B_SYM_CHECK & CIPS.DB.B_SYM_CHECK.TRA) == CIPS.DB.B_SYM_CHECK.TRA) && ((B_SYM_CHECK & CIPS.DB.B_SYM_CHECK.GOODS) != CIPS.DB.B_SYM_CHECK.GOODS))
                st = CIPS.DB.E_ST_CONS.CAR_TRA_NOGOODS;
            //7.有列有商　(核准比特码：列检结束＋商检结束)
            if (((B_SYM_CHECK & CIPS.DB.B_SYM_CHECK.TRA) == CIPS.DB.B_SYM_CHECK.TRA) && ((B_SYM_CHECK & CIPS.DB.B_SYM_CHECK.GOODS) == CIPS.DB.B_SYM_CHECK.GOODS))
                st = CIPS.DB.E_ST_CONS.CAR_TRA_GOODS;
            //8.无列有商　(核准比特码：列检结束＋商检结束)
            if (((B_SYM_CHECK & CIPS.DB.B_SYM_CHECK.TRA) != CIPS.DB.B_SYM_CHECK.TRA) && ((B_SYM_CHECK & CIPS.DB.B_SYM_CHECK.GOODS) == CIPS.DB.B_SYM_CHECK.GOODS))
                st = CIPS.DB.E_ST_CONS.CAR_NOTRA_GOODS;
            //9.调度核准　(核准比特码：调度核准　+ 报文状态：确报核准)
            if ((B_SYM_CHECK & CIPS.DB.B_SYM_CHECK.CONTROL) == CIPS.DB.B_SYM_CHECK.CONTROL)
                st = CIPS.DB.E_ST_CONS.CAR_CHECK;
            //结束　计划状态：结束
            if (E_PL_STATE == CIPS.DB.E_PL_STATE.END)
                st = CIPS.DB.E_ST_CONS.CAR_END;
            //已交班
            if (E_PL_STATE == CIPS.DB.E_PL_STATE.SHIFT)
                st = CIPS.DB.E_ST_CONS.SHIFT;
            return st;
        }
        static byte GetDepConsST(DataRow dr,DataTable dtTraText,out int YardId)
        {
            YardId = 0;
            int E_CONS_STATE = Convert.ToInt32(dr["E_CONS_STATE"]);
            int B_SYM_CHECK = Convert.ToInt32(dr["B_SYM_CHECK"]);
            int E_PL_STATE = Convert.ToInt32(dr["E_PL_STATE"]);
            byte st = 0;
            DataRow[] drs = dtTraText.Select("I_TRA_ID =" + Convert.ToInt32(dr["I_TRA_ID"]), "I_TRA_IN_SEQ");
            if ((int)dr["E_SYM_ARRDEP"] == CIPS.DB.E_SYM_ARRDEP.START_DEP)
            {
                if (drs.Length > 0)
                {
                    YardId = Convert.ToInt32(drs[0]["I_YARD_ID"]);
                }
            }
            //1.计划 (报文状态：初始值，车流)
            if (E_CONS_STATE <= CIPS.DB.E_CONS_STATE.PCONS)
                st = CIPS.DB.E_ST_DEPCONS.PLAN;
            //2.确报形成 (报文状态：发确报，发确报xml)
            if (E_CONS_STATE == CIPS.DB.E_CONS_STATE.SENDCONS || E_CONS_STATE == CIPS.DB.E_CONS_STATE.CONSXML)
                st = CIPS.DB.E_ST_DEPCONS.MAKE;
            //3.车号核对　(核准比特码：车号核对)
            if ((B_SYM_CHECK & CIPS.DB.B_SYM_CHECK.MANUAL) == CIPS.DB.B_SYM_CHECK.MANUAL)
                st = CIPS.DB.E_ST_DEPCONS.CHECK;
            //4.发确报：(报文状态：已发确报)
            if (E_CONS_STATE == CIPS.DB.E_CONS_STATE.SENTCONS)
                st = CIPS.DB.E_ST_DEPCONS.CONS;
            if (E_CONS_STATE == CIPS.DB.E_CONS_STATE.RETURN)
                st = CIPS.DB.E_ST_DEPCONS.RETURN;
            //5.报点
            if (E_PL_STATE >= CIPS.DB.E_PL_STATE.END)
                st = CIPS.DB.E_ST_DEPCONS.NOTIFY;
            //6.已交班
            if (E_PL_STATE == CIPS.DB.E_PL_STATE.SHIFT)
                st = CIPS.DB.E_ST_DEPCONS.SHIFT;
            else if (E_PL_STATE >= CIPS.DB.E_PL_STATE.END && st == 0)
                st = CIPS.DB.E_ST_CONS.CAR_END;
            return st;
        }
        /// <summary>
        /// 列车表-》确报目录表(主系统专用)
        /// </summary>
        /// <param name="dt">列车表</param>
        /// <param name="dtTrainText">列车子表</param>
        /// <returns></returns>
        public static ClSet TrainTable2ClSet(DataTable dt, DataTable dtTrainText)
        {
            return TrainTable2ClSet(dt, dtTrainText, false);
        }
        /// <summary>
        /// 列车表-》确报目录表
        /// </summary>
        /// <param name="dt">列车表</param>
        /// <param name="dtTrainText">列车子表</param>
        /// <param name="bExpressMode">是否后备模式</param>
        /// <returns></returns>
        public static ClSet TrainTable2ClSet(DataTable dt,DataTable dtTrainText,bool bExpressMode)
        {
            ClTable ctarr, ctdep;
            string selcmd;
            ctarr = CreateTable("ARRCONS");
            ctdep = CreateTable("DEPCONS");
            int stationid = 0;
            selcmd = "E_SYM_ARRDEP <> " + CIPS.DB.E_PL_TYPE.ARR_LIGHTLOCOM.ToString() + " and E_PL_TYPE <> " + CIPS.DB.E_PL_TYPE.DEP_LIGHTLOCOM.ToString() +
                " and E_PL_STATE < " + CIPS.DB.E_PL_STATE.SHIFT.ToString() + " and E_TRA_CHARA <> " + CIPS.DB.E_TRA_CHARA.TRA_CAR.ToString() +
                " and E_RECORD_U<>" + CIPS.DB.E_RECORD_U.STOP.ToString() + " and E_RECORD_U<>" + CIPS.DB.E_RECORD_U.STA_ADD.ToString();
            DataRow[] drs = dt.Select(selcmd, "E_PL_STATE DESC,B_SYM_CHECK DESC, E_CONS_STATE DESC, I_LINE_ID_ARRDEP", System.Data.DataViewRowState.CurrentRows);
            short n1 = 1, n2 = 1;
            foreach (DataRow dr in drs)
            {
                int arrdep = Convert.ToInt16(dr["E_SYM_ARRDEP"]);
                ClRow cr = ctarr.NewRow();
                if (arrdep == CIPS.DB.E_SYM_ARRDEP.END_ARR)
                {
                    cr["E_ST_CONS"] = GetArrConsST(dr, dtTrainText, out stationid);
                    cr["I_CONS_ORDER"] = n1;
                    n1++;
                    ctarr.Rows.Add(cr);
                }
                else if (arrdep == CIPS.DB.E_SYM_ARRDEP.START_DEP)
                {
                    cr["E_ST_CONS"] = GetDepConsST(dr, dtTrainText, out stationid);
                    cr["I_CONS_ORDER"] = n2;
                    n2++;
                    ctdep.Rows.Add(cr);
                }
                else
                    continue;
                if(bExpressMode)
                    cr["I_CONS_ID"] = dr["I_CONS_ID"];
                else
                    cr["I_CONS_ID"] = dr["I_TRA_ID"];
                cr["C_TRA_NUM"] = dr["C_TRA_NUM"];
                cr["D_TRA"] = dr["D_TRA_TIME"];
                cr["I_LINE_ID_ARRDEP"] = dr["I_LINE_ID_ARRDEP"];
                cr["I_CAR_NUM"] = dr["I_CAR_NUM"];
                cr["F_LEN_TRA"] = dr["F_LEN"];
                cr["F_WEGH_TRA"] = dr["F_WEGH"];
                cr["B_SYM_CHECK"] = dr["B_SYM_CHECK"];
                cr["C_STA_CO"] = "";
            }
            ClSet cs = new ClSet();
            cs.Tables.Add(ctarr);
            cs.Tables.Add(ctdep);
            cs.Tables.Add(CreateInfoTable(0));
            return cs;
        }

        public static ClSet TrainTable2ClSet(DataTable dt, DataTable dtTrainText, bool bExpressMode, DataRow[] arrconsrows, DataRow[] depconsrows)
        {
            ClTable ctarr, ctdep;
            string selcmd;
            ctarr = CreateTable("ARRCONS");
            ctdep = CreateTable("DEPCONS");
            int stationid = 0;
            selcmd = "E_SYM_ARRDEP <> " + CIPS.DB.E_PL_TYPE.ARR_LIGHTLOCOM.ToString() + " and E_PL_TYPE <> " + CIPS.DB.E_PL_TYPE.DEP_LIGHTLOCOM.ToString() +
                " and E_PL_STATE < " + CIPS.DB.E_PL_STATE.SHIFT.ToString() + " and E_TRA_CHARA <> " + CIPS.DB.E_TRA_CHARA.TRA_CAR.ToString() +
                " and E_RECORD_U<>" + CIPS.DB.E_RECORD_U.STOP.ToString() + " and E_RECORD_U<>" + CIPS.DB.E_RECORD_U.STA_ADD.ToString();
            DataRow[] drs = dt.Select(selcmd, "E_PL_STATE DESC,B_SYM_CHECK DESC, E_CONS_STATE DESC, I_LINE_ID_ARRDEP", System.Data.DataViewRowState.CurrentRows);
            short n1 = 1, n2 = 1;
            foreach (DataRow dr in drs)
            {
                int arrdep = Convert.ToInt16(dr["E_SYM_ARRDEP"]);
                ClRow cr = ctarr.NewRow();
                if (arrdep == CIPS.DB.E_SYM_ARRDEP.END_ARR)
                {
                    cr["E_ST_CONS"] = GetArrConsST(dr, dtTrainText,out stationid);
                    cr["I_CONS_ORDER"] = n1;
                    n1++;
                    ctarr.Rows.Add(cr);
                }
                else if (arrdep == CIPS.DB.E_SYM_ARRDEP.START_DEP)
                {
                    cr["E_ST_CONS"] = GetDepConsST(dr, dtTrainText, out stationid);

                    cr["I_CONS_ORDER"] = n2;
                    n2++;
                    ctdep.Rows.Add(cr);
                }
                else
                    continue;
                if (bExpressMode)
                    cr["I_CONS_ID"] = dr["I_CONS_ID"];
                else
                    cr["I_CONS_ID"] = dr["I_TRA_ID"];
                cr["C_TRA_NUM"] = dr["C_TRA_NUM"];
                cr["D_TRA"] = dr["D_TRA_TIME"];
                cr["I_LINE_ID_ARRDEP"] = dr["I_LINE_ID_ARRDEP"];
                cr["I_CAR_NUM"] = dr["I_CAR_NUM"];
                cr["F_LEN_TRA"] = dr["F_LEN"];
                cr["F_WEGH_TRA"] = dr["F_WEGH"];
                cr["B_SYM_CHECK"] = dr["B_SYM_CHECK"];
                cr["C_STA_CO"] = "";
                cr["I_STATION_ID"] = stationid;
            }

            foreach (DataRow dr in arrconsrows)
            {
                if ((Convert.ToInt32(dr["B_SYM_CHECK"]) & CIPS.DB.B_SYM_CHECK.STOP) != 0)
                    continue;
                ClRow cr = ctarr.NewRow();
                cr["E_ST_CONS"] = dr["E_ST_CONS"];
                cr["I_CONS_ORDER"] = n1;
                n1++;
                ctarr.Rows.Add(cr);
                cr["I_CONS_ID"] = dr["I_CONS_ID"];
                cr["C_TRA_NUM"] = dr["C_TRA_NUM_ARR"];
                cr["D_TRA"] = dr["D_TRA_DEP"];
                cr["I_LINE_ID_ARRDEP"] = dr["I_LINE_ID"];
                cr["I_CAR_NUM"] = dr["I_CAR_NUM"];
                cr["F_LEN_TRA"] = dr["F_LEN_TRA"];
                cr["F_WEGH_TRA"] = dr["F_WEGH_TRA"];
                cr["B_SYM_CHECK"] = dr["B_SYM_CHECK"];
                cr["C_STA_CO"] = dr["C_STA_CO_ARR"];
            }
            foreach (DataRow dr in depconsrows)
            {
                if ((Convert.ToInt32(dr["B_SYM_CHECK"]) & CIPS.DB.B_SYM_CHECK.STOP) != 0)
                    continue;
                ClRow cr = ctdep.NewRow();
                cr["E_ST_CONS"] = dr["E_ST_DEPCONS"];
                cr["I_CONS_ORDER"] = n2;
                n2++;
                ctdep.Rows.Add(cr);
                cr["I_CONS_ID"] = dr["I_CONS_ID"];
                cr["C_TRA_NUM"] = dr["C_TRA_NUM_DEP"];
                cr["D_TRA"] = dr["D_TRA_DEP"];
                cr["I_LINE_ID_ARRDEP"] = dr["I_LINE_ID"];
                cr["I_CAR_NUM"] = dr["I_CAR_NUM"];
                cr["F_LEN_TRA"] = dr["F_LEN_TRA"];
                cr["F_WEGH_TRA"] = dr["F_WEGH_TRA"];
                cr["B_SYM_CHECK"] = dr["B_SYM_CHECK"];
                cr["C_STA_CO"] = dr["C_STA_CO_DEP"];
            }
            ClSet cs = new ClSet();
            cs.Tables.Add(ctarr);
            cs.Tables.Add(ctdep);
            cs.Tables.Add(CreateInfoTable(0));
            return cs;
        }

        #endregion
        #region 确报目录 for bak
        /// <summary>
        /// 到发确报目录表-》CLTABLE
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="tdcsmode"></param>
        /// <param name="dsdict"></param>
        /// <returns></returns>
        public static ClSet ConsTable2ClSet(DataSet ds, byte tdcsmode,DataSet dsdict)
        {
            string[] tn ={ "C_TRA_NUM_ARR", "C_TRA_NUM_DEP" };
            string[] id ={ "I_CONS_ID", "I_CONS_ID" };
            string[] st ={ "E_ST_CONS", "E_ST_DEPCONS" };
            string[] tm ={ "D_TRA_DEP", "D_TRA_DEP" };
            string[] lid ={ "I_LINE_ID", "I_LINE_ID" };
            string[] ccnt ={ "I_CAR_NUM", "I_CAR_NUM" };
            string[] len ={ "F_LEN_TRA", "F_LEN_TRA" };
            string[] weig ={ "F_WEGH_TRA", "F_WEGH_TRA" };
            string[] sta ={ "C_STA_CO_ARR", "C_STA_CO_DEP" };
            string[] bstat ={ "B_SYM_CHECK", "B_SYM_CHECK" };
            string[] locom1 ={ "C_LOCOM_CO1", "C_LOCOM_CO1" };
            string[] locom2 ={ "C_LOCOM_CO2", "C_LOCOM_CO2" };
            ClTable ctarr, ctdep;
            ctarr = CreateTable("ARRCONS");
            ctdep = CreateTable("DEPCONS");
            ClTable ct;
            DataRow[] drs;
            ct = ctarr;
            drs = ds.Tables["DM_TD_ARRCONSLIST"].Select("E_ST_CONS < " + CIPS.DB.E_ST_CONS.SHIFT, "E_ST_CONS DESC, D_TRA_DEP, I_LINE_ID", DataViewRowState.CurrentRows);
            for (int i = 0; i < 2; i++)
            {
                short n = 1;
                foreach (DataRow dr in drs)
                {
                    if ((Convert.ToInt32(dr["B_SYM_CHECK"]) & CIPS.DB.B_SYM_CHECK.STOP) != 0)
                        continue;
                    ClRow cr = ct.NewRow();
                    cr["I_CONS_ID"] = dr[id[i]];
                    cr["E_ST_CONS"] = dr[st[i]];
                    cr["C_TRA_NUM"] = dr[tn[i]];
                    cr["D_TRA"] = dr[tm[i]];
                    cr["I_LINE_ID_ARRDEP"] = dr[lid[i]];
                    cr["I_CAR_NUM"] = dr[ccnt[i]];
                    cr["F_LEN_TRA"] = dr[len[i]];
                    cr["F_WEGH_TRA"] = dr[weig[i]];
                    cr["B_SYM_CHECK"] = dr[bstat[i]];
                    cr["I_CONS_ORDER"] = n;
                    cr["C_LOCOM_CO1"] = dr[locom1[i]];
                    cr["C_LOCOM_CO2"] = dr[locom2[i]];
                    cr["C_STA_CO"] = "";
                    cr["I_STATION_ID"] = 0;
                    DataRow[] _drs = dsdict.Tables["DC_TD_STA"].Select("C_STA_CO = '" + dr[sta[i]].ToString().Trim() + "'");
                    if (_drs.Length > 0)
                    {
                        _drs = dsdict.Tables["DC_TD_GRO"].Select("I_GRO_ID =" + _drs[0]["I_GRO_ID"].ToString().Trim());
                        if (_drs.Length > 0)
                            cr["C_STA_CO"] = _drs[0]["C_GRO_FORSH"].ToString().Trim();
                    }
                    _drs = dsdict.Tables["DC_RA_YARD"].Select("C_RANGE_FORSH = '" + dr[sta[i]].ToString().Trim() + "' and E_RANGE_LEVEL = " + CIPS.DB.E_RANGE_LEVEL.STATION.ToString());
                    if (_drs.Length > 0)
                        cr["I_STATION_ID"] = Convert.ToInt16(_drs[0]["I_RANGE_ID"]);
                    n++;
                    ct.Rows.Add(cr);
                }
                ct = ctdep;
                drs = ds.Tables["DM_TD_DEPCONSLIST"].Select("E_ST_DEPCONS < " + CIPS.DB.E_ST_DEPCONS.SHIFT, "E_ST_DEPCONS DESC, D_TRA_DEP, I_LINE_ID", DataViewRowState.CurrentRows);
            }
            ClSet cs = new ClSet();
            cs.Tables.Add(ctarr);
            cs.Tables.Add(ctdep);
            cs.Tables.Add(CreateInfoTable(tdcsmode));
            return cs;
        }
        #endregion
        #endregion


    }

}
