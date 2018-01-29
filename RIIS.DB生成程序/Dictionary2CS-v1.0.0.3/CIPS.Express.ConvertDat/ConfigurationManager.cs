using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace CIPS
{
    /// <summary>
    /// 应用程序配置管理
    /// </summary>
    public class ConfigurationManager
    {
        class INFO
        {
            public string Namespace = "";
            public string Keyname = "";
            public string Value = "";
        }
        List<INFO> Infos = new List<INFO>();
        /// <summary>
        /// 应用程序配置管理
        /// </summary>
        /// <param name="dt">DC_RA_CONFIG表</param>
        public ConfigurationManager(DataTable dt)
        {
            if (dt == null)
                return;
            foreach (DataRow dr in dt.Rows)
            {
                INFO i = new INFO();
                i.Namespace = dr["C_NAMESPACE"].ToString().Trim().ToUpper();
                i.Keyname = dr["C_KEY"].ToString().Trim().ToUpper();
                i.Value = dr["C_VALUE"].ToString();
                Infos.Add(i);
            }
        }
        /// <summary>
        /// 获取配置内容
        /// </summary>
        /// <param name="Namespace">命名空间</param>
        /// <param name="Key">键</param>
        /// <returns>值</returns>
        public string this[string Namespace, string Key]
        {
            get
            {
                string ns = Namespace.ToUpper().Trim();
                string k = Key.ToUpper().Trim();
                foreach (INFO i in Infos)
                {
                    if (i.Keyname == k && i.Namespace == ns)
                        return i.Value;
                }
                foreach (INFO i in Infos)
                {
                    if (i.Keyname == k && i.Namespace == "")
                        return i.Value;
                }
                return System.Configuration.ConfigurationManager.AppSettings[Key];
            }
        }
        /// <summary>
        /// 获取配置内容
        /// </summary>
        /// <param name="Key">键</param>
        /// <returns>值</returns>
        public string this[string Key]
        {
            get
            {
                string dllname = "";
                System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                System.Diagnostics.StackFrame[] fs = st.GetFrames();
                foreach (System.Diagnostics.StackFrame f in fs)
                {
                    Type tp = f.GetMethod().DeclaringType;
                    if (tp == typeof(ConfigurationManager))
                        continue;
                    dllname = tp.Namespace.ToUpper();
                    break;
                }
                return this[dllname, Key];
            }
        }
    }
}
