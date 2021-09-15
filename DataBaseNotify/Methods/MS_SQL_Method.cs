using Dapper;
using DataBaseNotify.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;

namespace DataBaseNotify.Methods
{
    public class MS_SQL_Method
    {
        /// <summary>
        /// server資料庫連結資訊
        /// </summary>
        public SqlConnectionStringBuilder Serverscsb { get; set; }
        /// <summary>
        /// 資料庫連結
        /// </summary>
        /// <param name="setting">資料庫資訊</param>
        public MS_SQL_Method(DataBaseSetting setting)
        {
            Serverscsb = new SqlConnectionStringBuilder()
            {
                DataSource = setting.DataSource,
                InitialCatalog = "master",
                UserID = setting.UserID,
                Password = setting.Password,
            };
        }
        #region 查詢資料庫
        /// <summary>
        /// 查詢資料庫
        /// </summary>
        /// <returns></returns>
        public List<string> Search_DataBase()
        {
            List<string> DataBaseName = new List<string>();
            try
            {
                string sql = string.Empty;
                using (var conn = new SqlConnection(Serverscsb.ConnectionString))
                {
                    sql = $"SELECT * FROM master.dbo.sysdatabases";
                    var data = conn.Query(sql).ToList();
                    foreach (var Dataitem in data)
                    {
                        foreach (KeyValuePair<string, object> item in Dataitem)
                        {
                            if (item.Key == "name")
                            {
                                DataBaseName.Add($"{item.Value}");
                            }
                        }

                    }
                }
                return DataBaseName;
            }
            catch (Exception ex) { Log.Error(ex, "無 MSSQL Server"); return null; }
        }
        #endregion
        #region 查詢資料表
        /// <summary>
        /// 查詢資料表
        /// </summary>
        /// <param name="DataBaseName">資料庫名稱</param>
        /// <returns></returns>
        public List<string> Search_Tables(string DataBaseName)
        {
            List<string> TableName = new List<string>();
            try
            {
                string sql = string.Empty;
                using (var conn = new SqlConnection(Serverscsb.ConnectionString))
                {
                    sql = $"USE {DataBaseName} select Table_name from INFORMATION_SCHEMA.TABLES";
                    var data = conn.Query(sql).ToList();
                    foreach (var Dataitem in data)
                    {
                        foreach (KeyValuePair<string, object> item in Dataitem)
                        {

                            TableName.Add($"{item.Value}");
                        }

                    }
                }
                return TableName;
            }
            catch (Exception ex) { Log.Error(ex, $"{DataBaseName} 無 資料表"); return null; }
        }
        #endregion
        #region 查詢欄位
        /// <summary>
        /// 查詢欄位
        /// </summary>
        /// <param name="DataBaseName">資料庫名稱</param>
        /// <param name="TableName">資料表名稱</param>
        /// <returns></returns>
        public List<object> Search_Field(string DataBaseName, string TableName)
        {
            List<object> FieldInformation = new List<object>();
            try
            {
                string sql = string.Empty;
                using (var conn = new SqlConnection(Serverscsb.ConnectionString))
                {
                    sql = $"USE {DataBaseName} SELECT COLUMN_NAME,DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{TableName}'";
                    FieldInformation = conn.Query<object>(sql).ToList();
                }
                return FieldInformation;
            }
            catch (Exception ex) { Log.Error(ex, $"{DataBaseName}.{TableName} 無 欄位"); return null; }
        }
        #endregion
        #region 查詢資料
        /// <summary>
        /// 查詢資料
        /// </summary>
        /// <param name="DataBaseName">資料庫名稱</param>
        /// <param name="TableName">資料表名稱</param>
        /// <param name="FieldName">欄位名稱</param>
        /// <param name="TimeName">時間名稱</param>
        /// <param name="TimeTypeEnum">時間欄位類型</param>
        /// <returns></returns>
        public double? Search_Data(string DataBaseName, string TableName, string FieldName, string TimeName, string TimeTypeEnum)
        {
            if (TimeName != "" && TimeName != null)
            {
                double? DataValue = null;
                try
                {
                    string sql = string.Empty;
                    DateTime dateTime = DateTime.Now;
                    using (var conn = new SqlConnection(Serverscsb.ConnectionString))
                    {
                        if (TimeTypeEnum == "datetime")
                        {
                            sql = $"USE {DataBaseName} SELECT {FieldName} FROM {TableName} WHERE {TimeName} >= '{dateTime:yyyy/MM/dd HH:mm:00}'";
                        }
                        else
                        {
                            sql = $"USE {DataBaseName} SELECT {FieldName} FROM {TableName} WHERE {TimeName} >= '{dateTime:yyyyMMddHHmm00}'";
                        }
                        var data = conn.Query<double>(sql).ToList();
                        if (data.Count > 0)
                        {
                            DataValue = data[0];
                            return DataValue;
                        }
                        else
                        {
                            return null;    
                        }
                    }
                }
                catch (ThreadAbortException) { return null; }
                catch (Exception ex) { Log.Error(ex, $"{DataBaseName}.{TableName}的{FieldName} 無 數值"); return null; }
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
