using DataBaseNotify.Configuration;
using DataBaseNotify.Methods;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraTab;
using LineNotifyLibrary;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SMSLibrary;
using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using TelegramLibrary;

namespace DataBaseNotify.Views
{
    public partial class SettingUserControl : Field4UserControl
    {
        #region 手機初始物件
        /// <summary>
        /// 目前聚焦手機資訊
        /// </summary>
        public TelePhoneSetting FocusTelephone { get; set; }
        /// <summary>
        /// 手機類型
        /// <para> 0 = Add</para>
        /// <para> 1 = Edit</para>
        /// </summary>
        public int TelephoneIndex { get; set; } = 0;
        /// <summary>
        /// 手機聚焦編號
        /// </summary>
        public int FocusTelephoneIndex { get; set; } = 0;
        #endregion
        #region Line Notify初始物件
        /// <summary>
        /// 目前聚焦Line資訊
        /// </summary>
        public LineNotifySetting FocusLine { get; set; }
        /// <summary>
        /// Line類型
        /// <para> 0 = Add</para>
        /// <para> 1 = Edit</para>
        /// </summary>
        public int LineIndex { get; set; } = 0;
        /// <summary>
        /// Line Notify聚焦編號
        /// </summary>
        public int FocusLineIndex { get; set; } = 0;
        #endregion
        #region Telegram Bot初始物件
        /// <summary>
        /// 目前聚焦Telegram資訊
        /// </summary>
        public TelegramBotSetting FocusTelegram { get; set; }
        /// <summary>
        /// Telegram類型
        /// <para> 0 = Add</para>
        /// <para> 1 = Edit</para>
        /// </summary>
        public int TelegramIndex { get; set; } = 0;
        /// <summary>
        /// Telegram聚焦編號
        /// </summary>
        public int FocusTelegramIndex { get; set; } = 0;
        #endregion
        public SettingUserControl()
        {
            InitializeComponent();
            SystemSetting = InitialMethod.Load_System();
            TelePhoneSettings = InitialMethod.Load_Telephone();
            LineNotifySettings = InitialMethod.Load_LineNotify();
            TelegramBotSettings = InitialMethod.Load_Telegram();
            NotifyVisible = InitialMethod.Load_NotifyVisible();

            TelephoneTabPage.PageVisible = NotifyVisible.TelePhoneFlag;
            LineTabPage.PageVisible = NotifyVisible.LineNotifyFlag;
            TelegramTabPage.PageVisible = NotifyVisible.TelegramFlag;

            #region 設備功能旗標
            TelephonetoggleSwitch.IsOn = SystemSetting.TelephoneFlag;
            TelephonecomboBoxEdit.Text = SystemSetting.TelephoneCOM;
            LinetoggleSwitch.IsOn = SystemSetting.LineFlag;
            TelegramtoggleSwitch.IsOn = SystemSetting.TelegramFlag;
            #endregion
            #region Telephone 報表
            TelephonegridControl.DataSource = TelePhoneSettings;
            for (int i = 0; i < TelephonegridView.Columns.Count; i++)
            {
                TelephonegridView.Columns[i].OptionsColumn.AllowEdit = false;
            }
            TelephonegridView.Columns["Index"].Visible = false;
            TelephonegridView.Columns["Name"].Caption = "姓名";
            TelephonegridView.Columns["PhoneNumber"].Caption = "手機號碼";
            TelephonegridView.FocusedRowChanged += (s, e) =>
            {
                if (e.FocusedRowHandle >= 0)
                {
                    FocusTelephoneIndex = e.FocusedRowHandle;
                    Change_TelephoneText();
                }
                else
                {
                    FocusTelephoneIndex = 0;
                }
            };
            Change_TelephoneText();
            #endregion
            #region Line Notify 報表
            LinegridControl.DataSource = LineNotifySettings;
            for (int i = 0; i < LinegridView.Columns.Count; i++)
            {
                LinegridView.Columns[i].OptionsColumn.AllowEdit = false;
            }
            LinegridView.Columns["Index"].Visible = false;
            LinegridView.Columns["Name"].Caption = "名稱";
            LinegridView.Columns["LineToken"].Caption = "Token";
            LinegridView.FocusedRowChanged += (s, e) =>
            {
                if (e.FocusedRowHandle >= 0)
                {
                    FocusLineIndex = e.FocusedRowHandle;
                    Change_LineText();
                }
                else
                {
                    FocusLineIndex = 0;
                }
            };
            Change_LineText();
            #endregion
            #region Telegram Bot 報表
            TelegramgridControl.DataSource = TelegramBotSettings;
            for (int i = 0; i < TelegramgridView.Columns.Count; i++)
            {
                TelegramgridView.Columns[i].OptionsColumn.AllowEdit = false;
            }
            TelegramgridView.Columns["Index"].Visible = false;
            TelegramgridView.Columns["Name"].Caption = "名稱";
            TelegramgridView.Columns["URL"].Visible = false;
            TelegramgridView.Columns["URL"].Caption = "網址";
            TelegramgridView.Columns["Chart_ID"].Caption = "Chart_ID";
            TelegramgridView.FocusedRowChanged += (s, e) =>
            {
                if (e.FocusedRowHandle >= 0)
                {
                    FocusTelegramIndex = e.FocusedRowHandle;
                    Change_TelegramText();
                }
                else
                {
                    FocusTelegramIndex = 0;
                }
            };
            Change_TelegramText();
            #endregion
            #region 簡訊機COM搜尋
            TelephonecomboBoxEdit.ButtonClick += (s, e) =>
            {
                TelephonecomboBoxEdit.Properties.Items.Clear();
                string[] portsName = SerialPort.GetPortNames();
                foreach (var item in portsName)
                {
                    TelephonecomboBoxEdit.Properties.Items.Add(item);
                }
                TelephonecomboBoxEdit.ShowPopup();
            };
            #endregion
            #region 切換視窗更新資訊
            xtraTabControl1.SelectedPageChanged += (s, e) =>
            {
                XtraTabControl xtraTab = (XtraTabControl)s;
                int i = xtraTab.SelectedTabPageIndex;
                if (xtraTab.TabPages[i].Name == "TelephoneTabPage")
                {
                    SystemSetting = InitialMethod.Load_System();
                    TelePhoneSettings = InitialMethod.Load_Telephone();
                    TelephonegridControl.DataSource = TelePhoneSettings;
                    TelephonegridControl.RefreshDataSource();
                    TelephonetoggleSwitch.IsOn = SystemSetting.TelephoneFlag;
                    TelephonecomboBoxEdit.Text = SystemSetting.TelephoneCOM;
                }
                else if (xtraTab.TabPages[i].Name == "LineTabPage")
                {
                    SystemSetting = InitialMethod.Load_System();
                    LineNotifySettings = InitialMethod.Load_LineNotify();
                    LinegridControl.DataSource = LineNotifySettings;
                    LinegridControl.RefreshDataSource();
                    LinetoggleSwitch.IsOn = SystemSetting.LineFlag;
                }
                else if (xtraTab.TabPages[i].Name == "TelegramTabPage")
                {
                    SystemSetting = InitialMethod.Load_System();
                    TelegramBotSettings = InitialMethod.Load_Telegram();
                    TelegramgridControl.DataSource = TelegramBotSettings;
                    TelegramgridControl.RefreshDataSource();
                    TelegramtoggleSwitch.IsOn = SystemSetting.TelegramFlag;
                }
            };
            #endregion
        }
        public override void InitialValue()
        {
            SystemSetting = InitialMethod.Load_System();
            TelePhoneSettings = InitialMethod.Load_Telephone();
            LineNotifySettings = InitialMethod.Load_LineNotify();
            TelegramBotSettings = InitialMethod.Load_Telegram();
            NotifyVisible = InitialMethod.Load_NotifyVisible();
            TelephoneTabPage.PageVisible = NotifyVisible.TelePhoneFlag;
            LineTabPage.PageVisible = NotifyVisible.LineNotifyFlag;
            TelegramTabPage.PageVisible = NotifyVisible.TelegramFlag;

            TelephonetoggleSwitch.IsOn = SystemSetting.TelephoneFlag;
            TelephonecomboBoxEdit.Text = SystemSetting.TelephoneCOM;
            LinetoggleSwitch.IsOn = SystemSetting.LineFlag;
            TelegramtoggleSwitch.IsOn = SystemSetting.TelegramFlag;

            TelephonegridControl.DataSource = TelePhoneSettings;
            TelephonegridControl.RefreshDataSource();
            Change_TelephoneText();

            LinegridControl.DataSource = LineNotifySettings;
            LinegridControl.RefreshDataSource();
            Change_LineText();

            TelegramgridControl.DataSource = TelegramBotSettings;
            TelegramgridControl.RefreshDataSource();
            Change_TelegramText();
        }

