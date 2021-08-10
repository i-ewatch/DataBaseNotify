using DataBaseNotify.Configuration;
using DataBaseNotify.Methods;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
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
    public partial class PointUserControl : Field4UserControl
    {
        /// <summary>
        /// 資料庫名稱
        /// </summary>
        private string DataBaseName { get; set; }
        /// <summary>
        /// 資料表名稱
        /// </summary>
        private string TableName { get; set; }
        /// <summary>
        /// 欄位名稱
        /// </summary>
        private string FieldName { get; set; }
        /// <summary>
        /// 錯誤視窗
        /// </summary>
        private FlyoutAction action { get; set; } = new FlyoutAction();
        #region AI初始物件
        /// <summary>
        /// AI類型
        /// <para> 0 = Add</para>
        /// <para> 1 = Edit</para>
        /// </summary>
        public int AIIndex { get; set; }
        /// <summary>
        /// 聚焦AI資訊
        /// </summary>
        public AI FocusAI { get; set; }
        /// <summary>
        /// 聚焦AI編號
        /// </summary>
        public int FocusAIIndex { get; set; }
        /// <summary>
        /// AI點位資訊
        /// </summary>
        public List<AI> AIs { get; set; } = new List<AI>();
        #endregion
        #region DI初始物件
        /// <summary>
        /// DI類型
        /// <para> 0 = Add</para>
        /// <para> 1 = Edit</para>
        /// </summary>
        public int DIIndex { get; set; }
        /// <summary>
        /// 聚焦DI資訊
        /// </summary>
        public DI FocusDI { get; set; }
        /// <summary>
        /// 聚焦DI編號
        /// </summary>
        public int FocusDIIndex { get; set; }
        /// <summary>
        /// DI點位資訊
        /// </summary>
        public List<DI> DIs { get; set; } = new List<DI>();
        #endregion
        #region Enums初始物件
        /// <summary>
        /// Enumses資訊
        /// </summary>
        private List<Enumses> Enumses { get; set; } = new List<Enumses>();
        /// <summary>
        /// Enums類型
        /// <para> 0 = Add</para>
        /// <para> 1 = Edit</para>
        /// </summary>
        public int EnumsIndex { get; set; }
        /// <summary>
        /// 聚焦Enums資訊
        /// </summary>
        public Enumses FocusEnums { get; set; }
        /// <summary>
        /// 聚焦Enums編號
        /// </summary>
        public int FocusEnumsIndex { get; set; }
        #endregion
        public PointUserControl()
        {
            InitializeComponent();
            InitialValue();
            #region 資料庫名稱下拉選單
            DataBasecomboBoxEdit.ButtonClick += (s, e) =>
              {
                  if (PointSettings != null)
                  {
                      if (DataBasecomboBoxEdit.Properties.Items.Count > 0)
                      {
                          DataBasecomboBoxEdit.Properties.Items.Clear();
                      }
                      foreach (var item in PointSettings)
                      {
                          DataBasecomboBoxEdit.Properties.Items.Add(item.DataBaseName);
                      }
                      DataBasecomboBoxEdit.ShowPopup();
                  }
                  else
                  {
                      DataBaseName = string.Empty;
                  }
                  TableNamecomboBoxEdit.Text = "";
                  TableName = string.Empty;
              };
            #endregion
            #region 資料表名稱下拉選單
            TableNamecomboBoxEdit.ButtonClick += (s, e) =>
              {
                  if (DataBasecomboBoxEdit.Text != "")
                  {
                      DataBaseName = DataBasecomboBoxEdit.Text;
                      if (TableNamecomboBoxEdit.Properties.Items.Count > 0)
                      {
                          TableNamecomboBoxEdit.Properties.Items.Clear();
                      }
                      var PointSetting = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);
                      if (PointSetting != null)
                      {
                          foreach (var item in PointSetting.DataSheets)
                          {
                              TableNamecomboBoxEdit.Properties.Items.Add(item.DataSheetName);
                          }
                      }
                      TableNamecomboBoxEdit.ShowPopup();
                  }
                  else
                  {
                      if (TableNamecomboBoxEdit.Properties.Items.Count > 0)
                      {
                          TableNamecomboBoxEdit.Properties.Items.Clear();
                      }
                      DataBaseName = string.Empty;
                  }
              };
            #endregion
            #region Scan按鈕
            RefreshsimpleButton.Click += (s, e) =>
              {
                  if (TableNamecomboBoxEdit.Text != "")
                  {
                      TableName = TableNamecomboBoxEdit.Text;
                  }
                  else
                  {
                      TableName = string.Empty;
                  }
                  if (DataBaseName != "" && TableName != "")
                  {
                      var PointSetting = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);
                      if (PointSetting != null)
                      {
                          var Table = PointSetting.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);
                          if (Table != null)
                          {
                              if (AIs.Count >= 0)
                              {
                                  AIs.Clear();
                              }
                              if (Table.AIs.Count > 0)
                              {
                                  AIs.AddRange(Table.AIs);
                                  AIEditsimpleButton.Enabled = true;
                                  AIgridControl.DataSource = AIs;
                              }
                              AIgridControl.RefreshDataSource();
                              if (DIs.Count >= 0)
                              {
                                  DIs.Clear();
                              }
                              if (Table.DIs.Count > 0)
                              {
                                  DIs.AddRange(Table.DIs);
                                  DIEditsimpleButton.Enabled = true;
                                  DIgridControl.DataSource = DIs;
                              }
                              DIgridControl.RefreshDataSource();
                              if (Enumses.Count >= 0)
                              {
                                  Enumses.Clear();
                              }
                              if (Table.Enumses.Count > 0)
                              {
                                  Enumses.AddRange(Table.Enumses);
                                  EnumsEditsimpleButton.Enabled = true;
                                  EnumsgridControl.DataSource = Enumses;
                              }
                              EnumsgridControl.RefreshDataSource();
                          }
                      }
                  }
              };
            #endregion

            #region AI 群組
            AIGroupcheckedComboBoxEdit.Properties.IncrementalSearch = true;
            AIGroupcheckedComboBoxEdit.Properties.DataSource = GroupSettings;
            AIGroupcheckedComboBoxEdit.Properties.DisplayMember = "Name";
            AIGroupcheckedComboBoxEdit.Properties.ValueMember = "Index";
            AIGroupcheckedComboBoxEdit.RefreshEditValue();
            #endregion
            #region AI 報表
            RepositoryItemToggleSwitch AIswitch = new RepositoryItemToggleSwitch();
            AIgridControl.RepositoryItems.Add(AIswitch);
            AIgridControl.DataSource = AIs;
            for (int i = 0; i < AIgridView.Columns.Count; i++)
            {
                AIgridView.Columns[i].Visible = false;
                AIgridView.Columns[i].OptionsColumn.AllowEdit = false;
            }
            AIgridView.Columns["MinLimit"].Visible = true;
            AIgridView.Columns["MinLimit"].Caption = "下限值";
            AIgridView.Columns["MaxLimit"].Visible = true;
            AIgridView.Columns["MaxLimit"].Caption = "上限值";
            AIgridView.Columns["FieldName"].Visible = true;
            AIgridView.Columns["FieldName"].Caption = "欄位名稱";
            AIgridView.Columns["AlarmFlag"].Visible = true;
            AIgridView.Columns["AlarmFlag"].Caption = "告警發布";
            AIgridView.Columns["AlarmFlag"].ColumnEdit = AIswitch;
            AIgridView.FocusedRowChanged += (s, e) =>
            {
                FocusAIIndex = e.FocusedRowHandle;
                Change_AIText();
            };
            #endregion
            #region AI修改按鈕
            AIEditsimpleButton.Click += (s, e) =>
              {
                  AItoggleSwitch.ReadOnly = false;
                  AINametextEdit.Enabled = true;
                  AIMaxtoggleSwitch.ReadOnly = false;
                  AIResettoggleSwitch.ReadOnly = false;
                  AIMintoggleSwitch.ReadOnly = false;
                  MaxtextEdit.Enabled = true;
                  MintextEdit.Enabled = true;
                  AIGroupcheckedComboBoxEdit.Enabled = true;
                  AISavesimpleButton.Enabled = true;
                  AICancelsimpleButton.Enabled = true;
                  AIEditsimpleButton.Enabled = false;
              };
            #endregion
            #region AI儲存按鈕
            AISavesimpleButton.Click += (s, e) =>
              {
                  FlyoutAction action = new FlyoutAction();
                  if (MaxtextEdit.Text != "" & MintextEdit.Text != "" & Convert.ToSingle(MaxtextEdit.Text) >= Convert.ToSingle(MintextEdit.Text))
                  {
                      var IPdata = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);
                      if (IPdata != null)
                      {
                          var Table = IPdata.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);
                          if (Table != null)
                          {
                              List<object> aigroup = AIGroupcheckedComboBoxEdit.Properties.Items.GetCheckedValues();
                              string[] group = new string[aigroup.Count];
                              for (int i = 0; i < aigroup.Count; i++)
                              {
                                  group[i] = $"{aigroup[i]}";
                              }
                              var data = AIs.SingleOrDefault(g => g.FieldName == FocusAI.FieldName);
                              if (data != null)
                              {
                                  data.MaxLimit = Convert.ToSingle(MaxtextEdit.Text);
                                  data.MinLimit = Convert.ToSingle(MintextEdit.Text);
                                  data.AlarmFlag = AItoggleSwitch.IsOn;
                                  data.ShowName = AINametextEdit.Text;
                                  data.MaxFlag = AIMaxtoggleSwitch.IsOn;
                                  data.ResetFlag = AIResettoggleSwitch.IsOn;
                                  data.MinFlag = AIMintoggleSwitch.IsOn;
                                  data.Group = group;
                              }
                              Table.AIs.Clear();
                              Table.AIs.AddRange(AIs);
                              //Table.AIs = AIs;
                              AItoggleSwitch.ReadOnly = true;
                              AINametextEdit.Enabled = false;
                              AIMaxtoggleSwitch.ReadOnly = true;
                              AIResettoggleSwitch.ReadOnly = true;
                              AIMintoggleSwitch.ReadOnly = true;
                              MaxtextEdit.Enabled = false;
                              MintextEdit.Enabled = false;
                              AIGroupcheckedComboBoxEdit.Enabled = false;
                              AISavesimpleButton.Enabled = false;
                              AICancelsimpleButton.Enabled = false;
                              AIEditsimpleButton.Enabled = true;
                              AIgridControl.DataSource = AIs;
                              AIgridControl.RefreshDataSource();
                              InitialMethod.Save_Point(PointSettings);
                          }
                      }
                  }
                  else
                  {
                      action.Caption = "修改點位錯誤";
                      action.Description = "請確認上下限值是否輸入完整";
                      action.Commands.Add(FlyoutCommand.OK);
                      FlyoutDialog.Show(FindForm().FindForm(), action);
                  }
              };
            #endregion
            #region AI取消按鈕
            AICancelsimpleButton.Click += (s, e) =>
              {
                  AItoggleSwitch.ReadOnly = true;
                  AINametextEdit.Enabled = false;
                  AIMaxtoggleSwitch.ReadOnly = true;
                  AIResettoggleSwitch.ReadOnly = true;
                  AIMintoggleSwitch.ReadOnly = true;
                  MaxtextEdit.Enabled = false;
                  MintextEdit.Enabled = false;
                  AIGroupcheckedComboBoxEdit.Enabled = false;
                  AISavesimpleButton.Enabled = false;
                  AICancelsimpleButton.Enabled = false;
                  AIEditsimpleButton.Enabled = true;
                  Change_AIText();
              };
            #endregion

            #region DI 群組
            DIGroupcheckedComboBoxEdit.Properties.IncrementalSearch = true;
            DIGroupcheckedComboBoxEdit.Properties.DataSource = GroupSettings;
            DIGroupcheckedComboBoxEdit.Properties.DisplayMember = "Name";
            DIGroupcheckedComboBoxEdit.Properties.ValueMember = "Index";
            DIGroupcheckedComboBoxEdit.RefreshEditValue();
            #endregion
            #region DI 報表
            RepositoryItemToggleSwitch DIswitch = new RepositoryItemToggleSwitch();
            DIgridControl.RepositoryItems.Add(DIswitch);
            DIgridControl.DataSource = DIs;
            for (int i = 0; i < DIgridView.Columns.Count; i++)
            {
                DIgridView.Columns[i].Visible = false;
                DIgridView.Columns[i].OptionsColumn.AllowEdit = false;
            }
            DIgridView.Columns["GeneralFlag"].Visible = true;
            DIgridView.Columns["GeneralFlag"].Caption = "主Key";
            DIgridView.Columns["GeneralFlag"].ColumnEdit = DIswitch;
            DIgridView.Columns["FieldName"].Visible = true;
            DIgridView.Columns["FieldName"].Caption = "欄位名稱";
            DIgridView.Columns["AlarmFlag"].Visible = true;
            DIgridView.Columns["AlarmFlag"].Caption = "告警發布";
            DIgridView.Columns["AlarmFlag"].ColumnEdit = DIswitch;
            DIgridView.FocusedRowChanged += (s, e) =>
            {
                FocusDIIndex = e.FocusedRowHandle;
                Change_DIText();
            };
            #endregion
            #region DI修改按鈕
            DIEditsimpleButton.Click += (s, e) =>
              {
                  DItoggleSwitch.ReadOnly = false;
                  DINametextEdit.Enabled = true;
                  DIResettoggleSwitch.ReadOnly = false;
                  DIGeneralFlagcomboBoxEdit.Enabled = true;
                  DIGroupcheckedComboBoxEdit.Enabled = true;
                  DISavesimpleButton.Enabled = true;
                  DICancelsimpleButton.Enabled = true;
                  DIEditsimpleButton.Enabled = false;
                  DIMessgaetextEdit.Enabled = true;
              };
            #endregion
            #region DI儲存按鈕
            DISavesimpleButton.Click += (s, e) =>
              {
                  var IPdata = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);
                  if (IPdata != null)
                  {
                      var Table = IPdata.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);
                      if (Table != null)
                      {
                          List<object> digroup = DIGroupcheckedComboBoxEdit.Properties.Items.GetCheckedValues();
                          string[] group = new string[digroup.Count];
                          for (int i = 0; i < digroup.Count; i++)
                          {
                              group[i] = $"{digroup[i]}";
                          }
                          var data = DIs.FirstOrDefault(g => g.FieldName == FocusDI.FieldName);
                          if (data != null)
                          {
                              data.GeneralFlag = Convert.ToBoolean(DIGeneralFlagcomboBoxEdit.SelectedIndex);
                              data.AlarmFlag = DItoggleSwitch.IsOn;
                              data.ShowName = DINametextEdit.Text;
                              data.ResetFlag = DIResettoggleSwitch.IsOn;
                              data.Message = DIMessgaetextEdit.Text;
                              data.Group = group;
                          }
                          Table.DIs.Clear();
                          Table.DIs.AddRange(DIs);
                          //Table.DIs = DIs;
                          DItoggleSwitch.ReadOnly = true;
                          DINametextEdit.Enabled = false;
                          DIResettoggleSwitch.ReadOnly = true;
                          DIGeneralFlagcomboBoxEdit.Enabled = false;
                          DIGroupcheckedComboBoxEdit.Enabled = false;
                          DISavesimpleButton.Enabled = false;
                          DICancelsimpleButton.Enabled = false;
                          DIEditsimpleButton.Enabled = true;
                          DIMessgaetextEdit.Enabled = false;
                          DIgridControl.DataSource = DIs;
                          DIgridControl.RefreshDataSource();
                          InitialMethod.Save_Point(PointSettings);
                      }
                  }
              };
            #endregion
            #region DI取消按鈕
            DICancelsimpleButton.Click += (s, e) =>
              {
                  DItoggleSwitch.ReadOnly = true;
                  DINametextEdit.Enabled = false;
                  DIResettoggleSwitch.ReadOnly = true;
                  DIGeneralFlagcomboBoxEdit.Enabled = false;
                  DIGroupcheckedComboBoxEdit.Enabled = false;
                  DISavesimpleButton.Enabled = false;
                  DICancelsimpleButton.Enabled = false;
                  DIEditsimpleButton.Enabled = true;
                  DIMessgaetextEdit.Enabled = false;
                  Change_DIText();
              };
            #endregion

            #region Enums 群組
            EnumsGroupcheckedComboBoxEdit.Properties.IncrementalSearch = true;
            EnumsGroupcheckedComboBoxEdit.Properties.DataSource = GroupSettings;
            EnumsGroupcheckedComboBoxEdit.Properties.DisplayMember = "Name";
            EnumsGroupcheckedComboBoxEdit.Properties.ValueMember = "Index";
            EnumsGroupcheckedComboBoxEdit.RefreshEditValue();
            #endregion
            #region Enums 報表
            RepositoryItemToggleSwitch Enumsswitch = new RepositoryItemToggleSwitch();
            EnumsgridControl.RepositoryItems.Add(Enumsswitch);
            EnumsgridControl.DataSource = Enumses;
            for (int i = 0; i < EnumsgridView.Columns.Count; i++)
            {
                EnumsgridView.Columns[i].Visible = false;
                EnumsgridView.Columns[i].OptionsColumn.AllowEdit = false;
            }
            EnumsgridView.Columns["FieldName"].Visible = true;
            EnumsgridView.Columns["FieldName"].Caption = "欄位名稱";
            EnumsgridView.Columns["AlarmFlag"].Visible = true;
            EnumsgridView.Columns["AlarmFlag"].Caption = "告警發布";
            EnumsgridView.Columns["AlarmFlag"].ColumnEdit = DIswitch;
            EnumsgridView.FocusedRowChanged += (s, e) =>
            {
                FocusEnumsIndex = e.FocusedRowHandle;
                Change_EnumsText();
            };
            #endregion
            #region Enums修改按鈕
            EnumsEditsimpleButton.Click += (s, e) =>
              {
                  EnumstoggleSwitch.ReadOnly = false;
                  EnumsNametextEdit.Enabled = true;
                  EnumsGroupcheckedComboBoxEdit.Enabled = true;
                  EnumsSavesimpleButton.Enabled = true;
                  EnumsCancelsimpleButton.Enabled = true;
                  EnumsEditsimpleButton.Enabled = false;
                  EnumsDescribetextEdit.Enabled = true;
              };
            #endregion
            #region Enums儲存按鈕
            EnumsSavesimpleButton.Click += (s, e) =>
              {
                  var IPdata = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);
                  if (IPdata != null)
                  {
                      var Table = IPdata.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);
                      if (Table != null)
                      {
                          List<object> digroup = EnumsGroupcheckedComboBoxEdit.Properties.Items.GetCheckedValues();
                          string[] group = new string[digroup.Count];
                          for (int i = 0; i < digroup.Count; i++)
                          {
                              group[i] = $"{digroup[i]}";
                          }
                          var data = Enumses.FirstOrDefault(g => g.FieldName == FocusEnums.FieldName);
                          if (data != null)
                          {
                              data.AlarmFlag = EnumstoggleSwitch.IsOn;
                              data.ShowName = EnumsNametextEdit.Text;
                              data.EnumsDescribe = EnumsDescribetextEdit.Text;
                              data.Group = group;
                          }
                          Table.Enumses.Clear();
                          Table.Enumses.AddRange(Enumses);
                          //Table.Enumses = Enumses;
                          EnumstoggleSwitch.ReadOnly = true;
                          EnumsNametextEdit.Enabled = false;
                          EnumsGroupcheckedComboBoxEdit.Enabled = false;
                          EnumsSavesimpleButton.Enabled = false;
                          EnumsCancelsimpleButton.Enabled = false;
                          EnumsEditsimpleButton.Enabled = true;
                          EnumsDescribetextEdit.Enabled = false;
                          EnumsgridControl.DataSource = Enumses;
                          EnumsgridControl.RefreshDataSource();
                          InitialMethod.Save_Point(PointSettings);
                      }
                  }
              };
            #endregion
            #region Enums取消按鈕
            EnumsCancelsimpleButton.Click += (s, e) =>
              {
                  EnumstoggleSwitch.ReadOnly = true;
                  EnumsNametextEdit.Enabled = false;
                  EnumsGroupcheckedComboBoxEdit.Enabled = false;
                  EnumsSavesimpleButton.Enabled = false;
                  EnumsCancelsimpleButton.Enabled = false;
                  EnumsEditsimpleButton.Enabled = true;
                  EnumsDescribetextEdit.Enabled = false;
              };
            #endregion
        }
        public override void InitialValue()
        {
            GroupSettings = InitialMethod.Load_Group();
            PointSettings = InitialMethod.Load_Point();
            AIGroupcheckedComboBoxEdit.Properties.DataSource = GroupSettings;
            AIGroupcheckedComboBoxEdit.RefreshEditValue();
            DIGroupcheckedComboBoxEdit.Properties.DataSource = GroupSettings;
            DIGroupcheckedComboBoxEdit.RefreshEditValue();
            EnumsGroupcheckedComboBoxEdit.Properties.DataSource = GroupSettings;
            EnumsGroupcheckedComboBoxEdit.RefreshEditValue();

            DataBasecomboBoxEdit.Text = "";
            TableNamecomboBoxEdit.Text = "";

            AIs.Clear();
            AIgridControl.RefreshDataSource();
            AIEditsimpleButton.Enabled = false;
            AISavesimpleButton.Enabled = false;
            AICancelsimpleButton.Enabled = false;
            AItoggleSwitch.IsOn = false;
            AIDeviceNamelabelControl.Text = "";
            AINametextEdit.Text = "";
            AIResettoggleSwitch.IsOn = false;
            AIMaxtoggleSwitch.IsOn = false;
            MaxtextEdit.Text = "";
            AIMintoggleSwitch.IsOn = false;
            MintextEdit.Text = "";
            AIGroupcheckedComboBoxEdit.Text = "";

            DIs.Clear();
            DIgridControl.RefreshDataSource();
            DIEditsimpleButton.Enabled = false;
            DISavesimpleButton.Enabled = false;
            DICancelsimpleButton.Enabled = false;
            DItoggleSwitch.IsOn = false;
            DIDeviceNamelabelControl.Text = "";
            DINametextEdit.Text = "";
            DIResettoggleSwitch.IsOn = false;
            DIGeneralFlagcomboBoxEdit.SelectedIndex = -1;
            DIGroupcheckedComboBoxEdit.Text = "";
            AIGroupcheckedComboBoxEdit.Text = "";
            DIMessgaetextEdit.Text = "";

            Enumses.Clear();
            EnumsgridControl.RefreshDataSource();
            EnumsEditsimpleButton.Enabled = false;
            EnumsSavesimpleButton.Enabled = false;
            EnumsCancelsimpleButton.Enabled = false;
            EnumstoggleSwitch.IsOn = false;
            EnumsDeviceNamelabelControl.Text = "";
            EnumsNametextEdit.Text = "";
            EnumsDescribetextEdit.Text = "";
        }
        #region AI 聚焦更新數值
        private void Change_AIText()
        {
            AItoggleSwitch.IsOn = Convert.ToBoolean(AIgridView.GetRowCellValue(FocusAIIndex, "AlarmFlag"));
            AIDeviceNamelabelControl.Text = $"點位名稱 : {AIgridView.GetRowCellValue(FocusAIIndex, "FieldName")}";
            AINametextEdit.Text = $"{AIgridView.GetRowCellValue(FocusAIIndex, "ShowName")}";
            MaxtextEdit.Text = $"{AIgridView.GetRowCellValue(FocusAIIndex, "MaxLimit")}";
            MintextEdit.Text = $"{AIgridView.GetRowCellValue(FocusAIIndex, "MinLimit")}";
            AIMaxtoggleSwitch.IsOn = Convert.ToBoolean(AIgridView.GetRowCellValue(FocusAIIndex, "MaxFlag"));
            AIResettoggleSwitch.IsOn = Convert.ToBoolean(AIgridView.GetRowCellValue(FocusAIIndex, "ResetFlag"));
            AIMintoggleSwitch.IsOn = Convert.ToBoolean(AIgridView.GetRowCellValue(FocusAIIndex, "MinFlag"));
            string[] aigroup = AIgridView.GetRowCellValue(FocusAIIndex, "Group") as string[];
            FocusAI = new AI()
            {
                AlarmFlag = Convert.ToBoolean(AIgridView.GetRowCellValue(FocusAIIndex, "AlarmFlag")),
                FieldName = $"{AIgridView.GetRowCellValue(FocusAIIndex, "FieldName")}",
                ShowName = $"{AIgridView.GetRowCellValue(FocusAIIndex, "ShowName")}",
                FieldNum = Convert.ToInt32(AIgridView.GetRowCellValue(FocusAIIndex, "FieldNum")),
                MaxLimit = Convert.ToSingle(AIgridView.GetRowCellValue(FocusAIIndex, "MaxLimit")),
                MinLimit = Convert.ToSingle(AIgridView.GetRowCellValue(FocusAIIndex, "MinLimit")),
                MaxFlag = Convert.ToBoolean(AIgridView.GetRowCellValue(FocusAIIndex, "MaxFlag")),
                ResetFlag = Convert.ToBoolean(AIgridView.GetRowCellValue(FocusAIIndex, "ResetFlag")),
                MinFlag = Convert.ToBoolean(AIgridView.GetRowCellValue(FocusAIIndex, "MinFlag")),
                Group = aigroup
            };
            FieldName = $"{AIgridView.GetRowCellValue(FocusAIIndex, "FieldName")}";
            if (AIGroupcheckedComboBoxEdit.Properties.Items.Count > 0)
            {
                for (int i = 0; i < AIGroupcheckedComboBoxEdit.Properties.Items.Count; i++)
                {
                    AIGroupcheckedComboBoxEdit.Properties.Items[i].CheckState = CheckState.Unchecked;
                }
                IList<GroupSetting> aIs = AIGroupcheckedComboBoxEdit.Properties.DataSource as IList<GroupSetting>;
                if (FocusAI != null & PointSettings != null)
                {
                    var IPdata = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);
                    if (IPdata != null)
                    {
                        var Table = IPdata.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);
                        if (Table != null)
                        {
                            var AIitem = Table.AIs.SingleOrDefault(g => g.FieldName == FieldName);
                            if (AIitem.Group != null)
                            {
                                foreach (var groupitem in AIitem.Group)
                                {
                                    var data = aIs.SingleOrDefault(s => s.Index == groupitem);
                                    if (data != null)
                                    {
                                        int index = aIs.IndexOf(data);
                                        AIGroupcheckedComboBoxEdit.Properties.Items[index].CheckState = CheckState.Checked;
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }
        #endregion

        #region DI更新聚焦數值
        private void Change_DIText()
        {
            DItoggleSwitch.IsOn = Convert.ToBoolean(DIgridView.GetRowCellValue(FocusDIIndex, "AlarmFlag"));
            DIDeviceNamelabelControl.Text = $"點位名稱 : {DIgridView.GetRowCellValue(FocusDIIndex, "FieldName")}";
            DINametextEdit.Text = $"{DIgridView.GetRowCellValue(FocusDIIndex, "ShowName")}";
            DIGeneralFlagcomboBoxEdit.SelectedIndex = Convert.ToInt32(DIgridView.GetRowCellValue(FocusDIIndex, "GeneralFlag"));
            DIResettoggleSwitch.IsOn = Convert.ToBoolean(DIgridView.GetRowCellValue(FocusDIIndex, "ResetFlag"));
            DIMessgaetextEdit.Text = $"{DIgridView.GetRowCellValue(FocusDIIndex, "Message")}";
            string[] digroup = DIgridView.GetRowCellValue(FocusDIIndex, "Group") as string[];
            FocusDI = new DI()
            {
                AlarmFlag = Convert.ToBoolean(DIgridView.GetRowCellValue(FocusDIIndex, "AlarmFlag")),
                FieldName = $"{DIgridView.GetRowCellValue(FocusDIIndex, "FieldName")}",
                ShowName = $"{DIgridView.GetRowCellValue(FocusDIIndex, "ShowName")}",
                FieldNum = Convert.ToInt32(DIgridView.GetRowCellValue(FocusDIIndex, "FieldNum")),
                GeneralFlag = Convert.ToBoolean(DIgridView.GetRowCellValue(FocusDIIndex, "GeneralFlag")),
                ResetFlag = Convert.ToBoolean(DIgridView.GetRowCellValue(FocusDIIndex, "ResetFlag")),
                Message = $"{DIgridView.GetRowCellValue(FocusDIIndex, "Message")}",
                Group = digroup
            };
            FieldName = $"{DIgridView.GetRowCellValue(FocusDIIndex, "FieldName")}";
            if (DIGroupcheckedComboBoxEdit.Properties.Items.Count > 0)
            {
                for (int i = 0; i < DIGroupcheckedComboBoxEdit.Properties.Items.Count; i++)
                {
                    DIGroupcheckedComboBoxEdit.Properties.Items[i].CheckState = CheckState.Unchecked;
                }
                IList<GroupSetting> dIs = DIGroupcheckedComboBoxEdit.Properties.DataSource as IList<GroupSetting>;
                if (FocusDI != null)
                {
                    var IPdata = PointSettings.SingleOrDefault(s => s.DataBaseName == DataBaseName);
                    if (IPdata != null)
                    {
                        var Table = IPdata.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);
                        if (Table != null)
                        {
                            var DIitem = Table.DIs.SingleOrDefault(g => g.FieldName == FieldName);
                            if (DIitem.Group != null)
                            {
                                foreach (var groupitem in DIitem.Group)
                                {
                                    var data = dIs.SingleOrDefault(s => s.Index == groupitem);
                                    if (data != null)
                                    {
                                        int index = dIs.IndexOf(data);
                                        DIGroupcheckedComboBoxEdit.Properties.Items[index].CheckState = CheckState.Checked;
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
        #endregion

        #region Enums更新聚焦數值
        private void Change_EnumsText()
        {
            EnumstoggleSwitch.IsOn = Convert.ToBoolean(EnumsgridView.GetRowCellValue(FocusEnumsIndex, "AlarmFlag"));
            EnumsDeviceNamelabelControl.Text = $"點位名稱 : {EnumsgridView.GetRowCellValue(FocusEnumsIndex, "FieldName")}";
            EnumsNametextEdit.Text = $"{EnumsgridView.GetRowCellValue(FocusEnumsIndex, "ShowName")}";
            EnumsDescribetextEdit.Text = $"{EnumsgridView.GetRowCellValue(FocusEnumsIndex, "EnumsDescribe")}";
            string[] digroup = EnumsgridView.GetRowCellValue(FocusEnumsIndex, "Group") as string[];
            FocusEnums = new Enumses()
            {
                AlarmFlag = Convert.ToBoolean(EnumsgridView.GetRowCellValue(FocusEnumsIndex, "AlarmFlag")),
                FieldName = $"{EnumsgridView.GetRowCellValue(FocusEnumsIndex, "FieldName")}",
                ShowName = $"{EnumsgridView.GetRowCellValue(FocusEnumsIndex, "ShowName")}",
                FieldNum = Convert.ToInt32(EnumsgridView.GetRowCellValue(FocusEnumsIndex, "FieldNum")),
                EnumsDescribe = $"{EnumsgridView.GetRowCellValue(FocusEnumsIndex, "EnumsDescribe")}",
                Group = digroup
            };
            FieldName = $"{EnumsgridView.GetRowCellValue(FocusEnumsIndex, "FieldName")}";
            if (EnumsGroupcheckedComboBoxEdit.Properties.Items.Count > 0)
            {
                for (int i = 0; i < EnumsGroupcheckedComboBoxEdit.Properties.Items.Count; i++)
                {
                    EnumsGroupcheckedComboBoxEdit.Properties.Items[i].CheckState = CheckState.Unchecked;
                }
                IList<GroupSetting> dIs = EnumsGroupcheckedComboBoxEdit.Properties.DataSource as IList<GroupSetting>;
                if (FocusEnums != null)
                {
                    var IPdata = PointSettings.SingleOrDefault(s => s.DataBaseName == DataBaseName);
                    if (IPdata != null)
                    {
                        var Table = IPdata.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);
                        if (Table != null)
                        {
                            var Enumsitem = Table.Enumses.SingleOrDefault(g => g.FieldName == FieldName);
                            if (Enumsitem.Group != null)
                            {
                                foreach (var groupitem in Enumsitem.Group)
                                {
                                    var data = dIs.SingleOrDefault(s => s.Index == groupitem);
                                    if (data != null)
                                    {
                                        int index = dIs.IndexOf(data);
                                        EnumsGroupcheckedComboBoxEdit.Properties.Items[index].CheckState = CheckState.Checked;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
