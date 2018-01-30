using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RIIS.DbParameter;

using MySql.Data.MySqlClient;

namespace WindowsFormsApp3
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int id;
        public string type;
        public string name;

        private List<DbParam> getDbParams()
        {
            List<DbParam> dbParams = new List<DbParam>();
            dbParams.Add(new DbParam("LH_NAME","林大", MySqlDbType.VarChar));
            dbParams.Add(new DbParam("LH_TYPE", "大", MySqlDbType.VarChar));
            dbParams.Add(new DbParam("LH_ID", 520, MySqlDbType.Int32));
            
            return dbParams;
        }


        private void InsertToLH()
        {
            List<DbParam> dbParams = getDbParams();
            DataAccessHelper.InsertWithParams("lh_test",dbParams.ToArray());
        }
        public void DeleteGoods2DB()
        {
           
            DataAccessHelper.ExecuteSql("delete from lh_test where LH_ID=1");
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            DeleteGoods2DB();

        }

         
        private void button2_Click(object sender, EventArgs e)
        {
            //List<DbParam> dbParams = new List<DbParam>();
            //dbParams.Add(new DbParam("LH_NAME", "CRSCD", MySqlDbType.VarChar));
            //dbParams.Add(new DbParam("LH_TYPE", "uuu", MySqlDbType.VarChar));
            //dbParams.Add(new DbParam("LH_ID", 1, MySqlDbType.Int32));
            List<DbParam> dbParams = getDbParams();
            InsertToLH();
        }

        public void UpdateLH()
        {
            List<DbParam> dbParams=new List<DbParam>();
            dbParams.Add(new DbParam("LH_NAME", "CRSCD", MySqlDbType.VarChar));
            dbParams.Add(new DbParam("LH_TYPE", "uuu", MySqlDbType.VarChar));
            dbParams.Add(new DbParam("LH_ID", 1, MySqlDbType.Int32));
            DataAccessHelper.UpdateWithParams("lh_test", dbParams.ToArray());
        }
       
        public void UpDateLH_2()
        {
            List<DbParam> dbParams = new List<DbParam>();
            dbParams.Add(new DbParam("age", 120, MySqlDbType.Int32));
            dbParams.Add(new DbParam("FIRSTNAME", "王", MySqlDbType.VarChar));
            dbParams.Add(new DbParam("LASTNAME", "六", MySqlDbType.VarChar));
            DataAccessHelper.UpdateWithParams_2("lh_name", dbParams.ToArray(),2);
        }
        public void ExecuteSyn()
        {
            List<string> sqlList = new List<string>();
            string delSql = "update lh_test set LH_TYPE='不不不'where LH_ID = 1";
            sqlList.Add(delSql);
            delSql = "delete from lh_test  where LH_ID = 5";
            sqlList.Add(delSql);
            DataAccessHelper.ExecuteBatchSql(sqlList);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ExecuteSyn();
        }

        #region 修改到数据库
        private bool MySynchronize(DataTable dt)
        {
            
            List<string> sqlList = new List<string>();
            if (dt == null || dt.Rows.Count <= 0)
                return false;

            //List<string> ts = new List<string>();
            //ts.Add("lh_test");
            //ts.Add("fm_trade_goods");
            //DataSet ds = DataAccessHelper.GetDataTables(ts);

            //DataTable dt = ds.Tables["lh_test"];

            //DataRow[] drs = dt.Select("LH_ID=4 or LH_ID=5");


            //-----修改dt的数据
            //DataRow[] drs = dt.Select("LH_ID=4");
            //drs[0]["LH_ID"] = 888;
            //dd = dt.GetChanges();
            //dd.AcceptChanges();
            //string mysql1 = "update lh_test set LH_ID='888'where LH_ID = 4";
            //sqlList.Add(mysql1);

            //---------往dt增加数据
            //DataRow dr = dt.NewRow();
            //dr[0] = 666;
            //dr[1] = "阿胖";
            //dr[2] = "玫瑰茶";
            //dt.Rows.Add(dr);
            //dd = dt.GetChanges();
            //dd.AcceptChanges();
            //for (int i = 0; i < 2; i++)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr[0] = i+556;
            //    dr[1] = "大佬";
            //    dr[2] = "薄荷";
            //    dt.Rows.Add(dr);
            //}

            //string mysql2 = "insert into lh_test values(666,'阿胖','玫瑰茶')";
            //sqlList.Add(mysql2);

            //string mysql3 = "insert into lh_test values(667,'阿胖','玫瑰茶')";
            //sqlList.Add(mysql3);

            //string mysql4 = "insert into lh_test values(668,'阿胖','玫瑰茶')";
            //sqlList.Add(mysql4);
            //---------删除dt数据
            DataRow[] drss = dt.Select("LH_TYPE='Q'");
            drss[0].Delete();
            //dd = dt.GetChanges();
            //dd.AcceptChanges();
            //string mysql3 = "delete from lh_test where LH_ID=3";
            //sqlList.Add(mysql3);

            //DataAccessHelper.ExecuteBatchSql(sqlList);
            return true;

        }

        private bool MySynchronizeDB(DataTable dt)
        {

            List<string> sql_list = new List<string>();
            int i, j, k, m;

            string value_string;
            string setsql;
            string DBPrimary;
            string DeleteSql, UpdateSql, AddSql;

            //获取表dt的变化，如果为null，则返回false
            DataTable cdt = dt.GetChanges();
            if (cdt == null) return false;
            int columns = cdt.Columns.Count;

            //if((cdt.Rows[0]["LH_DATE", DataRowVersion.Original].GetType()) == typeof(DateTime))
            //{
            //    DeleteSql = "你好";
            //}
            //获取date和datetime
            //string  dd=cdt.Rows[0]["LH_DATE", DataRowVersion.Original].ToString();
            //DeleteSql = "delete from " + dt.ToString() + " where LH_DATE='" + dd+"'";
            //sql_list.Add(DeleteSql);
            //DataAccessHelper.ExecuteBatchSql(sql_list);

            //获取表的主键
            DataColumn[] PrimaryCols = dt.PrimaryKey;

            for (i = 0; i < cdt.Rows.Count; i++)
            {
                if (cdt.Rows[i].RowState == DataRowState.Deleted)
                {
                    DBPrimary = "";
                    if (PrimaryCols.Length >= 1) //有主键
                    {
                        string pname1 = PrimaryCols[0].ToString();
                        if ((cdt.Rows[i][pname1, DataRowVersion.Original].GetType()) == typeof(string) || (cdt.Rows[i][pname1, DataRowVersion.Original].GetType()) == typeof(DateTime))
                        {
                            DBPrimary += pname1 + "='" + cdt.Rows[i][pname1, DataRowVersion.Original].ToString() + "'";
                        }
                        //else if ((cdt.Rows[i][pname1, DataRowVersion.Original].GetType()) == typeof(DateTime))
                        //{
                        //    DBPrimary +="";
                        //}
                        else
                        {
                            DBPrimary += PrimaryCols[0] + "=" + cdt.Rows[i][pname1, DataRowVersion.Original].ToString();
                        }
                        //if (i == 0)
                        //{
                        //    for (k = 0; k < cdt.Columns.Count; k++)
                        //    {
                        //        string pname1 = PrimaryCols[0].ToString();
                        //        if (cdt.Columns[k].ColumnName == PrimaryCols[0].ToString())
                        //        {
                        //            if ((cdt.Rows[i][pname1, DataRowVersion.Original].GetType()) == typeof(string))
                        //            {
                        //                DBPrimary += pname1 + "='" + cdt.Rows[i][pname1, DataRowVersion.Original].ToString() + "'";
                        //            }
                        //            else
                        //            {
                        //                DBPrimary += PrimaryCols[0] + "=" + cdt.Rows[i][pname1, DataRowVersion.Original].ToString();
                        //            }
                        //        }
                        //    }
                        //}

                        for (j = 1; j < PrimaryCols.Length; j++)
                        {
                            string pname2 = PrimaryCols[j].ToString();
                            for (k = 0; k < cdt.Columns.Count; k++)
                            {
                                if (cdt.Columns[k].ColumnName == PrimaryCols[j].ToString())
                                {
                                    if ((cdt.Rows[i][pname2, DataRowVersion.Original].GetType()) == typeof(string) || (cdt.Rows[i][pname2, DataRowVersion.Original].GetType()) == typeof(DateTime))
                                    {
                                        DBPrimary += "and " + PrimaryCols[j] + "='" + cdt.Rows[i][pname2, DataRowVersion.Original].ToString() + "'";
                                    }
                                    else
                                    {
                                        DBPrimary += "and " + PrimaryCols[j] + "=" + cdt.Rows[i][pname2, DataRowVersion.Original].ToString();
                                    }
                                }
                            }
                        }
                        DeleteSql = "delete from " + dt.ToString() + " where " + DBPrimary;
                        sql_list.Add(DeleteSql);
                    }
                    else //没有主键的情况-----再次从数据库中加载原来的表
                    {
                        string DBsql = "";
                        k = 0;
                        //if (i == 0)
                        {
                            string name0 = cdt.Columns[0].ToString();
                            if ((cdt.Rows[i][name0, DataRowVersion.Original].GetType()) == typeof(string) || (cdt.Rows[i][name0, DataRowVersion.Original].GetType()) == typeof(DateTime))
                            {
                                DBsql += name0 + "='" + cdt.Rows[i][name0, DataRowVersion.Original].ToString() + "'";

                            }
                            else
                            {
                                DBsql += name0 + "=" + cdt.Rows[i][name0, DataRowVersion.Original].ToString();

                            }
                        }
                        for (j = 1; j < cdt.Columns.Count; j++)
                        {
                            string name = cdt.Columns[j].ToString();
                            if ((cdt.Rows[i][name, DataRowVersion.Original].GetType()) == typeof(string) || (cdt.Rows[i][name, DataRowVersion.Original].GetType()) == typeof(DateTime))
                            {
                                DBsql += " and " + name + "='" + cdt.Rows[i][name, DataRowVersion.Original].ToString() + "'";
                            }
                            else
                            {
                                DBsql += " and " + name + "=" + cdt.Rows[i][name, DataRowVersion.Original].ToString();
                            }
                        }
                        DeleteSql = "delete from " + dt.ToString() + " where " + DBsql;
                        sql_list.Add(DeleteSql);
                    }

                }
                else if (cdt.Rows[i].RowState == DataRowState.Modified)
                {
                    DBPrimary = "";
                    if ((cdt.Rows[i][0].GetType()) == typeof(string) || (cdt.Rows[i][0].GetType()) == typeof(DateTime))
                    {
                        setsql = cdt.Columns[0].ToString() + "='" + cdt.Rows[i][0].ToString() + "'";
                    }
                    else
                    {
                        setsql = cdt.Columns[0].ToString() + "=" + cdt.Rows[i][0];
                    }

                    if (PrimaryCols.Length >= 1)
                    {
                        DBPrimary = "";
                        if (PrimaryCols.Length >= 1) //有主键
                        {
                            string pname1 = PrimaryCols[0].ToString();

                            if ((cdt.Rows[i][pname1, DataRowVersion.Original].GetType()) == typeof(string) || (cdt.Rows[i][pname1, DataRowVersion.Original].GetType()) == typeof(DateTime))
                            {
                                DBPrimary += pname1 + "='" + cdt.Rows[i][pname1, DataRowVersion.Original].ToString() + "'";
                            }
                            else
                            {
                                DBPrimary += PrimaryCols[0] + "=" + cdt.Rows[i][pname1, DataRowVersion.Original].ToString();
                            }
                            // if (i == 0)
                            //{
                            //    for (k = 0; k < cdt.Columns.Count; k++)
                            //    {
                            //        string pname1 = PrimaryCols[0].ToString();
                            //        if (cdt.Columns[k].ColumnName == PrimaryCols[0].ToString())
                            //        {
                            //            if ((cdt.Rows[i][pname1, DataRowVersion.Original].GetType()) == typeof(string))
                            //            {
                            //                DBPrimary += pname1 + "='" + cdt.Rows[i][pname1, DataRowVersion.Original].ToString() + "'";

                            //            }
                            //            else
                            //            {
                            //                DBPrimary += PrimaryCols[0] + "=" + cdt.Rows[i][pname1, DataRowVersion.Original].ToString();

                            //            }
                            //        }
                            //    }
                            //}

                            for (j = 1; j < PrimaryCols.Length; j++)
                            {
                                string pname2 = PrimaryCols[j].ToString();
                                for (k = 0; k < cdt.Columns.Count; k++)
                                {
                                    if (cdt.Columns[k].ColumnName == PrimaryCols[j].ToString())
                                    {

                                        if ((cdt.Rows[i][pname2, DataRowVersion.Original].GetType()) == typeof(string) || (cdt.Rows[i][pname2, DataRowVersion.Original].GetType()) == typeof(DateTime))
                                        {
                                            DBPrimary += "and " + PrimaryCols[j] + "='" + cdt.Rows[i][pname2, DataRowVersion.Original].ToString() + "'";
                                        }
                                        else
                                        {
                                            DBPrimary += "and " + PrimaryCols[j] + "=" + cdt.Rows[i][pname2, DataRowVersion.Original].ToString();
                                        }
                                    }
                                }
                            }
                            //遍历cdt的每一行，把这一行的数据写入数据库对应的行
                            for (m = 1; m < cdt.Columns.Count; m++)
                            {
                                if ((cdt.Rows[i][m].GetType()) == typeof(string) || (cdt.Rows[i][m].GetType()) == typeof(DateTime))
                                {
                                    setsql += "," + cdt.Columns[m].ToString() + "='" + cdt.Rows[i][m].ToString() + "'";
                                }
                                else
                                {
                                    setsql += "," + cdt.Columns[m].ToString() + "=" + cdt.Rows[i][m];
                                }
                            }
                            UpdateSql = "update " + dt.ToString() + " set " + setsql + " where " + DBPrimary;
                            sql_list.Add(UpdateSql);
                        }
                    }
                    else //没有主键的情况
                    {
                        string DBsql = "";
                        k = 0;
                        //if (i == 0)
                        {
                            string name0 = cdt.Columns[0].ToString();
                            if ((cdt.Rows[i][name0, DataRowVersion.Original].GetType()) == typeof(string) || (cdt.Rows[i][name0, DataRowVersion.Original].GetType()) == typeof(DateTime))
                            {
                                DBsql += name0 + "='" + cdt.Rows[i][name0, DataRowVersion.Original].ToString() + "'";
                            }
                            else
                            {
                                DBsql += name0 + "=" + cdt.Rows[i][name0, DataRowVersion.Original].ToString();
                            }

                        }
                        for (j = 1; j < cdt.Columns.Count; j++)
                        {
                            string name = cdt.Columns[j].ToString();
                            if ((cdt.Rows[i][name, DataRowVersion.Original].GetType()) == typeof(string) || (cdt.Rows[i][name, DataRowVersion.Original].GetType()) == typeof(DateTime))
                            {
                                DBsql += " and " + name + "='" + cdt.Rows[i][name, DataRowVersion.Original].ToString() + "'";
                            }
                            else
                            {
                                DBsql += " and " + name + "=" + cdt.Rows[i][name, DataRowVersion.Original].ToString();
                            }
                        }
                        for (m = 1; m < cdt.Columns.Count; m++)
                        {
                            if ((cdt.Rows[i][m].GetType()) == typeof(string) || (cdt.Rows[i][m].GetType()) == typeof(DateTime))
                            {
                                setsql += "," + cdt.Columns[m].ToString() + "='" + cdt.Rows[i][m].ToString() + "'";
                            }
                            else
                            {
                                setsql += "," + cdt.Columns[m].ToString() + "=" + cdt.Rows[i][m];
                            }
                        }
                        UpdateSql = "update " + dt.ToString() + " set " + setsql + " where " + DBsql;
                        sql_list.Add(UpdateSql);
                    }
                }
                else if (cdt.Rows[i].RowState == DataRowState.Added)
                {
                    value_string = "";
                    for (j = 0; j < columns - 1; j++)
                    {
                        if ((cdt.Rows[i][j].GetType()) == typeof(string) || (cdt.Rows[i][j].GetType()) == typeof(DateTime))
                        {
                            value_string += "'" + cdt.Rows[i][j] + "',";
                        }
                        else
                        {
                            value_string += cdt.Rows[i][j] + ",";
                        }
                    }
                    AddSql = "insert into " + dt.ToString() + " values(" + value_string + "'" + cdt.Rows[i][columns - 1] + "')";
                    sql_list.Add(AddSql);
                }    
            }
            DataAccessHelper.ExecuteBatchSql(sql_list);
            return true;
        }   

        #endregion
        private void button4_Click(object sender, EventArgs e)
        {
            //List<string> ts = new List<string>();
            //ts.Add("lh_test");
            //ts.Add("fm_trade_goods");
            //DataSet ds = DataAccessHelper.GetDataTables(ts);

            //DataTable dt = ds.Tables["lh_test"];

            //DataRow[] drs = dt.Select("LH_ID=4 or LH_ID=5");

            //DataRow dr = dt.NewRow();



            //DataRow[] ddrs = dt.Select(null, null, DataViewRowState.ModifiedCurrent);

            //List<string> ts = new List<string>();
            //ts.Add("lh_test");
            //ts.Add("fm_trade_goods");
            //DataSet ds = DataAccessHelper.GetDataTables(ts);

            //DataTable dt1 = ds.Tables["lh_test"];

            // bool result1 = MySynchronize(dt1);
            //bool result2=MySynchronizeDB(dt1);

            // UpdateLH();

            UpDateLH_2();

        }

        private void button5_Click(object sender, EventArgs e)
        {

            DbWebSvc.DbWebSvcSoapClient dbsc = new DbWebSvc.DbWebSvcSoapClient();
            string[] ts = new string[2];
            ts[0] = "lh_test";
            ts[1] = "lh_name";
            DataSet dss = dbsc.GetDataTables(ts);

            DataTable dt1 = dss.Tables["lh_test"];
            string type1 = dt1.Columns["LH_DATE"].DataType.GetType().ToString();
            string type2 = dt1.Columns["LH_DATETIME"].DataType.GetType().ToString();
            //DataColumn[] cols1 = new DataColumn[1];
            //cols1[0] = dt1.Columns["LH_ID"];
            //dt1.PrimaryKey = cols1;

            DataTable dt2 = dss.Tables["lh_name"];
            DataColumn[] cols2 = new DataColumn[2];
            cols2[0] = dt2.Columns["FIRSTNAME"];
            cols2[1] = dt2.Columns["LASTNAME"];
            dt2.PrimaryKey = cols2;
            ////将列A与列B作为dt的联合主键DataColumn[] cols = new DataColumn[] { dt_smartgrid.Columns["A"], dt_smartgrid.Columns["B"] };

            //for(int i=0;i<2;i++)
            //{
            //    DataRow dd = dt1.NewRow();
            //    dd[0] = i + 77;
            //    dd[1] = "测试";
            //    dd[2] = "23333";
            //    dt1.Rows.Add(dd);
            //}

            //DataRow[] drs2 = dt1.Select("LH_NAME='CRSCD'");
            //drs2[0].Delete();
            //drs2[2].Delete();

            DataRow[] drs3 = dt1.Select("LH_TYPE='CCC'");
            drs3[0]["LH_DATE"] = "2009-09-09";
            drs3[1]["LH_DATETIME"] = "2009-09-09 09:09:09";

            //for (int i = 0; i < 2; i++)
            //{
            //    DataRow dd = dt2.NewRow();
            //    dd[0] = "王麻子";
            //    dd[1] = "测试" +i;
            //    dd[2] = "23333";
            //    dt2.Rows.Add(dd);
            //}

            //DataRow[] drs2 = dt2.Select("LASTNAME='四'");
            //drs2[0].Delete();
            //drs2[1].Delete();

            //DataRow[] drs4 = dt2.Select("FIRSTNAME='李'");
            //drs4[0]["age"] = 999;
            //drs4[1]["LASTNAME"] = "lastname";

            bool mytest = MySynchronizeDB(dt1); 
        }
    }
}
