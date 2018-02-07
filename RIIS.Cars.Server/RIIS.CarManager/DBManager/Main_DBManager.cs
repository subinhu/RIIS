using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using RIIS.DbParameter;

namespace RIIS.CarManager
{
    public class Main_DBManager
    {
        public static DataSet DBSet = new DataSet();
        public const string QueueName = "数据处理队列"; 
        public Main_DBManager()
        {
            //加载数据库数据
            
            



            RIIS.MsgQueue.MsgReceiver rcv = RIIS.MsgQueue.MsgQueue.RegisterReceiver(QueueName);   
            rcv.MessageArrived += new RIIS.MsgQueue.MsgReceiver.MessageArrivedEvent(rcv_MessageArrived);
            rcv.Interval = 10000;
            rcv.Tick += new RIIS.MsgQueue.MsgReceiver.TimerEvent(rcv_Tick);
            rcv.Start();
        }

        /// <summary>
        /// 用于处理"数据处理队列"中的数据
        /// </summary>
        /// <param name="host">接收者</param>
        /// <param name="sender">发送者</param>
        /// <param name="msg">消息内容</param>
        /// <param name="bRequest">是否要求有返回值</param>
        /// <returns>返回值</returns>
        object rcv_MessageArrived(RIIS.MsgQueue.MsgReceiver host, RIIS.MsgQueue.MsgSender sender, object msg, bool bRequest)
        {


            return null;
        }

        /// <summary>
        /// 定时任务
        /// </summary>
        /// <param name="host"></param>
        void rcv_Tick(RIIS.MsgQueue.MsgReceiver host)
        {
           
        }


        /// <summary>
        /// 从数据库加载数据到DBSet,旧的DBSet数据将被删除
        /// </summary>
        /// <param name="tables">加载的表的名称列表</param>
        public void LoadTablesFromDB(List<string> tables)
        {
            if (tables == null || tables.Count <= 0)
                return;
            DBSet = DataAccessHelper.GetDataTables(tables);
        }

       
        public void AddTablesFromDB(List<string> tables,bool bReload)
        {
            if (tables == null || tables.Count <= 0)
                return;
            DataTable dt;
            foreach (string na in tables)
            {
                if (DBSet.Tables.Contains(na) && !bReload)
                    continue;
                dt = DataAccessHelper.GetDataTable(na);
                if (dt == null)
                    continue;
                dt.TableName = na;
                if (DBSet.Tables.Contains(na) && bReload)
                {
                    DBSet.Tables.Remove(na);
                }
                DBSet.Tables.Add(na);
            }
        }


        //DBSet中指定的数据表同步到数据库
        public bool SynchronizeToDB(DataTable dt)
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
                        else
                        {
                            DBPrimary += PrimaryCols[0] + "=" + cdt.Rows[i][pname1, DataRowVersion.Original].ToString();
                        }


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









    }
}
