using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using RIIS.DbParameter;

namespace RIIS.CarManager
{
    class DataAccessHelper
    {
        public static DataTable GetDataTable(string tableName)
        {
            string strSql = "select * from " + tableName;
            return GetDataTableFromSql(strSql);
            
        }

        public static DataSet GetDataTables(List<string> tableNames)
        {
            try
            {
                if (tableNames == null || tableNames.Count == 0)
                    return null;
                DbWebSvc.DbWebSvcSoapClient dbsc = new DbWebSvc.DbWebSvcSoapClient();
                DataSet ds = dbsc.GetDataTables(tableNames.ToArray());
                if (ds == null || ds.Tables.Count == 0)
                    return null;
                else
                    return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static DataTable GetDataTableFromSql(string strSql)
        {
            try
            {
                DbWebSvc.DbWebSvcSoapClient dbsc = new DbWebSvc.DbWebSvcSoapClient();
                DataSet ds = dbsc.GetDataFromSql(strSql);
                if (ds == null || ds.Tables.Count == 0)
                    return null;
                else
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public static void ExecuteSql(string strSql)
        {
            try
            {
                DbWebSvc.DbWebSvcSoapClient dbsc = new DbWebSvc.DbWebSvcSoapClient();
                dbsc.ExecuteSql(strSql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void ExecuteBatchSql(List<string> sqlList)
        {
            try
            {
                DbWebSvc.DbWebSvcSoapClient dbsc = new DbWebSvc.DbWebSvcSoapClient();
                dbsc.ExecuteBatchSQL(sqlList.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void InsertWithParams(string tableName, DbParam[] dbParams)
        {
            try
            {
                

                DbWebSvc.DbWebSvcSoapClient dbsc = new DbWebSvc.DbWebSvcSoapClient();
                dbsc.InsertWithParam(tableName, SerializeObject(dbParams));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void UpdateWithParams(string tableName, DbParam[] dbParams)
        {
            try
            {
                //DbWebSvc.MySqlParameter[] ops = GetMySqlParameters(dbParams);
                DbWebSvc.DbWebSvcSoapClient dbsc = new DbWebSvc.DbWebSvcSoapClient();
                dbsc.UpdateWithParam(tableName, SerializeObject(dbParams));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void UpdateWithParams_2(string tableName, DbParam[] dbParams, int keyNum)
        {
            try
            {
                //DbWebSvc.MySqlParameter[] ops = GetMySqlParameters(dbParams);
                DbWebSvc.DbWebSvcSoapClient dbsc = new DbWebSvc.DbWebSvcSoapClient();
                dbsc.UpdateWithParam_2(tableName, SerializeObject(dbParams), keyNum);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void ExecuteSP(string procName, DbParam[] dbParams)
        {
            try
            {
                //DbWebSvc.MySqlParameter[] ops = GetMySqlParameters(dbParams);
                DbWebSvc.DbWebSvcSoapClient dbsc = new DbWebSvc.DbWebSvcSoapClient();
                dbsc.ExecuteStoredProcedure(procName, SerializeObject(dbParams));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public static object DeserializeObject(byte[] pBytes)
        {
            object newOjb = null;
            if (pBytes == null)
            {
                return newOjb;
            }

            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(pBytes);
            memoryStream.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            newOjb = formatter.Deserialize(memoryStream);
            memoryStream.Close();

            return newOjb;
        }

        public static byte[] SerializeObject(object pObj)
        {
            if (pObj == null)
                return null;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, pObj);
            memoryStream.Position = 0;
            byte[] read = new byte[memoryStream.Length];
            memoryStream.Read(read, 0, read.Length);
            memoryStream.Close();
            return read;

        }
    } 
}
