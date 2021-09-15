using Dapper;
using DataBaseNotify.Configuration;
using MySql.Data.MySqlClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DataBaseNotify.Methods
{
    public class My_SQL_Method
    {
        /// <summary>
        /// MariaDB資料庫連結資訊
        /// </summary>
        public MySqlConnectionStringBuilder Serverscsb { get; set; }
        /// <summary>
        /// 資料庫連結
        /// </summary>
        /// <param name="setting">資料庫資訊</param>
        public My_SQL_Method(DataBaseSetting setting)
        {
            Serverscsb = new MySqlConnectionStringBuilder()
            {
                Database = "mysql",
                Server = setting.DataSource,
                UserID = setting.UserID,
                Password = setting.Password,
                CharacterSet = "utf8"
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
                using (var conn = new MySqlConnection(Serverscsb.ConnectionString))
                {
                    sql = $"SHOW DATABASES";
                    var data = conn.Query(sql).ToList();
                    foreach (var Dataitem in data)
                    {
                        foreach (KeyValuePair<string, object> item in Dataitem)
                        {
                            DataBaseName.Add(item.Value.ToString());
                        }
                    }
                }
                return DataBaseName;
            }
            catch (Exception ex) { Log.Error(ex, "無 MySQL Server"); return null; }
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
                using (var conn = new MySqlConnection(Serverscsb.ConnectionString))
                {
                    sql = $"SHOW TABLES FROM {DataBaseName}";
                    var data = conn.Query(sql).ToList();
                    foreach (var Dataitem in data)
                    {
                        foreach (KeyValuePair<string, object> item in Dataitem)
                        {
                            TableName.Add(item.Value.ToString());
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
                using (var conn = new MySqlConnection(Serverscsb.ConnectionString))
                {
                    sql = $"SHOW FIELDS FROM {DataBaseName}.{TableName}";
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
                    using (var conn = new MySqlConnection(Serverscsb.ConnectionString))
                    {
                        if (TimeTypeEnum == "datetime")
                        {
                            sql = $"SELECT {FieldName} FROM {DataBaseName}.{TableName} WHERE {TimeName} >= '{dateTime:yyyy/MM/dd HH:mm:00}'";
                        }
                        else
                        {
                            sql = $"SELECT {FieldName} FROM {DataBaseName}.{TableName} WHERE {TimeName} >= '{dateTime:yyyyMMddHHmm00}'";
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
