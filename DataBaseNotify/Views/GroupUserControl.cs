using DataBaseNotify.Configuration;
using DataBaseNotify.Methods;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace DataBaseNotify.Views
{
    public partial class GroupUserControl : Field4UserControl
    {
        #region 群組初始物件
        /// <summary>
        /// 群組類型
        /// <para> 0 = Add</para>
        /// <para> 1 = Edit</para>
        /// </summary>
        public int GroupIndex { get; set; }
        /// <summary>
        /// 聚焦群組資訊
        /// </summary>
        public GroupSetting FocusGroup { get; set; }
        /// <summary>
        /// 群組聚焦編號
        /// </summary>
        public int FocusGroupIndex { get; set; }
        #endregion
        /// <summary>
        /// 群組設定畫面
        /// </summary>
        public GroupUserControl()
        {
            InitializeComponent();
            GroupSettings = InitialMethod.Load_Group();
            TelePhoneSettings = InitialMethod.Load_Telephone();
            LineNotifySettings = InitialMethod.Load_LineNotify();
            TelegramBotSettings = InitialMethod.Load_Telegram();
            #region 簡訊機
            TelephonecheckedComboBoxEdit.Properties.IncrementalSearch = true;
            TelephonecheckedComboBoxEdit.Properties.DataSource = TelePhoneSettings;
            TelephonecheckedComboBoxEdit.Properties.DisplayMember = "Name";
            TelephonecheckedComboBoxEdit.Properties.ValueMember = "Index";
            TelephonecheckedComboBoxEdit.RefreshEditValue();
            #endregion
            #region Line Notify
            LinecheckedComboBoxEdit.Properties.IncrementalSearch = true;
            LinecheckedComboBoxEdit.Properties.DataSource = LineNotifySettings;
            LinecheckedComboBoxEdit.Properties.DisplayMember = "Name";
            LinecheckedComboBoxEdit.Properties.ValueMember = "Index";
            LinecheckedComboBoxEdit.RefreshEditValue();
            #endregion
            #region Telegram
            TelegramcheckedComboBoxEdit.Properties.IncrementalSearch = true;
            TelegramcheckedComboBoxEdit.Properties.DataSource = TelegramBotSettings;
            TelegramcheckedComboBoxEdit.Properties.DisplayMember = "Name";
            TelegramcheckedComboBoxEdit.Properties.ValueMember = "Index";
            TelegramcheckedComboBoxEdit.RefreshEditValue();
            #endregion
            #region 群組報表
            GroupgridControl.DataSource = GroupSettings;
            for (int i = 0; i < GroupgridView.Columns.Count; i++)
            {
                GroupgridView.Columns[i].Visible = false;
                GroupgridView.Columns[i].OptionsColumn.AllowEdit = false;
            }
            GroupgridView.Columns["Name"].Visible = true;
            GroupgridView.Columns["Name"].Caption = "群組名稱";
            GroupgridView.FocusedRowChanged += (s, e) =>
            {
                if (e.FocusedRowHandle >= 0)
                {
                    FocusGroupIndex = e.FocusedRowHandle;
                    Change_FocusGroupText();
                }
                else
                {
                    FocusGroupIndex = 0;
                }
            };
            Thread.Sleep(1000);
            Change_FocusGroupText();
            #endregion
        }
        #region 載入數值
        public override void InitialValue()
        {
            GroupSettings = InitialMethod.Load_Group();
            TelePhoneSettings = InitialMethod.Load_Telephone();
            LineNotifySettings = InitialMethod.Load_LineNotify();
            TelegramBotSettings = InitialMethod.Load_Telegram();

            TelephonecheckedComboBoxEdit.Properties.DataSource = TelePhoneSettings;
            TelephonecheckedComboBoxEdit.RefreshEditValue();

            LinecheckedComboBoxEdit.Properties.DataSource = LineNotifySettings;
            LinecheckedComboBoxEdit.RefreshEditValue();

            TelegramcheckedComboBoxEdit.Properties.DataSource = TelegramBotSettings;
            TelegramcheckedComboBoxEdit.RefreshEditValue();
            Change_FocusGroupText();
        }
        #endregion

        #region 群組新增按鈕
        private void GroupAddsimpleButton_Click(object sender, EventArgs e)
        {
            GroupIndex = 0;
            GroupNametextEdit.Text = "";
            for (int i = 0; i < TelephonecheckedComboBoxEdit.Properties.Items.Count; i++)
            {
                TelephonecheckedComboBoxEdit.Properties.Items[i].CheckState = CheckState.Unchecked;
            }
            for (int i = 0; i < LinecheckedComboBoxEdit.Properties.Items.Count; i++)
            {
                LinecheckedComboBoxEdit.Properties.Items[i].CheckState = CheckState.Unchecked;
            }
            for (int i = 0; i < TelegramcheckedComboBoxEdit.Properties.Items.Count; i++)
            {
                TelegramcheckedComboBoxEdit.Properties.Items[i].CheckState = CheckState.Unchecked;
            }
            GroupNametextEdit.Enabled = true;
            TelephonecheckedComboBoxEdit.Enabled = true;
            LinecheckedComboBoxEdit.Enabled = true;
            TelegramcheckedComboBoxEdit.Enabled = true;
            GroupSavesimpleButton.Enabled = true;
            GroupCancelsimpleButton.Enabled = true;
            GroupAddsimpleButton.Enabled = false;
            GroupEditsimpleButton.Enabled = false;
            GroupDeletesimpleButton.Enabled = false;
        }
        #endregion
        #region 群組刪除按鈕
        private void GroupDeletesimpleButton_Click(object sender, EventArgs e)
        {
            FlyoutAction action = new FlyoutAction();
            action.Caption = "刪除群組資訊";
            action.Description = "確定是否刪除群組";
            action.Commands.Add(FlyoutCommand.OK);
            action.Commands.Add(FlyoutCommand.Cancel);
            if (FlyoutDialog.Show(FindForm().FindForm(), action) == DialogResult.OK)
            {
                GroupgridView.DeleteSelectedRows();
                InitialMethod.Save_Group(GroupSettings);
                Change_FocusGroupText();
            }
        }
        #endregion
        #region 群組修改按鈕
        private void GroupEditsimpleButton_Click(object sender, EventArgs e)
        {
            GroupIndex = 1;
            GroupNametextEdit.Enabled = true;
            TelephonecheckedComboBoxEdit.Enabled = true;
            LinecheckedComboBoxEdit.Enabled = true;
            TelegramcheckedComboBoxEdit.Enabled = true;
            GroupSavesimpleButton.Enabled = true;
            GroupCancelsimpleButton.Enabled = true;
            GroupAddsimpleButton.Enabled = false;
            GroupEditsimpleButton.Enabled = false;
            GroupDeletesimpleButton.Enabled = false;
        }
        #endregion
        #region 群組儲存按鈕
        private void GroupSavesimpleButton_Click(object sender, EventArgs e)
        {
            FlyoutAction action = new FlyoutAction();
            switch (GroupIndex)
            {
                case 0:
                    {
                        if (GroupNametextEdit.Text != "")
                        {
                            #region 簡訊機
                            List<object> TelephoneChecked = TelephonecheckedComboBoxEdit.Properties.Items.GetCheckedValues();
                            string[] Telephones = new string[TelephoneChecked.Count];
                            for (int i = 0; i < TelephoneChecked.Count; i++)
                            {
                                Telephones[i] = $"{TelephoneChecked[i]}";
                            }
                            #endregion
                            #region Line Notify
                            List<object> LineeChecked = LinecheckedComboBoxEdit.Properties.Items.GetCheckedValues();
                            string[] Lines = new string[LineeChecked.Count];
                            for (int i = 0; i < LineeChecked.Count; i++)
                            {
                                Lines[i] = $"{LineeChecked[i]}";
                            }
                            #endregion
                            #region Telegram
                            List<object> TelegramChecked = TelegramcheckedComboBoxEdit.Properties.Items.GetCheckedValues();
                            string[] Telegrams = new string[TelegramChecked.Count];
                            for (int i = 0; i < TelegramChecked.Count; i++)
                            {
                                Telegrams[i] = $"{TelegramChecked[i]}";
                            }
                            #endregion
                            GroupSettings.Add(new GroupSetting()
                            {
                                Index = Guid.NewGuid().ToString(),
                                Name = GroupNametextEdit.Text,
                                TelePhone = Telephones,
                                Line = Lines,
                                Telegram = Telegrams
                            });
                            GroupNametextEdit.Enabled = false;
                            TelephonecheckedComboBoxEdit.Enabled = false;
                            LinecheckedComboBoxEdit.Enabled = false;
                            TelegramcheckedComboBoxEdit.Enabled = false;
                            GroupSavesimpleButton.Enabled = false;
                            GroupCancelsimpleButton.Enabled = false;
                            GroupAddsimpleButton.Enabled = true;
                            GroupEditsimpleButton.Enabled = true;
                            GroupDeletesimpleButton.Enabled = true;
                            GroupgridControl.DataSource = GroupSettings;
                            GroupgridControl.RefreshDataSource();
                            InitialMethod.Save_Group(GroupSettings);
                            Change_FocusGroupText();
                        }
                        else
                        {
                            action.Caption = "新增群組錯誤";
                            action.Description = "請確認條件是否輸入完整";
                            action.Commands.Add(FlyoutCommand.OK);
                            FlyoutDialog.Show(FindForm().FindForm(), action);
                        }
                    }
                    break;
                case 1:
                    {
                        if (GroupNametextEdit.Text != "")
                        {
                            #region 簡訊機
                            List<object> TelephoneChecked = TelephonecheckedComboBoxEdit.Properties.Items.GetCheckedValues();
                            string[] Telephones = new string[TelephoneChecked.Count];
                            for (int i = 0; i < TelephoneChecked.Count; i++)
                            {
                                Telephones[i] = $"{TelephoneChecked[i]}";
                            }
                            #endregion
                            #region Line Notify
                            List<object> LineeChecked = LinecheckedComboBoxEdit.Properties.Items.GetCheckedValues();
                            string[] Lines = new string[LineeChecked.Count];
                            for (int i = 0; i < LineeChecked.Count; i++)
                            {
                                Lines[i] = $"{LineeChecked[i]}";
                            }
                            #endregion
                            #region Telegram
                            List<object> TelegramChecked = TelegramcheckedComboBoxEdit.Properties.Items.GetCheckedValues();
                            string[] Telegrams = new string[TelegramChecked.Count];
                            for (int i = 0; i < TelegramChecked.Count; i++)
                            {
                                Telegrams[i] = $"{TelegramChecked[i]}";
                            }
                            #endregion
                            var point = GroupSettings.SingleOrDefault(s => s.Index == FocusGroup.Index);
                            if (point != null)
                            {
                                point.Name = GroupNametextEdit.Text;
                                point.TelePhone = Telephones;
                                point.Line = Lines;
                                point.Telegram = Telegrams;
                            }
                            GroupNametextEdit.Enabled = false;
                            TelephonecheckedComboBoxEdit.Enabled = false;
                            LinecheckedComboBoxEdit.Enabled = false;
                            TelegramcheckedComboBoxEdit.Enabled = false;
                            GroupSavesimpleButton.Enabled = false;
                            GroupCancelsimpleButton.Enabled = false;
                            GroupAddsimpleButton.Enabled = true;
                            GroupEditsimpleButton.Enabled = true;
                            GroupDeletesimpleButton.Enabled = true;
                            GroupgridControl.DataSource = GroupSettings;
                            GroupgridControl.RefreshDataSource();
                            InitialMethod.Save_Group(GroupSettings);
                        }
                        else
                        {
                            action.Caption = "修改群組錯誤";
                            action.Description = "請確認條件是否輸入完整";
                            action.Commands.Add(FlyoutCommand.OK);
                            FlyoutDialog.Show(FindForm().FindForm(), action);
                        }
                    }
                    break;
            }
        }
        #endregion
        #region 群組取消按鈕
        private void GroupCancelsimpleButton_Click(object sender, EventArgs e)
        {
            GroupNametextEdit.Enabled = false;
            TelephonecheckedComboBoxEdit.Enabled = false;
            LinecheckedComboBoxEdit.Enabled = false;
            TelegramcheckedComboBoxEdit.Enabled = false;
            GroupSavesimpleButton.Enabled = false;
            GroupCancelsimpleButton.Enabled = false;
            GroupAddsimpleButton.Enabled = true;
            GroupEditsimpleButton.Enabled = true;
            GroupDeletesimpleButton.Enabled = true;
            Change_FocusGroupText();
        }
        #endregion
        #region 群組更新聚焦數值
        /// <summary>
        /// 群組更新聚焦數值
        /// </summary>
        private void Change_FocusGroupText()
        {
            GroupNametextEdit.Text = $"{GroupgridView.GetRowCellValue(FocusGroupIndex, "Name")}";
            string[] telephone = GroupgridView.GetRowCellValue(FocusGroupIndex, "TelePhone") as string[];
            string[] line = GroupgridView.GetRowCellValue(FocusGroupIndex, "Line") as string[];
            string[] telegram = GroupgridView.GetRowCellValue(FocusGroupIndex, "Telegram") as string[];
            FocusGroup = new GroupSetting()
            {
                Index = $"{GroupgridView.GetRowCellValue(FocusGroupIndex, "Index")}",
                Name = $"{GroupgridView.GetRowCellValue(FocusGroupIndex, "Name")}",
                TelePhone = telephone,
                Line = line,
                Telegram = telegram
            };
            #region 簡訊機
            if (TelephonecheckedComboBoxEdit.Properties.Items.Count > 0)
            {
                for (int i = 0; i < TelephonecheckedComboBoxEdit.Properties.Items.Count; i++)
                {
                    TelephonecheckedComboBoxEdit.Properties.Items[i].CheckState = CheckState.Unchecked;
                }
                IList<TelePhoneSetting> telePhoneSettings = TelephonecheckedComboBoxEdit.Properties.DataSource as IList<TelePhoneSetting>;
                if (FocusGroup.TelePhone != null)
                {
                    foreach (var TelePhoneitem in FocusGroup.TelePhone)
                    {
                        var data = TelePhoneSettings.SingleOrDefault(s => s.Index == TelePhoneitem);
                        if (data != null)
                        {
                            int Index = telePhoneSettings.IndexOf(data);
                            TelephonecheckedComboBoxEdit.Properties.Items[Index].CheckState = CheckState.Checked;
                        }
                    }
                }
            }
            #endregion
            #region Line Notify
            if (LinecheckedComboBoxEdit.Properties.Items.Count > 0)
            {
                for (int i = 0; i < LinecheckedComboBoxEdit.Properties.Items.Count; i++)
                {
                    LinecheckedComboBoxEdit.Properties.Items[i].CheckState = CheckState.Unchecked;
                }
                IList<LineNotifySetting> lineNotifySettings = LinecheckedComboBoxEdit.Properties.DataSource as IList<LineNotifySetting>;
                if (FocusGroup.Line != null)
                {
                    foreach (var Lineitem in FocusGroup.Line)
                    {
                        var data = LineNotifySettings.SingleOrDefault(s => s.Index == Lineitem);
                        if (data != null)
                        {
                            int Index = lineNotifySettings.IndexOf(data);
                            LinecheckedComboBoxEdit.Properties.Items[Index].CheckState = CheckState.Checked;
                        }
                    }
                }
            }
            #endregion
            #region Telegram
            if (TelegramcheckedComboBoxEdit.Properties.Items.Count > 0)
            {
                for (int i = 0; i < TelegramcheckedComboBoxEdit.Properties.Items.Count; i++)
                {
                    TelegramcheckedComboBoxEdit.Properties.Items[i].CheckState = CheckState.Unchecked;
                }
                IList<TelegramBotSetting> telegramBotSettings = TelegramcheckedComboBoxEdit.Properties.DataSource as IList<TelegramBotSetting>;
                if (FocusGroup.Telegram != null)
                {
                    foreach (var TelePhoneitem in FocusGroup.Telegram)
                    {
                        var data = TelegramBotSettings.SingleOrDefault(s => s.Index == TelePhoneitem);
                        if (data != null)
                        {
                            int Index = telegramBotSettings.IndexOf(data);
                            TelegramcheckedComboBoxEdit.Properties.Items[Index].CheckState = CheckState.Checked;
                        }
                    }
                }
            }
            #endregion
        }
        #endregion
    }
}
