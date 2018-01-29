using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Dictionary2CS
{
    class MakeCarChara
    {
        public static string BuildCS(string fname, DataTable dt)
        {
            FileStream fsFile = File.Create(fname);
            StreamWriter csFile = new StreamWriter(fsFile, Encoding.Default);
            string strRecord = "\t\t//";

            DataRow[] mixRows = dt.Select("","I_CO_VALUE");
            mixRows = dt.Select("", "I_CO_VALUE");

            char[] ch = { ' ', '\n', '\t', '\r' };
            csFile.WriteLine("namespace CIPS");
            csFile.WriteLine("{");
            csFile.WriteLine("\t/// <summary>");
            csFile.WriteLine("\t/// 车辆自定义特征" + "!");
            csFile.WriteLine("\t/// </summary>");
            csFile.WriteLine("\tpublic class B_CAR_CHARA_DEFINE");
            csFile.WriteLine("\t{");
            for (int k = 0; k < mixRows.Length; k++)
            {
                if (Convert.ToInt64(mixRows[k]["I_CO_VALUE"]) > Int64.MaxValue)
                    continue;
                csFile.WriteLine("\t\t/// <summary>");
                csFile.WriteLine("\t\t/// " + mixRows[k]["C_CO_TYPE"].ToString().Trim(ch) + "!");
                csFile.WriteLine("\t\t/// " + mixRows[k]["C_CO_DESC"].ToString().Trim(ch) + "!");
                csFile.WriteLine("\t\t/// {0} = {1}", mixRows[k]["C_FORSH_SPELL"].ToString().ToUpper().Trim(ch), mixRows[k]["I_CO_VALUE"].ToString());
                csFile.WriteLine("\t\t/// </summary>");
                string vname = mixRows[k]["C_FORSH_SPELL"].ToString().ToUpper();
                vname = vname.Replace('/', '_');
                if (Convert.ToInt64(mixRows[k]["I_CO_VALUE"]) > Int32.MaxValue)
                    csFile.WriteLine("\t\tpublic const long {0} = {1};", vname, mixRows[k]["I_CO_VALUE"].ToString());
                else
                    csFile.WriteLine("\t\tpublic const int {0} = {1};", vname, mixRows[k]["I_CO_VALUE"].ToString());
            }
            csFile.WriteLine("\t}//end class B_CAR_CHARA_DEFINE");

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
            return "DC_RA_CARCHARA 完成";
        }
    }
}
