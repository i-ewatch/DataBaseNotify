using DataBaseNotify.Configuration;
using DataBaseNotify.Methods;
using DataBaseNotify.Modules;
using DataBaseNotify.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace DataBaseNotify.Components
{
    public class Field4Component : Component
    {
        /// <summary>
        /// MS_SQL
        /// </summary>
        public MS_SQL_Method MS_SQL_Method { get; set; }
        /// <summary>
        /// My_SQL
        /// </summary>
        public My_SQL_Method My_SQL_Method { get; set; }
        /// <summary>
        /// 簡訊發送訊息
        /// </summary>
        public Queue<MessageModule> TelePhoneMessage { get; set; } = new Queue<MessageModule>();
        /// <summary>
        /// Line Notify發送訊息
        /// </summary>
        public Queue<MessageModule> LineNotifyMessage { get; set; } = new Queue<MessageModule>();
        /// <summary>
        /// Telegram Bot發送訊息
        /// </summary>
        public Queue<MessageModule> TelegramMessage { get; set; } = new Queue<MessageModule>();
        /// <summary>
        /// 資料儲存
        /// </summary>
        public CsvMethod CsvMethod { get; set; } = new CsvMethod();
        /// <summary>
        /// 首頁畫面
        /// </summary>
        public HomeUserControl HomeUserControl { get; set; }
        #region DateBase 初始物件
        /// <summary>
        /// AI點位
        /// <para>0 = 正常</para>
        /// <para>1 = 上限值</para>
        /// <para>2 = 下限值</para>
        /// </summary>
        public int[] AIs;
        /// <summary>
        /// DI點位
        /// </summary>
        public bool[] DIs;
        /// <summary>
        /// 狀態類型
        /// </summary>
        public int[] Enumses;
        #endregion
        #region Configuration
        /// <summary>
        /// 點位資訊
        /// </summary>
        public PointSetting PointSetting { get; set; }
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
        /// 總點位資訊
        /// </summary>
        public List<PointSetting> PointSettings { get; set; }
        #endregion 
        #region 初始功能
        /// <summary>
        /// 執行緒
        /// </summary>
        public Thread ComponentThread { get; set; }
        /// <summary>
        /// 時間
        /// </summary>
        public DateTime ComponentTime { get; set; }
        public Field4Component()
        {
            OnMyWorkStateChanged += new MyWorkStateChanged(AfterMyWorkStateChanged);
        }
        protected void WhenMyWorkStateChange()
        {
            OnMyWorkStateChanged?.Invoke(this, null);
        }
        public delegate void MyWorkStateChanged(object sender, EventArgs e);
        public event MyWorkStateChanged OnMyWorkStateChanged;
        /// <summary>
        /// 系統工作路徑
        /// </summary>
        protected readonly string WorkPath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 通訊功能啟動判斷旗標
        /// </summary>
        protected bool myWorkState;
        /// <summary>
        /// 通訊功能啟動旗標
        /// </summary>
        public bool MyWorkState
        {
            get { return myWorkState; }
            set
            {
                if (value != myWorkState)
                {
                    myWorkState = value;
                    WhenMyWorkStateChange();
                }
            }
        }
        /// <summary>
        /// 執行續工作狀態改變觸發事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void AfterMyWorkStateChanged(object sender, EventArgs e) { }
        #endregion
    }
}
