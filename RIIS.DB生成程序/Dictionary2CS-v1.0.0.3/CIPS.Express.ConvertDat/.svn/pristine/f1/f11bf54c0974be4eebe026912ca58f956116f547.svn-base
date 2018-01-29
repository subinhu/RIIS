using System;
using System.Collections.Generic;
using System.Text;

namespace CIPS
{
    /// <summary>
    /// 客户端命令
    /// </summary>
    public class ClientCommand
    {
        /// <summary>
        /// 命令
        /// </summary>
        public string Cmd = "";
        /// <summary>
        /// 站码
        /// </summary>
        public string StationCode = "";
        /// <summary>
        /// 岗位
        /// </summary>
        public string PostName = "";
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator = "";
        /// <summary>
        /// 客户端IP
        /// </summary>
        public string Ip = "";
        /// <summary>
        /// 注释
        /// </summary>
        public string Note = "";
        /// <summary>
        /// 命令类型
        /// </summary>
        protected string CmdType = "";
        /// <summary>
        /// 包ID
        /// </summary>
        public long PackageID = -1;
        private void ToClTable(ClSet cs)
        {
            ClTable ct=cs.Tables["CmdInfo"];
            if (ct == null)
            {
                ct = new ClTable("CmdInfo");
                cs.Tables.Add(ct);
            }
            else
                ct.Rows.Clear();
            ct.AddColumn("DATA", typeof(string));
            ClRow cr;
            cr = ct.NewRow();
            cr[0] = CmdType;
            ct.Rows.Add(cr);
            cr = ct.NewRow();
            cr[0] = Cmd;
            ct.Rows.Add(cr);
            cr = ct.NewRow();
            cr[0] = StationCode;
            ct.Rows.Add(cr);
            cr = ct.NewRow();
            cr[0] = PostName;
            ct.Rows.Add(cr);
            cr = ct.NewRow();
            cr[0] = Operator;
            ct.Rows.Add(cr);
            cr = ct.NewRow();
            cr[0] = Ip;
            ct.Rows.Add(cr);
            cr = ct.NewRow();
            cr[0] = Note;
            ct.Rows.Add(cr);
            cr = ct.NewRow();
            cr[0] = PackageID.ToString();
            ct.Rows.Add(cr);
        }
        /// <summary>
        /// 获取字节流的对象名
        /// </summary>
        /// <param name="buf">字节流</param>
        /// <returns></returns>
        public static string GetCommandType(byte[] buf)
        {
            string s = CIPS.Express.ConvertDat.Tools.GetUnicodeString(buf, 0);
            if (s.StartsWith(typeof(CIPS.ClientCommand).FullName + "\n"))
            {
                string[] ss = s.Split('\n');
                if (ss.Length == 2)
                    return ss[1];
            }
            return null;
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="cs">数据集</param>
        /// <returns>字节流</returns>
        protected byte[] Serialize(ClSet cs)
        {
            string s;
            s = typeof(CIPS.ClientCommand).FullName + "\n" + GetType().FullName + "\0";
            ToClTable(cs);
            byte[] head = System.Text.Encoding.Unicode.GetBytes(s);
            byte[] b = cs.Serialize(head.Length);
            head.CopyTo(b, 0);
            return b;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="b">字节流</param>
        /// <returns>数据集</returns>
        protected ClSet Deserialize(byte[] b)
        {
            int off = 0;
            string s = CIPS.Express.ConvertDat.Tools.GetUnicodeString(b, ref off);
            string tp = null;
            if (s.StartsWith(typeof(CIPS.ClientCommand).FullName + "\n"))
            {
                string[] ss = s.Split('\n');
                if (ss.Length == 2)
                    tp = ss[1];
            }

            //if (tp != GetType().FullName)
            //    return null;
            //byte[] head = System.Text.Encoding.Default.GetBytes(cmdhead);
            //if (!IsCipsClientCmd(b))
            //    return null;
            ClSet cs = new ClSet(b, off);
            ClTable ct = cs.Tables["CmdInfo"];
            if (ct != null)
            {
                if (ct.Rows.Count > 0)
                    CmdType = ct.Rows[0][0].ToString();
                if (ct.Rows.Count > 1)
                    Cmd = ct.Rows[1][0].ToString();
                if (ct.Rows.Count > 2)
                    StationCode = ct.Rows[2][0].ToString();
                if (ct.Rows.Count > 3)
                    PostName = ct.Rows[3][0].ToString();
                if (ct.Rows.Count > 4)
                    Operator = ct.Rows[4][0].ToString();
                if (ct.Rows.Count > 5)
                    Ip = ct.Rows[5][0].ToString();
                if (ct.Rows.Count > 6)
                    Note = ct.Rows[6][0].ToString();
                if (ct.Rows.Count > 7)
                    PackageID = Convert.ToInt64(ct.Rows[7][0]);
            }
            return cs;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="buf"></param>
        public ClientCommand(byte[] buf)
        {
            Deserialize(buf);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ClientCommand()
        { }
    }
}
