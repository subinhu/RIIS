using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace Dictionary2CS
{
    class MakeFuncStr
    {
        public static string BuildCS(string fname, DataSet ds)
        {
            DataTable dt = ds.Tables["DC_SA_FUNCTION"];
            DataRow[] drs;
            FileStream fsFile = File.Create(fname);
            StreamWriter csFile = new StreamWriter(fsFile, Encoding.Default);

            //DataRow[] mixRows;
            //mixRows = dt.Select("", "C_SEG_NA ASC");
            List<string> exenames = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string s = dr["C_EXE_ENGLISH"].ToString().ToUpper().Trim();
                if (s == "")
                    continue;
                if (!exenames.Contains(s))
                    exenames.Add(s);
            }
            char[] ch = { ' ', '\n', '\t', '\r' };
            csFile.WriteLine("namespace CIPS.FUNC");
            csFile.WriteLine("{");

            string strRecord = "\t\t//";
            foreach (string exename in exenames)
            {
                string execo = "", exememo = "";
                DataRow[] drsclass = ds.Tables["DC_SA_EXE"].Select("C_EXE_ENGLISH = '" + exename + "'", "C_EXE_FILE");
                if (drsclass.Length > 0)
                {
                    execo = drsclass[0]["C_EXE_CO"].ToString().Trim(ch) + "!";
                    exememo = drsclass[0]["C_EXE_MEMO"].ToString().Trim(ch) + "!";

                }
                drs = dt.Select("C_EXE_ENGLISH = '" + exename + "'", "C_FUNCT_ENGLISH");
                if (drs.Length > 0)
                {
                    csFile.WriteLine("\t/// <summary>");
                    csFile.WriteLine("\t/// " + execo);
                    csFile.WriteLine("\t/// " + exememo);
                    csFile.WriteLine("\t/// </summary>");
                    csFile.WriteLine("\tpublic class " + exename.Replace('.', '_'));
                    csFile.WriteLine("\t{");


                    foreach (DataRow dr in drs)
                    {
                        csFile.WriteLine("\t\t/// <summary>");
                        csFile.WriteLine("\t\t/// " + dr["C_FUNCT_CO"].ToString().Trim(ch) + "!");
                        csFile.WriteLine("\t\t/// " + dr["C_REMARK"].ToString().Trim(ch) + "!");
                        csFile.WriteLine("\t\t/// 序号 = {0}", dr["I_FUNCT_NO"].ToString());

                        csFile.WriteLine("\t\t/// </summary>");
                        string vname = dr["C_FUNCT_ENGLISH"].ToString().ToUpper();
                        vname = vname.Replace('/', '_');
                        vname = vname.Replace('-', '_');
                        vname = vname.Replace('.', '_');
                        vname = vname.Replace('&', '_');
                        csFile.WriteLine("\t\tpublic const string {0} = \"{1}\";", vname, dr["C_FUNCT_ENGLISH"].ToString());
                    }

                    csFile.WriteLine();
                    csFile.WriteLine("\t};//end class" + exename);
                }

            }
            csFile.WriteLine();

            //加上默认值注释

            csFile.WriteLine("\t/// <summary>");
            csFile.WriteLine("\t/// 默认值" + "!");
            csFile.WriteLine("\t/// </summary>");
            csFile.WriteLine("\tpublic class E_DEFAULT");
            csFile.WriteLine("\t{");

            csFile.WriteLine("\t\t/// <summary>");
            csFile.WriteLine("\t\t/// 整型 枚举 比特位" + "!");
            csFile.WriteLine("\t\t/// E_INT = 0");
            csFile.WriteLine("\t\t/// </summary>");
            csFile.WriteLine("\t\tpublic static int E_INT = 0;");

            csFile.WriteLine("\t\t/// <summary>");
            csFile.WriteLine("\t\t/// 字符型" + "!");
            csFile.WriteLine("\t\t/// E_STR = \" \"");
            csFile.WriteLine("\t\t/// </summary>");
            csFile.WriteLine("\t\tpublic static string  E_STR = \" \";");

            csFile.WriteLine("\t\t/// <summary>");
            csFile.WriteLine("\t\t/// 日期型" + "!");
            csFile.WriteLine("\t\t/// E_DATETIME = new DateTime(2000,1,1,0,0,0);");
            csFile.WriteLine("\t\t/// </summary>");
            csFile.WriteLine("\t\tpublic static System.DateTime E_DATETIME = new System.DateTime(1990,1,1,12,0,0);");

            csFile.WriteLine("\t}//end class E_DEFAULT");
            //完成加上默认值注释			

            csFile.WriteLine(strRecord);
            csFile.WriteLine("}//namespace");

            csFile.Close();
            return "DC_SA_FUNCTION (string) 完成";
        }
    }
}
