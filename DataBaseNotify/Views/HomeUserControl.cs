using DataBaseNotify.Components;
using DataBaseNotify.Methods;
using DataBaseNotify.Modules;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DataBaseNotify.Views
{
    public partial class HomeUserControl : Field4UserControl
    {
        #region 推播Buffer
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
        #endregion
        /// <summary>
        /// 執行緒啟動旗標
        /// </summary>
        public bool ComponentFlag { get; set; } = false;
        /// <summary>
        /// 資料庫執行緒
        /// </summary>
        public DataBaseComponent DataBaseComponents { get; set; }
        /// <summary>
        /// 簡訊推播
        /// </summary>
        private SMSComponent SMSComponent { get; set; }
        /// <summary>
        /// Line推播
        /// </summary>
        private LineComponent LineComponent { get; set; }
        /// <summary>
        /// Telegram推播
        /// </summary>
        private TelegramComponent TelegramComponent { get; set; }
        /// <summary>
        /// 主畫面繼承
        /// </summary>
        private Form1 Form1 { get; set; }
        public HomeUserControl(My_SQL_Method my_SQL_Method, MS_SQL_Method mS_SQL_Method, Form1 form1)
        {
            InitializeComponent();
            Form1 = form1;
            My_SQL_Method = my_SQL_Method;
            MS_SQL_Method = mS_SQL_Method;
            InitialValue();
            DataBaseComponents = new DataBaseComponent(this, PointSettings, My_SQL_Method, MS_SQL_Method);
            ComponentpictureEdit.Image = ComponentimageCollection.Images["Play"];
            timer1.Interval = 1000;
        }
        #region 通訊按鈕
        /// <summary>
        /// 通訊按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ComponentpictureEdit_Click(object sender, EventArgs e)
        {
            if (!ComponentFlag)
            {
                SystemSetting = InitialMethod.Load_System();
                PointSettings = InitialMethod.Load_Point();
                DataBaseComponents.MyWorkState = true;
                {
                    SMSComponent = new SMSComponent(TelePhoneMessage);
                    SMSComponent.MyWorkState = true;
                }
                if (SystemSetting.LineFlag)
                {
                    LineComponent = new LineComponent(LineNotifyMessage);
                    LineComponent.MyWorkState = true;
                }
                if (SystemSetting.TelegramFlag)
                {
                    TelegramComponent = new TelegramComponent(TelegramMessage);
                    TelegramComponent.MyWorkState = true;
                }
                TimelabelControl.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                StatuslabelControl.Text = "運行";
                ComponentpictureEdit.Image = ComponentimageCollection.Images["Stop"];
                ComponentFlag = true;
                timer1.Enabled = true;
                Form1.Component_Action(true);
                if (DataBaseComponents != null)
                {
                    MessageModule message = new MessageModule()
                    {
                        PointTypeEnum = 0,
                        DataBaseNum = 0,
                        DataSheetNum = 0,
                        FieldNum = 0,
                        DateTime = DateTime.Now,
                        FieldName = "通訊狀態",
                        Description = $"運行中"
                    };
                    DataBaseComponents.CsvMethod.Save_Csv("設備通訊", message);
                }
            }
            else
            {
                UserControl control = new UserControl() { Size = new Size(300, 80) };

                SimpleButton cancelButton = new SimpleButton() { Dock = DockStyle.Right, Text = "Cancel", Size = new Size(150, 30) };
                cancelButton.DialogResult = DialogResult.Cancel;
                cancelButton.Parent = control;
                SimpleButton okButton = new SimpleButton() { Dock = DockStyle.Left, Text = "Ok", Size = new Size(150, 30) };
                okButton.DialogResult = DialogResult.OK;
                okButton.Parent = control;

                LabelControl labelControl = new LabelControl() { Dock = DockStyle.Top };
                labelControl.Text = "Stop Component Password";
                labelControl.Appearance.Font = new Font("Tahoma", 18);

                TextEdit textEdit = new TextEdit() { Dock = DockStyle.Top };
                textEdit.Properties.UseSystemPasswordChar = true;
                textEdit.Parent = control;
                labelControl.Parent = control;


                if (FlyoutDialog.Show(FindForm(), control) == DialogResult.OK)
                {
                    if (textEdit.Text == "stop")
                    {
                        if (DataBaseComponents != null)
                        {
                            MessageModule message = new MessageModule()
                            {
                                PointTypeEnum = 0,
                                DataBaseNum = 0,
                                DataSheetNum = 0,
                                FieldNum = 0,
                                DateTime = DateTime.Now,
                                FieldName = "通訊狀態",
                                Description = $"停止"
                            };
                            DataBaseComponents.CsvMethod.Save_Csv("設備通訊", message);
                        }
                        Close_Thread();
                        Form1.Component_Action(false);
                    }
                }
            }
        }
        #endregion
        #region 關閉流程
        /// <summary>
        /// 關閉流程
        /// </summary>
        public void Close_Thread()
        {
            if (DataBaseComponents != null)
            {
                DataBaseComponents.MyWorkState = false;
            }
            if (SMSComponent != null)
            {
                SMSComponent.MyWorkState = false;
            }
            if (LineComponent != null)
            {
                LineComponent.MyWorkState = false;
            }
            if (TelegramComponent != null)
            {
                TelegramComponent.MyWorkState = false;
            }
            StatuslabelControl.Text = "停止";
            ComponentpictureEdit.Image = ComponentimageCollection.Images["Play"];
            ComponentFlag = false;
            timer1.Enabled = false;
        }
        #endregion
        #region 載入數值
        /// <summary>
        /// 載入數值
        /// </summary>
        public override void InitialValue()
        {
            PointSettings = InitialMethod.Load_Point();
            SystemSetting = InitialMethod.Load_System();
            AutoActiontoggleSwitch.IsOn = SystemSetting.AutoActionFlag;
        }
        #endregion
        #region 儲存按鈕
        /// <summary>
        /// 儲存按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavesimpleButton_Click(object sender, EventArgs e)
        {
            FlyoutAction action = new FlyoutAction();
            SystemSetting.AutoActionFlag = AutoActiontoggleSwitch.IsOn;
            InitialMethod.Save_System(SystemSetting);
            action.Caption = "自動通訊儲存完畢";
            action.Commands.Add(FlyoutCommand.OK);
            FlyoutDialog.Show(FindForm().FindForm(), action);
        }
        #endregion
        #region 時間物件
        /// <summary>
        /// 時間物件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimelabelControl.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        #endregion
    }
}