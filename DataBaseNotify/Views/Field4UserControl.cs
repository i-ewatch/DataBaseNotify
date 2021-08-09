using DataBaseNotify.Configuration;
using DataBaseNotify.Methods;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseNotify.Views
{
    public partial class Field4UserControl : DevExpress.XtraEditors.XtraUserControl
    {
        #region Loading
        /// <summary>
        /// 關閉Loading視窗
        /// </summary>
        /// <param name="handle"></param>
        public void CloseProgressPanel(IOverlaySplashScreenHandle handle)
        {
            if (handle != null)
                SplashScreenManager.CloseOverlayForm(handle);
        }
        /// <summary>
        /// Loading物件繼承
        /// </summary>
        public IOverlaySplashScreenHandle handle { get; set; } = null;
        #endregion
        #region Configuration
        /// <summary>
        /// 資料庫資訊
        /// </summary>
        public DataBaseSetting DataBaseSetting { get; set; }
        /// <summary>
        /// 簡訊資訊
        /// </summary>
        public List<TelePhoneSetting> TelePhoneSettings { get; set; }
        /// <summary>
        /// Line資訊
        /// </summary>
        public List<LineNotifySetting> LineNotifySettings { get; set; }
        /// <summary>
        /// Telegram資訊
        /// </summary>
        public List<TelegramBotSetting> TelegramBotSettings { get; set; }
        /// <summary>
        /// 群組資訊
        /// </summary>
        public List<GroupSetting> GroupSettings { get; set; }
        /// <summary>
        /// 系統資訊
        /// </summary>
        public SystemSetting SystemSetting { get; set; }
        /// <summary>
        /// 點位資訊
        /// </summary>
        public List<PointSetting> PointSettings { get; set; }
        /// <summary>
        /// 推播顯示旗標資訊
        /// </summary>
        public NotifyVisible NotifyVisible { get; set; }
        #endregion
        #region Method
        public MS_SQL_Method MS_SQL_Method { get; set; }
        /// <summary>
        /// MySql方法
        /// </summary>
        public My_SQL_Method My_SQL_Method { get; set; }
        /// <summary>
        /// 紀錄功能
        /// </summary>
        public CsvMethod CsvMethod { get; set; } = new CsvMethod();
        #endregion
        /// <summary>
        /// 初始路徑
        /// </summary>
        public string MyWorkPath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 載入數值
        /// </summary>
        public virtual void InitialValue() { }
    }
}
