using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Dictionary2CS
{
    class MakeMixco
    {
        public static string BuildCS(string fname, DataTable dt)
        {
            FileStream fsFile = File.Create(fname);
            StreamWriter csFile = new StreamWriter(fsFile, Encoding.UTF8);

            DataRow[] mixRows;
            mixRows = dt.Select("", "C_SEG_NA ASC");

            char[] ch = { ' ', '\n', '\t', '\r' };
            csFile.WriteLine("namespace RIIS.DB");
            csFile.WriteLine("{");

            int i = 0, j = 0, k = 0, m = 0;
            bool isRepeat = false;
            //string strRecord = "\t\t//";
            while (i < mixRows.Length)
            {
                j = i; k = 0;
                while (j < mixRows.Length - 1)
                {
                    k++;
                    if (mixRows[j]["C_SEG_NA"].ToString() != mixRows[j + 1]["C_SEG_NA"].ToString()) 
                        break;
                    j++;
                }

                if (k > 0)
                {

                    csFile.WriteLine("\t/// <summary>");
                    //csFile.WriteLine("\t/// " + mixRows[i]["C_CO_TYPE"].ToString().Trim(ch) + "!");
                    csFile.WriteLine("\t/// " + mixRows[i]["C_REMARK"].ToString().Trim(ch) + "!");
                    csFile.WriteLine("\t/// </summary>");
                    csFile.WriteLine("\tpublic enum " + mixRows[i]["C_SEG_NA"].ToString().ToUpper());
                    csFile.WriteLine("\t{");

                    for (k = i; k <= j; k++)
                    {
                                                {
                            isRepeat = false;
                            for (m = i; m < k; m++)
                            {
                                
                                if (mixRows[k]["C_SPELL"].ToString() == mixRows[m]["C_SPELL"].ToString())
                                {
                                    //strRecord += "Row" + mixRows[m]["I_SEQ"].ToString() + " = Row" + mixRows[k]["I_SEQ"].ToString() + ",  ";

                                    isRepeat = true;
                                    break;
                                }
                            }

                            if (!isRepeat)
                            {
                                csFile.WriteLine("\t\t/// <summary>");
                             
                                
                                csFile.WriteLine("\t\t/// " + mixRows[k]["C_FULL"].ToString().Trim(ch) + "!");
                                
                              
                                csFile.WriteLine("\t\t/// </summary>");
                                string vname = mixRows[k]["C_SPELL"].ToString().ToUpper();
                                vname = vname.Replace('/', '_');
                                
                                    csFile.WriteLine("\t\t {0} ,", vname);
                                
                            }
                        }
                    }//end for(k);

                    csFile.WriteLine();
                    csFile.WriteLine("\t}//end enum " + mixRows[i]["C_SEG_NA"].ToString().ToUpper());
                }

                i = j + 1;
            }
            csFile.WriteLine();

            
            csFile.WriteLine("}//namespace");

            csFile.Close();
            return "DC_MIXCO Íê³É";
        }
    }
}
