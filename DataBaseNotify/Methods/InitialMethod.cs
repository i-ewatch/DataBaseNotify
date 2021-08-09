using DataBaseNotify.Configuration;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseNotify.Methods
{
    public static class InitialMethod
    {
        /// <summary>
        /// 初始路徑
        /// </summary>
        private static string MyWorkPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        #region 簡訊號碼資訊
        /// <summary>
        /// 載入簡訊號碼資訊
        /// </summary>
        /// <returns></returns>
        public static List<TelePhoneSetting> Load_Telephone()
        {
            List<TelePhoneSetting> setting = new List<TelePhoneSetting>();
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\Telephone.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<List<TelePhoneSetting>>(json);
                }
                else
                {
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " 簡訊號碼資訊載入錯誤");
            }
            return setting;
        }
        /// <summary>
        /// 儲存簡訊號碼資訊
        /// </summary>
        /// <param name="setting">簡訊資訊</param>
        public static void Save_Telephone(List<TelePhoneSetting> setting)
        {
            string SettingPath = $"{MyWorkPath}\\stf\\Telephone.json";
            string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
            File.WriteAllText(SettingPath, output);
        }
        #endregion
        #region Line資訊
        /// <summary>
        /// 載入Line資訊
        /// </summary>
        /// <returns></returns>
        public static List<LineNotifySetting> Load_LineNotify()
        {
            List<LineNotifySetting> setting = new List<LineNotifySetting>();
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\LineNotify.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<List<LineNotifySetting>>(json);
                }
                else
                {
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Line資訊載入錯誤");
            }
            return setting;
        }
        /// <summary>
        /// 儲存Line資訊
        /// </summary>
        /// <param name="setting">Line資訊</param>
        public static void Save_LineNotify(List<LineNotifySetting> setting)
        {
            string SettingPath = $"{MyWorkPath}\\stf\\LineNotify.json";
            string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
            File.WriteAllText(SettingPath, output);
        }
        #endregion
        #region Telegram資訊
        /// <summary>
        /// 載入Telegram資訊
        /// </summary>
        /// <returns></returns>
        public static List<TelegramBotSetting> Load_Telegram()
        {
            List<TelegramBotSetting> setting = new List<TelegramBotSetting>();
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\TelegramBot.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<List<TelegramBotSetting>>(json);
                }
                else
                {
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Telegram資訊載入錯誤");
            }
            return setting;
        }
        /// <summary>
        /// 儲存Telegram資訊
        /// </summary>
        /// <param name="setting">Telegram資訊</param>
        public static void Save_Telegram(List<TelegramBotSetting> setting)
        {
            string SettingPath = $"{MyWorkPath}\\stf\\TelegramBot.json";
            string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
            File.WriteAllText(SettingPath, output);
        }
        #endregion
        #region Group資訊
        /// <summary>
        /// 載入Group資訊
        /// </summary>
        /// <returns></returns>
        public static List<GroupSetting> Load_Group()
        {
            List<GroupSetting> setting = new List<GroupSetting>();
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\Group.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<List<GroupSetting>>(json);
                }
                else
                {
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Group資訊載入錯誤");
            }
            return setting;
        }
        /// <summary>
        /// 儲存Group資訊
        /// </summary>
        /// <param name="setting">Telegram資訊</param>
        public static void Save_Group(List<GroupSetting> setting)
        {
            string SettingPath = $"{MyWorkPath}\\stf\\Group.json";
            string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
            File.WriteAllText(SettingPath, output);
        }
        #endregion
        #region 系統資訊
        /// <summary>
        /// 載入系統資訊
        /// </summary>
        /// <returns></returns>
        public static SystemSetting Load_System()
        {
            SystemSetting setting = null;
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\System.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<SystemSetting>(json);
                }
                else
                {
                    SystemSetting Setting = new SystemSetting()
                    {
                        TelegramFlag = false,
                        TelephoneCOM = "COM3",
                        LineFlag = false,
                        TelephoneFlag = false
                    };
                    setting = Setting;
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " 系統資訊載入錯誤");
            }
            return setting;
        }
        /// <summary>
        /// 儲存系統資訊
        /// </summary>
        /// <param name="setting"></param>
        public static void Save_System(SystemSetting setting)
        {
            string SettingPath = $"{MyWorkPath}\\stf\\System.json";
            string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
            File.WriteAllText(SettingPath, output);
        }
        #endregion
        #region 資料庫資訊
        /// <summary>
        /// 載入資料庫資訊
        /// </summary>
        /// <returns></returns>
        public static DataBaseSetting Load_DataBase()
        {
            DataBaseSetting setting = new DataBaseSetting();
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\DataBase.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<DataBaseSetting>(json);
                }
                else
                {
                    setting.DataBaseTypeEnum = 1;
                    setting.DataSource = "127.0.0.1";
                    setting.UserID = "root";
                    setting.Password = "1234";
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "資料庫資訊載入錯誤");
            }
            return setting;
        }
        /// <summary>
        /// 儲存資料庫資訊
        /// </summary>
        /// <param name="setting">資料庫資訊</param>
        public static void Save_Point(DataBaseSetting setting)
        {
            string SettingPath = $"{MyWorkPath}\\stf\\DataBase.json";
            string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
            File.WriteAllText(SettingPath, output);
        }
        #endregion
        #region 點位資訊
        /// <summary>
        /// 載入點位資訊
        /// </summary>
        /// <returns></returns>
        public static List<PointSetting> Load_Point()
        {
            List<PointSetting> setting = new List<PointSetting>();
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\Point.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<List<PointSetting>>(json);
                }
                else
                {
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " 點位資訊載入錯誤");
            }
            return setting;
        }
        /// <summary>
        /// 儲存點位資訊
        /// </summary>
        /// <param name="setting">簡訊資訊</param>
        public static void Save_Point(List<PointSetting> setting)
        {
            string SettingPath = $"{MyWorkPath}\\stf\\Point.json";
            string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
            File.WriteAllText(SettingPath, output);
        }
        #endregion
        #region 推播顯示旗標資訊
        public static NotifyVisible Load_NotifyVisible()
        {
            NotifyVisible setting = null;
            if (!Directory.Exists($"{MyWorkPath}\\stf"))
                Directory.CreateDirectory($"{MyWorkPath}\\stf");
            string SettingPath = $"{MyWorkPath}\\stf\\NotifyVisible.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<NotifyVisible>(json);
                }
                else
                {
                    NotifyVisible Setting = new NotifyVisible()
                    {
                        TelePhoneFlag = true,
                        LineNotifyFlag = true,
                        TelegramFlag = true
                    };
                    setting = Setting;
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " 推播顯示旗標資訊載入錯誤");
            }
            return setting;
        }
        #endregion
    }
}
