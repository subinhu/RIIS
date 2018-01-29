using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace Dictionary2CS
{
    class MakeFunc
    {
        public static string BuildCS(string fname, DataSet ds)
        {
            DataTable dt = ds.Tables["DC_SA_FUNCTION"];
            DataRow[] drsclass = ds.Tables["DC_SA_EXE"].Select("C_EXE_FILE <> ''","C_EXE_FILE");
            DataRow[] drs;
            FileStream fsFile = File.Create(fname);
            StreamWriter csFile = new StreamWriter(fsFile, Encoding.Default);

            //DataRow[] mixRows;
            //mixRows = dt.Select("", "C_SEG_NA ASC");

            char[] ch = { ' ', '\n', '\t', '\r' };
            csFile.WriteLine("namespace CIPS.FUNCTION");
            csFile.WriteLine("{");

            int i = 0;
            string strRecord = "\t\t//";
            string sclass;
            foreach(DataRow drclass in drsclass)
            {
                drs = dt.Select("I_EXE_ID = "+drclass["I_EXE_ID"].ToString(), "C_FUNCT_ENGLISH");
                sclass = drclass["C_EXE_FILE"].ToString().Trim().ToUpper();
                i = sclass.LastIndexOf(".EXE");
                if (i > 0)
                    sclass=sclass.Remove(i);
                sclass=sclass.Replace('.', '_');
                if (drs.Length > 0)
                {

                    csFile.WriteLine("\t/// <summary>");
                    csFile.WriteLine("\t/// " + drclass["C_EXE_CO"].ToString().Trim(ch) + "!");
                    csFile.WriteLine("\t/// " + drclass["C_EXE_MEMO"].ToString().Trim(ch) + "!");
                    csFile.WriteLine("\t/// </summary>");
                    csFile.WriteLine("\tpublic class " + sclass);
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
                        csFile.WriteLine("\t\tpublic const int {0} = {1};", vname, dr["I_FUNCT_ID"].ToString());
                    }

                    csFile.WriteLine();
                    csFile.WriteLine("\t};//end class" + sclass);
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
            return "DC_SA_FUNCTION (id) 完成";
        }
    }
}
