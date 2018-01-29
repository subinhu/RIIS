using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;

namespace CIPS.Express.ConvertDat
{
    /// <summary>
    /// CIPS用户界面提示管理
    /// </summary>
    public class Prompt
    {
        /// <summary>
        /// Prompt实例
        /// </summary>
        protected class OnePrompt
        {
            /// <summary>
            /// 命名空间
            /// </summary>
            public string Namespace = "";
            /// <summary>
            /// 索引名
            /// </summary>
            public string accessname = "";
            /// <summary>
            /// 显示名
            /// </summary>
            public string displayname = "";
            /// <summary>
            /// 中文名
            /// </summary>
            public string chinesename = "";
        }
        System.Collections.Hashtable Notes = new System.Collections.Hashtable();
        //List<OnePrompt> Notes = new List<OnePrompt>();
        //class PromptGroup
        //{
        //    public string Namespace = "";
        //    public List<OnePrompt> Notes = new List<OnePrompt>();
        //}
        //List<PromptGroup> Groups = new List<PromptGroup>();
        int Mode = 0;
        int _Version = 0;
        /// <summary>
        /// 版本
        /// 0:按顺序填参数
        /// 1:按%后的数值选择参数
        /// </summary>
        public int Version
        {
            get
            {
                return _Version;
            }
            set
            {
                _Version = value;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dt">DC_RA_PROMPT表</param>
        public Prompt(DataTable dt)
        {
            Init(dt);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="ct">DC_RA_PROMPT表</param>
        public Prompt(ClTable ct)
        {
            if (ct != null)
                Init(ct.GetDataTable());
        }
        /// <summary>
        /// 重新初始化
        /// </summary>
        /// <param name="dt">DC_RA_PROMPT表</param>
        public void Init(DataTable dt)
        {
            if (dt == null)
                return;
            
            //List<OnePrompt> ns = new List<OnePrompt>();
            //PromptGroup gro = new PromptGroup();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    OnePrompt p = new OnePrompt();
            //    p.accessname = dr["C_ACCESS_CODE"].ToString().Trim();
            //    p.chinesename = dr["C_CHINESE_CODE"].ToString().Trim();
            //    p.displayname = dr["C_DISPLAY_CODE"].ToString().Trim();
            //    p.Namespace = dr["C_NAMESPACE"].ToString().Trim();
            //    if (p.accessname == "DC_RA_PROMPT:MODE")
            //        int.TryParse(p.displayname, out Mode);
            //    else
            //        ns.Add(p);
            //    if (p.Namespace == "")
            //        gro.Notes.Add(p);
            //}
            //Groups.Add(gro);
            //Notes = ns;
            System.Collections.Hashtable nsgro = new System.Collections.Hashtable();
            foreach (DataRow dr in dt.Rows)
            {
                OnePrompt p = new OnePrompt();
                p.accessname = dr["C_ACCESS_CODE"].ToString().Trim();
                p.chinesename = dr["C_CHINESE_CODE"].ToString().Trim();
                p.displayname = dr["C_DISPLAY_CODE"].ToString().Trim();
                p.Namespace = dr["C_NAMESPACE"].ToString().Trim();
                if (p.accessname == "DC_RA_PROMPT:MODE")
                    int.TryParse(p.displayname, out Mode);
                else
                {
                    System.Collections.Hashtable ns = nsgro[p.Namespace] as System.Collections.Hashtable;
                    if (ns == null)
                    {
                        ns = new System.Collections.Hashtable();
                        nsgro.Add(p.Namespace, ns);
                    }
                    ns.Add(p.accessname, p);
                }
            }
            Notes = nsgro;
        }
        /// <summary>
        /// 获取提示内容
        /// </summary>
        /// <param name="Note">模板字符串</param>
        /// <returns></returns>
        public string GetPrompt(string Note)
        {
            return GetPrompt(Note, null);
        }
        /// <summary>
        /// 获取提示内容
        /// </summary>
        /// <param name="Note">模板字符串</param>
        /// <param name="Param">参数</param>
        /// <returns>返回对应的提示字符串</returns>
        public string GetPrompt(string Note, object[] Param)
        {
            string dllname = "";
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            System.Diagnostics.StackFrame[] fs = st.GetFrames();
            foreach (System.Diagnostics.StackFrame f in fs)
            {
                Type tp = f.GetMethod().DeclaringType;
                if (tp == typeof(Prompt))
                    continue;
                dllname = tp.Namespace.ToUpper();
                break;
                //if (st.FrameCount > 1)
                //    dllname = st.GetFrame(1).GetMethod().DeclaringType.Namespace.ToUpper();
            }
            return GetPrompt(dllname, Note, Param);
        }
        System.Threading.Mutex mutex = new Mutex();
        /// <summary>
        /// 获取提示内容
        /// </summary>
        /// <param name="Namespace">命名空间</param>
        /// <param name="Note">模板字符串</param>
        /// <param name="Param">参数</param>
        /// <returns>返回对应的提示字符串</returns>
        public string GetPrompt(string Namespace, string Note, object[] Param)
        {
            if (Note == "")
                return "";
            OnePrompt newprompt = null;
            mutex.WaitOne();
            System.Collections.Hashtable ns = Notes[Namespace] as System.Collections.Hashtable;
            if (ns == null)
            {
                ns = new System.Collections.Hashtable();
                Notes.Add(Namespace, ns);
            }
            OnePrompt prompt = ns[Note] as OnePrompt;
            if (prompt == null)
            {
                prompt = new OnePrompt();
                prompt.accessname = Note;
                prompt.chinesename = Note;
                prompt.displayname = Note;
                prompt.Namespace = Namespace;
                newprompt = prompt;
                ns.Add(Note, prompt);
            }
            mutex.ReleaseMutex();
            if (newprompt != null)
            {
                PromptCannotFind tmp = CannotFind;
                if (tmp != null)
                    tmp(newprompt);
            }
            if (Mode == 1)
                return BuildNote(prompt.displayname, Param);
            else if (Mode == 2)
                return BuildNote(prompt.chinesename, Param);
            else if (Mode == 3)
                return BuildNote(prompt.displayname, Param) + "\r\n" + BuildNote(Note, Param);
            else
                return BuildNote(Note, Param);
        }
        //public string GetPrompt(string Namespace, string Note, object[] Param)
        //{
        //    if (Note == "")
        //        return "";
        //    OnePrompt prompt = null;
        //    PromptGroup[] gros = new PromptGroup[2];
        //    if (Groups.Count > 0)
        //        gros[0] = Groups[0];
        //    if (Namespace != "")
        //    {
        //        for (int i = 1; i < Groups.Count; i++)
        //        {
        //            if (Groups[i].Namespace == Namespace)
        //            {
        //                gros[1] = Groups[i];
        //                break;
        //            }
        //        }
        //        if (gros[1] == null)
        //        {
        //            gros[1] = new PromptGroup();
        //            gros[1].Namespace = Namespace;
        //            foreach (OnePrompt p in Notes)
        //            {
        //                if (Namespace == p.Namespace)
        //                    gros[1].Notes.Add(p);
        //            }
        //            Groups.Add(gros[1]);
        //        }
        //    }
        //    foreach (PromptGroup g in gros)
        //    {
        //        if (g == null)
        //            continue;
        //        foreach (OnePrompt o in g.Notes)
        //        {
        //            if (o.accessname == Note)
        //            {
        //                prompt = o;
        //                break;
        //            }
        //        }
        //        if (prompt != null)
        //            break;
        //    }
        //    if (prompt == null)
        //    {
        //        prompt = new OnePrompt();
        //        prompt.accessname = Note;
        //        prompt.chinesename = Note;
        //        prompt.displayname = Note;
        //        prompt.Namespace = Namespace;
        //        Notes.Add(prompt);
        //        foreach (PromptGroup g in gros)
        //        {
        //            if (g == null)
        //                continue;
        //            if (g.Namespace == Namespace)
        //            {
        //                g.Notes.Add(prompt);
        //                break;
        //            }
        //        }
        //        PromptCannotFind tmp = CannotFind;
        //        if (tmp != null)
        //            tmp(prompt);
        //    }
        //    if (Mode == 1)
        //        return BuildNote(prompt.displayname, Param);
        //    else if (Mode == 2)
        //        return BuildNote(prompt.chinesename, Param);
        //    else if (Mode == 3)
        //        return BuildNote(prompt.displayname, Param) + "\r\n" + BuildNote(Note, Param);
        //    else
        //        return BuildNote(Note, Param);

        //}
        string BuildNote(string note, object[] param)
        {
            if (note == "")
                return "";
            if (param == null)
                param = new object[0];
            if (_Version == 0)
            {
                string s = note.Replace("\0", "").Replace("%%", "\0");
                string[] ss = s.Split('%');
                s = "";
                for (int i = 0; i < ss.Length; i++)
                {
                    s += ss[i];
                    if (i < param.Length)
                    {
                        if (param[i] == null)
                            param[i] = "";
                        s += param[i].ToString().Replace("\0", "");
                    }
                }
                s = s.Replace('\0', '%');
                return s;
            }
            else if (_Version == 1)
            {
                string[] ss = note.Split('%');
                string s = ss[0];
                for (int i = 1; i < ss.Length; i++)
                {
                    if (ss[i].Length == 0)
                        s += "%";
                    else
                    {
                        int n;
                        if (int.TryParse(ss[i][0].ToString(), out n))
                        {
                            n--;
                            if (n >= 0 && n < param.Length)
                                s += param[n];
                        }
                        s += ss[i].Substring(1);
                    }
                }
                return s;
            }
            else
                return note;
        }
        /// <summary>
        /// 表中未找到
        /// </summary>
        /// <param name="prompt"></param>
        protected delegate void PromptCannotFind(OnePrompt prompt);
        /// <summary>
        /// 未找到
        /// </summary>
        protected event PromptCannotFind CannotFind;
    }
}
