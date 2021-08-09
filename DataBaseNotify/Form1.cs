using DataBaseNotify.Configuration;
using DataBaseNotify.Enums;
using DataBaseNotify.Methods;
using DataBaseNotify.Modules;
using DataBaseNotify.Views;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DataBaseNotify
{
    public partial class Form1 : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
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
        #region Views
        /// <summary>
        /// 切換畫面使用
        /// </summary>
        private NavigationFrame navigationFrame { get; set; }
        /// <summary>
        /// 畫面總物件
        /// </summary>
        private List<Field4UserControl> field4UserControls { get; set; } = new List<Field4UserControl>();
        /// <summary>
        /// 首頁畫面
        /// </summary>
        private HomeUserControl homeUserControl { get; set; }
        /// <summary>
        /// 報表畫面
        /// </summary>
        private ReportUserControl reportUserControl { get; set; }
        /// <summary>
        /// 搜尋DataBase點位畫面
        /// </summary>
        private SearchUserControl SearchUserControl { get; set; }
        /// <summary>
        /// 群組設定畫面
        /// </summary>
        private GroupUserControl groupUserControl { get; set; }
        /// <summary>
        /// 推播設定
        /// </summary>
        private SettingUserControl settingUserControl { get; set; }
        /// <summary>
        /// 攔位設定畫面
        /// </summary>
        private PointUserControl PointUserControl { get; set; }
        #endregion
        #region Method
        private MS_SQL_Method MS_SQL_Method { get; set; } = null;
        /// <summary>
        /// MySql方法
        /// </summary>
        private My_SQL_Method My_SQL_Method { get; set; } = null;
        /// <summary>
        /// 紀錄功能
        /// </summary>
        private CsvMethod CsvMethod { get; set; } = new CsvMethod();
        #endregion
        /// <summary>
        /// 軟體被開啟旗標
        /// </summary>
        private bool OpenFlag { get; set; }
        /// <summary>
        /// 登入權限
        /// <para>0 = 登出</para>
        /// <para>1 = 客戶</para>
        /// <para>2 = 員工</para>
        /// </summary>
        public int LogFlag { get; set; }
        /// <summary>
        /// 授權時間
        /// </summary>
        public DateTime TokenTime { get; set; }
        /// <summary>
        /// 授權關機
        /// </summary>
        public bool TokenCloseForm { get; set; }
        /// <summary>
        /// 授權位置
        /// </summary>
        private string TokenPath = $"C:\\ProgramData\\DataBaseNotify.dll";
        public Form1()
        {
            InitializeComponent();
            #region 禁止軟體重複開啟功能
            //string ProcessName = Process.GetCurrentProcess().ProcessName;
            //Process[] p = Process.GetProcessesByName(ProcessName);
            //if (p.Length > 1)
            //{
            //    FlyoutAction action = new FlyoutAction();
            //    action.Caption = "軟體錯誤";
            //    action.Description = "重複開啟!";
            //    action.Commands.Add(FlyoutCommand.OK);
            //    FlyoutDialog.Show(FindForm().FindForm(), action);
            //    OpenFlag = true;
            //    Environment.Exit(1);
            //}
            #endregion
            if (!OpenFlag)
            {
                #region Serilog initial
                Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}\\log\\log-.txt",
                                          rollingInterval: RollingInterval.Day,
                                          outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                            .CreateLogger();        //宣告Serilog初始化
                #endregion
                #region Load Configuration
                DataBaseSetting = InitialMethod.Load_DataBase();
                TelePhoneSettings = InitialMethod.Load_Telephone();
                LineNotifySettings = InitialMethod.Load_LineNotify();
                TelegramBotSettings = InitialMethod.Load_Telegram();
                GroupSettings = InitialMethod.Load_Group();
                SystemSetting = InitialMethod.Load_System();
                PointSettings = InitialMethod.Load_Point();
                NotifyVisible = InitialMethod.Load_NotifyVisible();
                #endregion
                #region Method
                DataBaseTypeEnum dataBaseTypeEnum = (DataBaseTypeEnum)DataBaseSetting.DataBaseTypeEnum;
                switch (dataBaseTypeEnum)
                {
                    case DataBaseTypeEnum.MS_SQL:
                        {
                            MS_SQL_Method = new MS_SQL_Method(DataBaseSetting);
                        }
                        break;
                    case DataBaseTypeEnum.MY_SQL:
                        {
                            My_SQL_Method = new My_SQL_Method(DataBaseSetting);
                        }
                        break;
                }
                #endregion
                LoginbarButtonItem.ImageOptions.Image = imageCollection1.Images[0];
                for (int i = 2; i < accordionControl1.Elements.Count; i++)
                {
                    accordionControl1.Elements[i].Enabled = false;
                }
                accordionControl1.AllowItemSelection = true;
                #region Views
                navigationFrame = new NavigationFrame() { Dock = DockStyle.Fill, Parent = DisplaypanelControl };

                homeUserControl = new HomeUserControl(My_SQL_Method, MS_SQL_Method, this);
                navigationFrame.AddPage(homeUserControl);
                field4UserControls.Add(homeUserControl);

                reportUserControl = new ReportUserControl();
                navigationFrame.AddPage(reportUserControl);
                field4UserControls.Add(reportUserControl);

                SearchUserControl = new SearchUserControl(My_SQL_Method, MS_SQL_Method);
                navigationFrame.AddPage(SearchUserControl);
                field4UserControls.Add(SearchUserControl);

                PointUserControl = new PointUserControl();
                navigationFrame.AddPage(PointUserControl);
                field4UserControls.Add(PointUserControl);

                groupUserControl = new GroupUserControl();
                navigationFrame.AddPage(groupUserControl);
                field4UserControls.Add(groupUserControl);

                settingUserControl = new SettingUserControl();
                navigationFrame.AddPage(settingUserControl);
                field4UserControls.Add(settingUserControl);
                #endregion
                accordionControl1.ElementClick += (s, e) =>
                {
                    if (e.Element.Style == ElementStyle.Group) return;
                    if (e.Element.Tag == null) return;
                    int Index = Convert.ToInt32(e.Element.Tag);
                    navigationFrame.SelectedPageIndex = Index;
                    field4UserControls[Index].InitialValue();
                };
                accordionControl1.SelectedElement = accordionControl1.Elements[0];
                MessageModule message = new MessageModule()
                {
                    PointTypeEnum = 0,
                    DataBaseNum = 0,
                    FieldNum = 0,
                    DateTime = DateTime.Now,
                    FieldName = "軟體狀態",
                    Description = $"開啟"
                };
                CsvMethod.Save_Csv("軟體視窗", message);
                #region 授權按鈕
                TokenbarButtonItem.ImageOptions.Image = imageCollection1.Images["Key.png"];
                TokenbarButtonItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                Tokentimer.Interval = 1000;
                if (File.Exists(TokenPath))
                {
                    TokenCloseForm = true;
                    Text = "資料庫簡訊發報系統";
                }
                else
                {
                    Text = "資料庫簡訊發報系統(二小時試用版)";
                }
                if (!TokenCloseForm)
                {
                    TokenTime = DateTime.Now;
                    Tokentimer.Enabled = true;
                }
                #endregion
            }
        }
        #region 通訊開關關(閉按鈕功能)
        public void Component_Action(bool Flag)
        {
            if (Flag)
            {
                navigationFrame.SelectedPageIndex = 0;
                switch (LogFlag)
                {
                    case 1:
                        {
                            for (int i = 3; i < accordionControl1.Elements.Count; i++)
                            {
                                accordionControl1.Elements[i].Enabled = false;
                            }
                        }
                        break;
                    case 2:
                        {
                            for (int i = 2; i < accordionControl1.Elements.Count; i++)
                            {
                                accordionControl1.Elements[i].Enabled = false;
                            }
                        }
                        break;
                }
            }
            else
            {
                switch (LogFlag)
                {
                    case 1:
                        {
                            for (int i = 3; i < accordionControl1.Elements.Count; i++)
                            {
                                accordionControl1.Elements[i].Enabled = true;
                            }
                        }
                        break;
                    case 2:
                        {
                            for (int i = 2; i < accordionControl1.Elements.Count; i++)
                            {
                                accordionControl1.Elements[i].Enabled = true;
                            }
                        }
                        break;
                }
            }
        }
        #endregion
        #region 軟體視窗關閉
        /// <summary>
        /// 軟體視窗關閉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!TokenCloseForm)
            {
                MessageModule message = new MessageModule()
                {
                    PointTypeEnum = 0,
                    DataBaseNum = 0,
                    FieldNum = 0,
                    DateTime = DateTime.Now,
                    FieldName = "通訊狀態",
                    Description = $"授權時間結束通訊關閉"
                };
                CsvMethod.Save_Csv("設備通訊", message);

            }
            Tokentimer.Enabled = false;
            UserControl control = new UserControl() { Size = new Size(300, 100) };

            SimpleButton cancelButton = new SimpleButton() { Dock = DockStyle.Right, Text = "Cancel", Size = new Size(150, 30) };
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Parent = control;
            SimpleButton okButton = new SimpleButton() { Dock = DockStyle.Left, Text = "Ok", Size = new Size(150, 30) };
            okButton.DialogResult = DialogResult.OK;
            okButton.Parent = control;

            LabelControl labelControl = new LabelControl() { Dock = DockStyle.Top };
            labelControl.Text = "Password";
            labelControl.Appearance.Font = new Font("Tahoma", 28);

            TextEdit textEdit = new TextEdit() { Dock = DockStyle.Top };
            textEdit.Properties.UseSystemPasswordChar = true;
            textEdit.Parent = control;
            labelControl.Parent = control;

            if (FlyoutDialog.Show(FindForm(), control) == DialogResult.OK)
            {
                if (textEdit.Text == "quit")
                {
                    homeUserControl.Close_Thread();
                    MessageModule message = new MessageModule()
                    {
                        PointTypeEnum = 0,
                        DataBaseNum = 0,
                        FieldNum = 0,
                        DateTime = DateTime.Now,
                        FieldName = "軟體狀態",
                        Description = $"關閉"
                    };
                    CsvMethod.Save_Csv("軟體視窗", message);
                    if (e != null)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        Dispose();
                    }
                }
                else
                {
                    if (e != null)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        Dispose();
                    }
                }
            }
            else
            {
                if (e != null)
                {
                    e.Cancel = true;
                }
                else
                {
                    Dispose();
                }
            }
        }
        #endregion
        #region 登入按鈕
        /// <summary>
        /// 登入按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginbarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch (LogFlag)
            {
                case 0:
                    {
                        UserControl control = new UserControl() { Size = new Size(300, 100) };

                        SimpleButton cancelButton = new SimpleButton() { Dock = DockStyle.Right, Text = "Cancel", Size = new Size(150, 30) };
                        cancelButton.DialogResult = DialogResult.Cancel;
                        cancelButton.Parent = control;
                        SimpleButton okButton = new SimpleButton() { Dock = DockStyle.Left, Text = "Ok", Size = new Size(150, 30) };
                        okButton.DialogResult = DialogResult.OK;
                        okButton.Parent = control;

                        LabelControl labelControl = new LabelControl() { Dock = DockStyle.Top };
                        labelControl.Text = "Password";
                        labelControl.Appearance.Font = new Font("Tahoma", 28);

                        TextEdit textEdit = new TextEdit() { Dock = DockStyle.Top };
                        textEdit.Properties.UseSystemPasswordChar = true;
                        textEdit.Parent = control;
                        labelControl.Parent = control;

                        if (FlyoutDialog.Show(FindForm(), control) == DialogResult.OK)
                        {
                            if (textEdit.Text == "admin")
                            {
                                LogFlag = 1;
                                LoginbarButtonItem.ImageOptions.Image = imageCollection1.Images[1];
                                LoginbarButtonItem.Hint = "管理使用者";
                                for (int i = 3; i < accordionControl1.Elements.Count; i++)
                                {
                                    accordionControl1.Elements[i].Enabled = true;
                                }
                                MessageModule message = new MessageModule()
                                {
                                    PointTypeEnum = 0,
                                    DataBaseNum = 0,
                                    FieldNum = 0,
                                    DateTime = DateTime.Now,
                                    FieldName = "管理使用者",
                                    Description = $"登入"
                                };
                                CsvMethod.Save_Csv("帳戶功能", message);
                            }
                            else if (textEdit.Text == "root" || textEdit.Text == "ewatch")
                            {
                                if (!TokenCloseForm)
                                {
                                    TokenbarButtonItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                }
                                LogFlag = 2;
                                LoginbarButtonItem.ImageOptions.Image = imageCollection1.Images[2];
                                LoginbarButtonItem.Hint = "超級管理使用者";
                                for (int i = 2; i < accordionControl1.Elements.Count; i++)
                                {
                                    accordionControl1.Elements[i].Enabled = true;
                                }
                                MessageModule message = new MessageModule()
                                {
                                    PointTypeEnum = 0,
                                    DataBaseNum = 0,
                                    FieldNum = 0,
                                    DateTime = DateTime.Now,
                                    FieldName = "超級管理使用者",
                                    Description = $"登入"
                                };
                                CsvMethod.Save_Csv("帳戶功能", message);
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        LogFlag = 0;
                        LoginbarButtonItem.ImageOptions.Image = imageCollection1.Images[0];
                        LoginbarButtonItem.Hint = "使用者登入";
                        for (int i = 3; i < accordionControl1.Elements.Count; i++)
                        {
                            accordionControl1.Elements[i].Enabled = false;
                        }
                        MessageModule message = new MessageModule()
                        {
                            PointTypeEnum = 0,
                            DataBaseNum = 0,
                            FieldNum = 0,
                            DateTime = DateTime.Now,
                            FieldName = "管理使用者",
                            Description = $"登出"
                        };
                        CsvMethod.Save_Csv("帳戶功能", message);
                        navigationFrame.SelectedPageIndex = 0;
                    }
                    break;
                case 2:
                    {
                        if (!TokenCloseForm)
                        {
                            TokenbarButtonItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        }
                        LogFlag = 0;
                        LoginbarButtonItem.ImageOptions.Image = imageCollection1.Images[0];
                        LoginbarButtonItem.Hint = "使用者登入";
                        for (int i = 2; i < accordionControl1.Elements.Count; i++)
                        {
                            accordionControl1.Elements[i].Enabled = false;
                        }
                        MessageModule message = new MessageModule()
                        {
                            PointTypeEnum = 0,
                            DataBaseNum = 0,
                            FieldNum = 0,
                            DateTime = DateTime.Now,
                            FieldName = "超級管理使用者",
                            Description = $"登出"
                        };
                        CsvMethod.Save_Csv("帳戶功能", message);
                        navigationFrame.SelectedPageIndex = 0;
                    }
                    break;
            }
        }
        #endregion
        #region 授權倒數
        /// <summary>
        /// 授權倒數
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tokentimer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(TokenTime);
            if (timeSpan.TotalHours >= 2)
            {
                Form1_FormClosing(null, null);
            }
        }
        #endregion
        #region 授權按鈕
        /// <summary>
        /// 授權按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TokenbarButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            TokenCloseForm = true;
            Text = "資料庫簡訊發報系統";
            Tokentimer.Enabled = false;
            string output = "";
            File.WriteAllText(TokenPath, output);
            TokenbarButtonItem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }
        #endregion
    }
}