        #region 手機新增按鈕
        private void TelephoneAddsimpleButton_Click(object sender, EventArgs e)
        {
            TelephoneIndex = 0;
            TelephoneNametextEdit.Text = "";
            TelePhoneNumtextEdit.Text = "";
            TelephoneNametextEdit.Enabled = true;
            TelePhoneNumtextEdit.Enabled = true;
            TelephoneSavesimpleButton.Enabled = true;
            TelephoneCancelsimpleButton.Enabled = true;
            TelephoneAddsimpleButton.Enabled = false;
            TelephoneEditsimpleButton.Enabled = false;
            TelephoneDeletesimpleButton.Enabled = false;
        }
        #endregion
        #region 手機刪除按鈕
        private void TelephoneDeletesimpleButton_Click(object sender, EventArgs e)
        {
            FlyoutAction action = new FlyoutAction();
            action.Caption = "刪除簡訊設備";
            action.Description = "確定是否刪除設備";
            action.Commands.Add(FlyoutCommand.OK);
            action.Commands.Add(FlyoutCommand.Cancel);
            if (FlyoutDialog.Show(FindForm().FindForm(), action) == DialogResult.OK)
            {
                TelephonegridView.DeleteSelectedRows();
                InitialMethod.Save_Telephone(TelePhoneSettings);
                Change_TelephoneText();
            }
        }
        #endregion
        #region 手機修改案紐
        private void TelephoneEeditsimpleButton_Click(object sender, EventArgs e)
        {
            TelephoneIndex = 1;
            TelephoneNametextEdit.Enabled = true;
            TelePhoneNumtextEdit.Enabled = true;
            TelephoneSavesimpleButton.Enabled = true;
            TelephoneCancelsimpleButton.Enabled = true;
            TelephoneAddsimpleButton.Enabled = false;
            TelephoneEditsimpleButton.Enabled = false;
            TelephoneDeletesimpleButton.Enabled = false;
        }
        #endregion
        #region 手機儲存按鈕
        private void TelephoneSavesimpleButton_Click(object sender, EventArgs e)
        {
            FlyoutAction action = new FlyoutAction();
            switch (TelephoneIndex)
            {
                case 0:
                    {
                        if (TelephoneNametextEdit.Text != "" & TelePhoneNumtextEdit.Text.Length == 10)
                        {
                            TelePhoneSettings.Add(new TelePhoneSetting()
                            {
                                Index = Guid.NewGuid().ToString(),
                                Name = TelephoneNametextEdit.Text,
                                PhoneNumber = TelePhoneNumtextEdit.Text
                            });
                            TelephoneNametextEdit.Enabled = false;
                            TelePhoneNumtextEdit.Enabled = false;
                            TelephoneSavesimpleButton.Enabled = false;
                            TelephoneCancelsimpleButton.Enabled = false;
                            TelephoneAddsimpleButton.Enabled = true;
                            TelephoneEditsimpleButton.Enabled = true;
                            TelephoneDeletesimpleButton.Enabled = true;
                            TelephonegridControl.DataSource = TelePhoneSettings;
                            TelephonegridControl.RefreshDataSource();
                            InitialMethod.Save_Telephone(TelePhoneSettings);
                            Change_TelephoneText();
                        }
                        else
                        {
                            action.Caption = "新增簡訊設備錯誤";
                            action.Description = "請確認條件是否輸入完整";
                            action.Commands.Add(FlyoutCommand.OK);
                            FlyoutDialog.Show(FindForm().FindForm(), action);
                        }
                    }
                    break;
                case 1:
                    {
                        if (TelephoneNametextEdit.Text != "" & TelePhoneNumtextEdit.Text.Length == 10)
                        {
                            var point = TelePhoneSettings.SingleOrDefault(g => g.Index == FocusTelephone.Index);
                            if (point != null)
                            {
                                point.Name = TelephoneNametextEdit.Text;
                                point.PhoneNumber = TelePhoneNumtextEdit.Text;
                            }
                            TelephoneNametextEdit.Enabled = false;
                            TelePhoneNumtextEdit.Enabled = false;
                            TelephoneSavesimpleButton.Enabled = false;
                            TelephoneCancelsimpleButton.Enabled = false;
                            TelephoneAddsimpleButton.Enabled = true;
                            TelephoneEditsimpleButton.Enabled = true;
                            TelephoneDeletesimpleButton.Enabled = true;
                            TelephonegridControl.DataSource = TelePhoneSettings;
                            TelephonegridControl.RefreshDataSource();
                            InitialMethod.Save_Telephone(TelePhoneSettings);
                        }
                        else
                        {
                            action.Caption = "修改簡訊設備錯誤";
                            action.Description = "請確認條件是否輸入完整";
                            action.Commands.Add(FlyoutCommand.OK);
                            FlyoutDialog.Show(FindForm().FindForm(), action);
                        }
                    }
                    break;
            }
        }
        #endregion
        #region 手機取修按鈕
        private void TelephoneCancelsimpleButton_Click(object sender, EventArgs e)
        {
            TelephoneNametextEdit.Enabled = false;
            TelePhoneNumtextEdit.Enabled = false;
            TelephoneSavesimpleButton.Enabled = false;
            TelephoneCancelsimpleButton.Enabled = false;
            TelephoneAddsimpleButton.Enabled = true;
            TelephoneEditsimpleButton.Enabled = true;
            TelephoneDeletesimpleButton.Enabled = true;
            Change_TelephoneText();
        }
        #endregion
        #region 手機系統儲存按鈕
        private void SystemTelephoneSavesimpleButton_Click(object sender, EventArgs e)
        {
            FlyoutAction action = new FlyoutAction();
            SystemSetting.TelephoneFlag = TelephonetoggleSwitch.IsOn;
            SystemSetting.TelephoneCOM = TelephonecomboBoxEdit.Text;
            SystemSetting.LineFlag = LinetoggleSwitch.IsOn;
            SystemSetting.TelegramFlag = TelegramtoggleSwitch.IsOn;
            InitialMethod.Save_System(SystemSetting);
            action.Caption = "手機系統儲存完畢";
            action.Commands.Add(FlyoutCommand.OK);
            FlyoutDialog.Show(FindForm().FindForm(), action);
        }
        #endregion
        #region 手機更新聚焦數值
        /// <summary>
        /// 手機更新聚焦數值
        /// </summary>
        private void Change_TelephoneText()
        {
            TelephoneNametextEdit.Text = $"{TelephonegridView.GetRowCellValue(FocusTelephoneIndex, "Name")}";
            TelePhoneNumtextEdit.Text = $"{TelephonegridView.GetRowCellValue(FocusTelephoneIndex, "PhoneNumber")}";
            FocusTelephone = new TelePhoneSetting()
            {
                Index = $"{TelephonegridView.GetRowCellValue(FocusTelephoneIndex, "Index")}",
                Name = $"{TelephonegridView.GetRowCellValue(FocusTelephoneIndex, "Name")}",
                PhoneNumber = $"{TelephonegridView.GetRowCellValue(FocusTelephoneIndex, "PhoneNumber")}"
            };
        }
        #endregion
        #region 手機測試發送
        public SerialPort Rs232 = new SerialPort();
        private void TestSendsimpleButton_Click(object sender, EventArgs e)
        {
            using (SMSClass SMS = new SMSClass(Rs232, SystemSetting.TelephoneCOM))
            {
                SMS.SMS_Send(TelePhoneNumtextEdit.Text, $"測試發送");
            }
        }
        #endregion
        #region 手機資訊匯出
        /// <summary>
        /// 手機資訊匯出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TelephoneExportsimpleButto_Click(object sender, EventArgs e)
        {
            if (TelephonegridControl.DataSource != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = ".Xlsx|*.xlsx";
                saveFileDialog.Title = "Export Data";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    TelephonegridView.ExportToXlsx($"{saveFileDialog.FileName}");
                }
            }
        }
        #endregion
        #region 手機資訊匯入
        /// <summary>
        /// 手機資訊匯入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TelephoneImportsimpleButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "xlsx files (*.*)|*.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream file = new FileStream($"{dialog.FileName}", FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook xworkbook = new XSSFWorkbook(file);//Ecexl檔案載入
                    int sheet = xworkbook.NumberOfSheets;//取得分頁數量
                    var data = xworkbook.GetSheetAt(0);
                    IRow row = data.GetRow(0);
                    ICell name = row.GetCell(0);
                    ICell Telephone = row.GetCell(1);
                    if ($"{name}" == "姓名" && $"{Telephone}" == "手機號碼")
                    {
                        for (int Rownum = 1; Rownum < data.LastRowNum + 1; Rownum++)
                        {
                            row = data.GetRow(Rownum);
                            if (row != null)
                            {
                                name = row.GetCell(0);
                                Telephone = row.GetCell(1);
                                var telephoneSetting = TelePhoneSettings.SingleOrDefault(g => g.Name == $"{name}" & g.PhoneNumber == $"{Telephone}");
                                if (telephoneSetting == null)
                                {
                                    TelePhoneSetting setting = new TelePhoneSetting()
                                    {
                                        Index = $"{Guid.NewGuid()}",
                                        Name = $"{name}",
                                        PhoneNumber = $"{Telephone}"
                                    };
                                    TelePhoneSettings.Add(setting);
                                }
                            }
                        }
                        TelephonegridControl.DataSource = TelePhoneSettings;
                        TelephonegridControl.RefreshDataSource();
                        InitialMethod.Save_Telephone(TelePhoneSettings);
                        Change_TelephoneText();
                    }
                }
            }
        }
        #endregion

        #region Line Notify新增按鈕
        private void LineAddsimpleButton_Click(object sender, EventArgs e)
        {
            LineIndex = 0;
            LineNametextEdit.Text = "";
            LineTokentextEdit.Text = "";
            LineNametextEdit.Enabled = true;
            LineTokentextEdit.Enabled = true;
            LineSavesimpleButton.Enabled = true;
            LineCancelsimpleButton.Enabled = true;
            LineAddsimpleButton.Enabled = false;
            LineEditsimpleButton.Enabled = false;
            LineDeletesimpleButton.Enabled = false;

        }
        #endregion
        #region Line Notify刪除按鈕
        private void LineDeletesimpleButton_Click(object sender, EventArgs e)
        {
            FlyoutAction action = new FlyoutAction();
            action.Caption = "刪除Line Notify";
            action.Description = "確定是否刪除";
            action.Commands.Add(FlyoutCommand.OK);
            action.Commands.Add(FlyoutCommand.Cancel);
            if (FlyoutDialog.Show(FindForm().FindForm(), action) == DialogResult.OK)
            {
                LinegridView.DeleteSelectedRows();
                InitialMethod.Save_LineNotify(LineNotifySettings);
                Change_LineText();
            }
        }
        #endregion
        #region Line Notify修改按鈕
        private void LineEditsimpleButton_Click(object sender, EventArgs e)
        {
            LineIndex = 1;
            LineNametextEdit.Enabled = true;
            LineTokentextEdit.Enabled = true;
            LineSavesimpleButton.Enabled = true;
            LineCancelsimpleButton.Enabled = true;
            LineAddsimpleButton.Enabled = false;
            LineEditsimpleButton.Enabled = false;
            LineDeletesimpleButton.Enabled = false;
        }
        #endregion
        #region Line Notify儲存按鈕
        private void LineSavesimpleButton_Click(object sender, EventArgs e)
        {
            FlyoutAction action = new FlyoutAction();
            switch (LineIndex)
            {
                case 0:
                    {
                        if (LineNametextEdit.Text != "" & LineTokentextEdit.Text != "")
                        {
                            LineNotifySettings.Add(new LineNotifySetting()
                            {
                                Index = Guid.NewGuid().ToString(),
                                Name = LineNametextEdit.Text,
                                LineToken = LineTokentextEdit.Text
                            });
                            LineNametextEdit.Enabled = false;
                            LineTokentextEdit.Enabled = false;
                            LineSavesimpleButton.Enabled = false;
                            LineCancelsimpleButton.Enabled = false;
                            LineAddsimpleButton.Enabled = true;
                            LineEditsimpleButton.Enabled = true;
                            LineDeletesimpleButton.Enabled = true;
                            LinegridControl.DataSource = LineNotifySettings;
                            LinegridControl.RefreshDataSource();
                            InitialMethod.Save_LineNotify(LineNotifySettings);
                            Change_LineText();
                        }
                        else
                        {
                            action.Caption = "新增Line Notify錯誤";
                            action.Description = "請確認條件是否輸入完整";
                            action.Commands.Add(FlyoutCommand.OK);
                            FlyoutDialog.Show(FindForm().FindForm(), action);
                        }
                    }
                    break;
                case 1:
                    {
                        if (LineNametextEdit.Text != "" & LineTokentextEdit.Text != "")
                        {
                            var point = LineNotifySettings.SingleOrDefault(g => g.Index == FocusLine.Index);
                            if (point != null)
                            {
                                point.Name = LineNametextEdit.Text;
                                point.LineToken = LineTokentextEdit.Text;
                            }
                            LineNametextEdit.Enabled = false;
                            LineTokentextEdit.Enabled = false;
                            LineSavesimpleButton.Enabled = false;
                            LineCancelsimpleButton.Enabled = false;
                            LineAddsimpleButton.Enabled = true;
                            LineEditsimpleButton.Enabled = true;
                            LineDeletesimpleButton.Enabled = true;
                            LinegridControl.DataSource = LineNotifySettings;
                            LinegridControl.RefreshDataSource();
                            InitialMethod.Save_LineNotify(LineNotifySettings);
                        }
                        else
                        {
                            action.Caption = "修改Line Notify錯誤";
                            action.Description = "請確認條件是否輸入完整";
                            action.Commands.Add(FlyoutCommand.OK);
                            FlyoutDialog.Show(FindForm().FindForm(), action);
                        }
                    }
                    break;
            }
        }
        #endregion
        #region Line Notify取消按鈕
        private void LineCancelsimpleButton_Click(object sender, EventArgs e)
        {
            LineNametextEdit.Enabled = false;
            LineTokentextEdit.Enabled = false;
            LineSavesimpleButton.Enabled = false;
            LineCancelsimpleButton.Enabled = false;
            LineAddsimpleButton.Enabled = true;
            LineEditsimpleButton.Enabled = true;
            LineDeletesimpleButton.Enabled = true;
            Change_LineText();
        }
        #endregion
        #region Line Notify系統儲存按鈕
        private void SystemLineSavesimpleButton_Click(object sender, EventArgs e)
        {
            FlyoutAction action = new FlyoutAction();
            SystemSetting.TelephoneFlag = TelephonetoggleSwitch.IsOn;
            SystemSetting.TelephoneCOM = TelephonecomboBoxEdit.Text;
            SystemSetting.LineFlag = LinetoggleSwitch.IsOn;
            SystemSetting.TelegramFlag = TelegramtoggleSwitch.IsOn;
            InitialMethod.Save_System(SystemSetting);
            action.Caption = "Line Notify系統儲存完畢";
            action.Commands.Add(FlyoutCommand.OK);
            FlyoutDialog.Show(FindForm().FindForm(), action);
        }
        #endregion
        #region Line Notify更新聚焦數值
        /// <summary>
        /// Line Notify更新聚焦數值
        /// </summary>
        private void Change_LineText()
        {
            LineNametextEdit.Text = $"{LinegridView.GetRowCellValue(FocusLineIndex, "Name")}";
            LineTokentextEdit.Text = $"{LinegridView.GetRowCellValue(FocusLineIndex, "LineToken")}";
            FocusLine = new LineNotifySetting()
            {
                Index = $"{LinegridView.GetRowCellValue(FocusLineIndex, "Index")}",
                Name = $"{LinegridView.GetRowCellValue(FocusLineIndex, "Name")}",
                LineToken = $"{LinegridView.GetRowCellValue(FocusLineIndex, "LineToken")}"
            };
        }
        #endregion
        #region Line測試發報
        /// <summary>
        /// Line測試發報
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LineTestSendsimpleButton_Click(object sender, EventArgs e)
        {
            using (LineNotifyClass lineNotify = new LineNotifyClass(LineTokentextEdit.Text))
            {
                lineNotify.LineNotifyFunction($"時間:{DateTime.Now:yyyy/MM/dd HH:mm:ss}，測試發報");
            }
        }
        #endregion
        #region Line資訊匯出
        /// <summary>
        /// Line資訊匯出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LineExportsimpleButton_Click(object sender, EventArgs e)
        {
            if (LinegridControl.DataSource != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = ".Xlsx|*.xlsx";
                saveFileDialog.Title = "Export Data";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LinegridView.ExportToXlsx($"{saveFileDialog.FileName}");
                }
            }
        }
        #endregion
        #region Line資訊匯入
        /// <summary>
        /// Line資訊匯入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LineImportsimpleButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "xlsx files (*.*)|*.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream file = new FileStream($"{dialog.FileName}", FileMode.Open, FileAccess.Read))
                {
                    XSSFWorkbook xworkbook = new XSSFWorkbook(file);//Ecexl檔案載入
                    int sheet = xworkbook.NumberOfSheets;//取得分頁數量
                    var data = xworkbook.GetSheetAt(0);
                    IRow row = data.GetRow(0);
                    ICell name = row.GetCell(0);
                    ICell Token = row.GetCell(1);
                    if ($"{name}" == "名稱" && $"{Token}" == "Token")
                    {
                        for (int Rownum = 1; Rownum < data.LastRowNum + 1; Rownum++)
                        {
                            row = data.GetRow(Rownum);
                            if (row != null)
                            {
                                name = row.GetCell(0);
                                Token = row.GetCell(1);
                                var lineNotifysetting = LineNotifySettings.SingleOrDefault(g => g.Name == $"{name}" & g.LineToken == $"{Token}");
                                if (lineNotifysetting == null)
                                {
                                    LineNotifySetting setting = new LineNotifySetting()
                                    {
                                        Index = $"{Guid.NewGuid()}",
                                        Name = $"{name}",
                                        LineToken = $"{Token}"
                                    };
                                    LineNotifySettings.Add(setting);
                                }
                            }
                        }
                        LinegridControl.DataSource = LineNotifySettings;
                        LinegridControl.RefreshDataSource();
                        InitialMethod.Save_LineNotify(LineNotifySettings);
                        Change_LineText();
                    }
                }
            }
        }
        #endregion

        #region Telegram Bot新增按鈕
        private void TelegramAddsimpleButton_Click(object sender, EventArgs e)
        {
            TelephoneIndex = 0;
            TelegramNametextEdit.Text = "";
            TelegramURLtextEdit.Text = "";
            TelegramNametextEdit.Enabled = true;
            TelegramURLtextEdit.Enabled = true;
            TelegramSavesimpleButton.Enabled = true;
            TelegramCancelsimpleButton.Enabled = true;
            SearchChart_IDsimpleButton.Visible = true;
            Chart_IDgridControl.Visible = true;
            TelegramAddsimpleButton.Enabled = false;
            TelegramEditsimpleButton.Enabled = false;
            TelegramDeletesimpleButton.Enabled = false;
        }
        #endregion
        #region Telegram Bot刪除按鈕
        private void TelegramDeletesimpleButton_Click(object sender, EventArgs e)
        {
            FlyoutAction action = new FlyoutAction();
            action.Caption = "刪除Telegram Bot";
            action.Description = "確定是否刪除";
            action.Commands.Add(FlyoutCommand.OK);
            action.Commands.Add(FlyoutCommand.Cancel);
            if (FlyoutDialog.Show(FindForm().FindForm(), action) == DialogResult.OK)
            {
                TelegramgridView.DeleteSelectedRows();
                InitialMethod.Save_Telegram(TelegramBotSettings);
                Change_TelegramText();
            }
        }
        #endregion
        #region Telegram Bot修改按鈕
        private void TelegramEditsimpleButton_Click(object sender, EventArgs e)
        {
            TelephoneIndex = 1;
            TelegramNametextEdit.Enabled = true;
            TelegramURLtextEdit.Enabled = true;
            TelegramSavesimpleButton.Enabled = true;
            TelegramCancelsimpleButton.Enabled = true;
            SearchChart_IDsimpleButton.Visible = true;
            Chart_IDgridControl.Visible = true;
            TelegramAddsimpleButton.Enabled = false;
            TelegramEditsimpleButton.Enabled = false;
            TelegramDeletesimpleButton.Enabled = false;
        }
        #endregion
        #region Telegram Bot儲存按鈕
        private void TelegramSavesimpleButton_Click(object sender, EventArgs e)
        {
            FlyoutAction action = new FlyoutAction();
            switch (TelephoneIndex)
            {
                case 0:
                    {
                        if (TelegramNametextEdit.Text != "" & TelegramURLtextEdit.Text != "" & Chart_IDgridView.FocusedRowHandle > -1)
                        {
                            TelegramBotSettings.Add(new TelegramBotSetting()
                            {
                                Index = Guid.NewGuid().ToString(),
                                Name = TelegramNametextEdit.Text,
                                URL = TelegramURLtextEdit.Text,
                                Chart_ID = $"{Chart_IDgridView.GetRowCellValue(Chart_IDgridView.FocusedRowHandle, "id")}"
                            });
                            TelegramNametextEdit.Enabled = false;
                            TelegramURLtextEdit.Enabled = false;
                            TelegramSavesimpleButton.Enabled = false;
                            TelegramCancelsimpleButton.Enabled = false;
                            SearchChart_IDsimpleButton.Visible = false;
                            Chart_IDgridControl.Visible = false;
                            TelegramAddsimpleButton.Enabled = true;
                            TelegramEditsimpleButton.Enabled = true;
                            TelegramDeletesimpleButton.Enabled = true;
                            TelegramgridControl.DataSource = TelegramBotSettings;
                            TelegramgridControl.RefreshDataSource();
                            InitialMethod.Save_Telegram(TelegramBotSettings);
                            Change_TelegramText();
                            Chart_IDgridView.Columns.Clear();
                        }
                        else
                        {
                            action.Caption = "新增Telegram Bot錯誤";
                            action.Description = "請確認條件是否輸入完整";
                            action.Commands.Add(FlyoutCommand.OK);
                            FlyoutDialog.Show(FindForm().FindForm(), action);
                        }
                    }
                    break;
                case 1:
                    {
                        if (TelegramNametextEdit.Text != "" & TelegramURLtextEdit.Text != "" & Chart_IDgridView.FocusedRowHandle > -1)
                        {
                            var point = TelegramBotSettings.SingleOrDefault(g => g.Index == FocusTelegram.Index);
                            if (point != null)
                            {
                                point.Name = TelegramNametextEdit.Text;
                                point.URL = TelegramURLtextEdit.Text;
                                point.Chart_ID = $"{Chart_IDgridView.GetRowCellValue(Chart_IDgridView.FocusedRowHandle, "id")}";
                            }
                            TelegramNametextEdit.Enabled = false;
                            TelegramURLtextEdit.Enabled = false;
                            TelegramSavesimpleButton.Enabled = false;
                            TelegramCancelsimpleButton.Enabled = false;
                            SearchChart_IDsimpleButton.Visible = false;
                            Chart_IDgridControl.Visible = false;
                            TelegramAddsimpleButton.Enabled = true;
                            TelegramEditsimpleButton.Enabled = true;
                            TelegramDeletesimpleButton.Enabled = true;
                            TelegramgridControl.DataSource = TelegramBotSettings;
                            TelegramgridControl.RefreshDataSource();
                            InitialMethod.Save_Telegram(TelegramBotSettings);
                            Chart_IDgridView.Columns.Clear();
                        }
                        else
                        {
                            action.Caption = "修改Telegram Bot錯誤";
                            action.Description = "請確認條件是否輸入完整";
                            action.Commands.Add(FlyoutCommand.OK);
                            FlyoutDialog.Show(FindForm().FindForm(), action);
                        }
                    }
                    break;
            }
        }
        #endregion
        #region Telegram Bot取消按鈕
        private void TelegramCancelsimpleButton_Click(object sender, EventArgs e)
        {
            TelegramNametextEdit.Enabled = false;
            TelegramURLtextEdit.Enabled = false;
            TelegramSavesimpleButton.Enabled = false;
            TelegramCancelsimpleButton.Enabled = false;
            SearchChart_IDsimpleButton.Visible = false;
            Chart_IDgridControl.Visible = false;
            TelegramAddsimpleButton.Enabled = true;
            TelegramEditsimpleButton.Enabled = true;
            TelegramDeletesimpleButton.Enabled = true;
            Change_TelegramText();
            if (Chart_IDgridView.Columns.Count > 0)
            {
                Chart_IDgridView.Columns.Clear();
            }
        }
        #endregion
        #region Telegram Bot系統儲存按鈕
        private void SystemTelegramSavesimpleButton_Click(object sender, EventArgs e)
        {
            SystemSetting.TelephoneFlag = TelephonetoggleSwitch.IsOn;
            SystemSetting.TelephoneCOM = TelephonecomboBoxEdit.Text;
            SystemSetting.LineFlag = LinetoggleSwitch.IsOn;
            SystemSetting.TelegramFlag = TelegramtoggleSwitch.IsOn;
            InitialMethod.Save_System(SystemSetting);
            FlyoutAction action = new FlyoutAction();
            action.Caption = "Telegram Bot資訊儲存完畢";
            action.Commands.Add(FlyoutCommand.OK);
            FlyoutDialog.Show(FindForm().FindForm(), action);
        }
        #endregion
        #region Telegram Bot 查詢Chart_ID按鈕
        private void SearchChart_IDsimpleButton_Click(object sender, EventArgs e)
        {
            FlyoutAction action = new FlyoutAction();
            if (TelegramURLtextEdit.Text != "")
            {
                handle = SplashScreenManager.ShowOverlayForm(this);
                using (TelegramBotClass telegram = new TelegramBotClass(TelegramURLtextEdit.Text, ""))
                {
                    var data = telegram.Serch_TelegramID();
                    Chart_IDgridControl.DataSource = data;
                    for (int i = 0; i < Chart_IDgridView.Columns.Count; i++)
                    {
                        Chart_IDgridView.Columns[i].OptionsColumn.AllowEdit = false;
                        Chart_IDgridView.Columns[i].BestFit();
                    }
                    Chart_IDgridView.Columns["id"].Caption = "Chart_ID";
                    Chart_IDgridView.Columns["first_name"].Caption = "姓";
                    Chart_IDgridView.Columns["last_name"].Caption = "名";
                    Chart_IDgridView.Columns["title"].Caption = "群組名稱";
                    Chart_IDgridView.Columns["type"].Caption = "類型";
                }
                CloseProgressPanel(handle);
            }
            else
            {
                action.Caption = "搜尋Chart_ID錯誤";
                action.Description = "請確認URL輸入完整";
                action.Commands.Add(FlyoutCommand.OK);
                FlyoutDialog.Show(FindForm().FindForm(), action);
            }
        }
        #endregion
        #region Telegram Bot更新聚焦數值
        /// <summary>
        /// Telegram Bot更新聚焦數值
        /// </summary>
        private void Change_TelegramText()
        {
            TelegramNametextEdit.Text = $"{TelegramgridView.GetRowCellValue(FocusTelegramIndex, "Name")}";
            TelegramURLtextEdit.Text = $"{TelegramgridView.GetRowCellValue(FocusTelegramIndex, "URL")}";
            FocusTelegram = new TelegramBotSetting()
            {
                Index = $"{TelegramgridView.GetRowCellValue(FocusTelegramIndex, "Index")}",
                Name = $"{TelegramgridView.GetRowCellValue(FocusTelegramIndex, "Name")}",
                URL = $"{TelegramgridView.GetRowCellValue(FocusTelegramIndex, "URL")}",
                Chart_ID = $"{TelegramgridView.GetRowCellValue(FocusTelegramIndex, "Chart_ID")}"
            };
        }

        #endregion
    }
}
