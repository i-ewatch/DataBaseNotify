using DataBaseNotify.Configuration;
using DataBaseNotify.Methods;
using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
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
    public partial class SearchUserControl : Field4UserControl
    {
        /// <summary>
        /// 資料庫名稱
        /// </summary>
        private string DataBaseName { get; set; } = string.Empty;
        /// <summary>
        /// 資料表名稱
        /// </summary>
        private string TableName { get; set; } = string.Empty;
        /// <summary>
        /// 欄位名稱
        /// </summary>
        private string FieldName { get; set; }
        /// <summary>
        /// 錯誤視窗
        /// </summary>
        private FlyoutAction action { get; set; } = new FlyoutAction();
        /// <summary>
        /// 資料表資訊(時間欄位)
        /// </summary>
        private List<DataSheet> DataSheets { get; set; } = new List<DataSheet>();
        /// <summary>
        /// AI資訊
        /// </summary>
        private List<AI> AIs { get; set; } = new List<AI>();
        /// <summary>
        /// AI資訊
        /// </summary>
        private List<DI> DIs { get; set; } = new List<DI>();
        /// <summary>
        /// Enumses資訊
        /// </summary>
        private List<Enumses> Enumses { get; set; } = new List<Enumses>();
        public SearchUserControl(My_SQL_Method my_SQL_Method, MS_SQL_Method mS_SQL_Method)
        {
            InitializeComponent();
            My_SQL_Method = my_SQL_Method;
            MS_SQL_Method = mS_SQL_Method;
            InitialValue();

            #region 資料庫名稱下拉選單
            DataBasecomboBoxEdit.ButtonClick += (s, e) =>
            {
                List<string> data = null;
                if (MS_SQL_Method != null)
                {
                    data = MS_SQL_Method.Search_DataBase();
                }
                else if (My_SQL_Method != null)
                {
                    data = My_SQL_Method.Search_DataBase();
                }
                if (data != null)
                {
                    if (DataBasecomboBoxEdit.Properties.Items.Count > 0)
                    {
                        DataBasecomboBoxEdit.Properties.Items.Clear();
                    }
                    foreach (var item in data)
                    {
                        DataBasecomboBoxEdit.Properties.Items.Add(item);
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
                    List<string> data = null;
                    DataBaseName = DataBasecomboBoxEdit.Text;
                    if (MS_SQL_Method != null)
                    {

                        data = MS_SQL_Method.Search_Tables(DataBaseName);
                    }
                    else if (My_SQL_Method != null)
                    {
                        data = My_SQL_Method.Search_Tables(DataBaseName);
                    }
                    if (data != null)
                    {
                        if (TableNamecomboBoxEdit.Properties.Items.Count > 0)
                        {
                            TableNamecomboBoxEdit.Properties.Items.Clear();
                        }
                        foreach (var item in data)
                        {
                            TableNamecomboBoxEdit.Properties.Items.Add(item);
                        }
                        TableNamecomboBoxEdit.ShowPopup();
                    }
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
            ScansimpleButton.Click += (s, e) =>
            {
                if (TableNamecomboBoxEdit.Text != "")
                {
                    TableName = TableNamecomboBoxEdit.Text;
                }
                if (DataBaseName != "" && TableName != "")
                {
                    List<object> data = null;
                    TableName = TableNamecomboBoxEdit.Text;
                    if (MS_SQL_Method != null)
                    {
                        data = MS_SQL_Method.Search_Field(DataBaseName, TableName);
                    }
                    else if (My_SQL_Method != null)
                    {
                        data = My_SQL_Method.Search_Field(DataBaseName, TableName);
                    }
                    if (SearchFieldgridView.Columns.Count > 0)
                    {
                        SearchFieldgridView.Columns.Clear();
                    }
                    SearchFieldgridControl.DataSource = data;
                    SearchFieldgridView.OptionsBehavior.Editable = false;
                    for (int i = 2; i < SearchFieldgridView.Columns.Count; i++)
                    {
                        SearchFieldgridView.Columns[i].Visible = false;
                    }
                    SearchFieldgridView.Columns[0].Caption = "欄位名稱";
                    SearchFieldgridView.Columns[1].Caption = "欄位類型";
                    if (AIs.Count > 0)
                    {
                        AIs.Clear();
                    }
                    if (DIs.Count > 0)
                    {
                        DIs.Clear();
                    }
                    if (Enumses.Count > 0)
                    {
                        Enumses.Clear();
                    }
                    if (DataSheets.Count >0)
                    {
                        DataSheets.Clear();
                    }
                    var pointSetting = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);
                    if (pointSetting != null)
                    {
                        var Table = pointSetting.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);
                        if (Table != null)
                        {
                            AIs.AddRange(Table.AIs);
                            DIs.AddRange(Table.DIs);
                            Enumses.AddRange(Table.Enumses);
                            DataSheets.Add(Table);
                            AIgridControl.RefreshDataSource();
                            DIgridControl.RefreshDataSource();
                            EnumsgridControl.RefreshDataSource();
                            TimegridControl.RefreshDataSource();
                        }
                    }
                }
            };
            #endregion
            #region Save按鈕
            SavesimpleButton.Click += (s, e) =>
             {
                 InitialMethod.Save_Point(PointSettings);
             };
            #endregion

            #region Time GridControl
            TimegridControl.DataSource = DataSheets;
            for (int i = 0; i < TimegridView.Columns.Count; i++)
            {
                TimegridView.Columns[i].Visible = false;
                TimegridView.Columns[i].OptionsColumn.AllowEdit = false;
            }
            TimegridView.Columns["TimeTypeEnum"].Caption = "欄位類型";
            TimegridView.Columns["TimeTypeEnum"].Visible = true;
            TimegridView.Columns["TimeName"].Caption = "欄位名稱";
            TimegridView.Columns["TimeName"].Visible = true;
            TimegridView.RowDeleting += (s, e) =>
              {
                  ColumnView view = (ColumnView)s;
                  string Name = Convert.ToString(view.GetListSourceRowCellValue(e.ListSourceIndex, "TimeName"));
                  var PointSetting = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);
                  if (PointSetting != null)
                  {
                      var Table = PointSetting.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);
                      if (Table != null)
                      {
                          Table.TimeName = string.Empty;
                          if (Table.AIs.Count == 0 && Table.DIs.Count == 0 && Table.Enumses.Count == 0 && (Table.TimeName == null || Table.TimeName == ""))
                          {
                              for (int i = 0; i < PointSetting.DataSheets.Count; i++)
                              {
                                  if (PointSetting.DataSheets[i].DataSheetName == TableName)
                                  {
                                      PointSetting.DataSheets.RemoveAt(i);
                                  }
                              }
                          }
                      }
                      if (PointSetting.DataSheets.Count == 0)
                      {
                          for (int i = 0; i < PointSettings.Count; i++)
                          {
                              if (PointSettings[i].DataBaseName == DataBaseName)
                              {
                                  PointSettings.RemoveAt(i);
                              }
                          }
                      }
                  }
              };
            #endregion
            #region Add Time按鈕
            Add_TimesimpleButton.Click += (s, e) =>
              {
                  if (SearchFieldgridView.FocusedRowHandle > -1)
                  {
                      var PointSetting = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);//資料庫名稱
                      if (PointSetting != null)
                      {
                          var Table = PointSetting.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);//資料表名稱
                          if (Table != null)
                          {
                              Table.TimeName = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}";
                              Table.TimeTypeEnum = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[1].FieldName)}";
                              DataSheets.Clear();
                              DataSheets.Add(Table);
                          }
                          else
                          {
                              DataSheet dataSheet = new DataSheet()
                              {
                                  DataSheetName = TableName,
                                  DataSheetNum = PointSetting.DataSheets.Count,
                                  TimeName = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}",
                                  TimeTypeEnum = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[1].FieldName)}"
                              };
                              PointSetting.DataSheets.Add(dataSheet);
                              DataSheets.Clear();
                              DataSheets.Add(dataSheet);
                          }
                      }
                      else
                      {
                          PointSetting pointSetting = new PointSetting()
                          {
                              DataBaseName = DataBaseName,
                              DataBaseNum = PointSettings.Count
                          };
                          DataSheet dataSheet = new DataSheet()
                          {
                              DataSheetName = TableName,
                              DataSheetNum = 0,
                              TimeName = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}",
                              TimeTypeEnum = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[1].FieldName)}"
                          };
                          pointSetting.DataSheets.Add(dataSheet);
                          PointSettings.Add(pointSetting);
                          DataSheets.Clear();
                          DataSheets.Add(dataSheet);
                      }
                      TimegridControl.RefreshDataSource();
                  }
              };
            #endregion
            #region Delete Time按鈕
            TimeDeletesimpleButton.Click += (s, e) =>
              {
                  TimegridView.DeleteSelectedRows();
              };
            #endregion

            #region AI GridControl
            AIgridControl.DataSource = AIs;
            for (int i = 0; i < AIgridView.Columns.Count; i++)
            {
                AIgridView.Columns[i].Visible = false;
                AIgridView.Columns[i].OptionsColumn.AllowEdit = false;
            }
            AIgridView.Columns["FieldName"].Caption = "欄位名稱";
            AIgridView.Columns["FieldName"].Visible = true;
            AIgridView.RowDeleting += (s, e) =>
            {
                ColumnView view = (ColumnView)s;
                string Name = Convert.ToString(view.GetListSourceRowCellValue(e.ListSourceIndex, "FieldName"));
                var PointSetting = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);
                if (PointSetting != null)
                {
                    var Table = PointSetting.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);
                    if (Table != null)
                    {
                        for (int i = 0; i < Table.AIs.Count; i++)
                        {
                            if (Table.AIs[i].FieldName == Name)
                            {
                                Table.AIs.RemoveAt(i);
                            }
                        }
                        if (Table.AIs.Count == 0 && Table.DIs.Count == 0 && Table.Enumses.Count == 0 && (Table.TimeName == null || Table.TimeName == ""))
                        {
                            for (int i = 0; i < PointSetting.DataSheets.Count; i++)
                            {
                                if (PointSetting.DataSheets[i].DataSheetName == TableName)
                                {
                                    PointSetting.DataSheets.RemoveAt(i);
                                }
                            }
                        }
                    }
                    if (PointSetting.DataSheets.Count == 0)
                    {
                        for (int i = 0; i < PointSettings.Count; i++)
                        {
                            if (PointSettings[i].DataBaseName == DataBaseName)
                            {
                                PointSettings.RemoveAt(i);
                            }
                        }
                    }
                }
            };
            #endregion
            #region Add AI按鈕
            Add_AIsimpleButton.Click += (s, e) =>
             {
                 if (SearchFieldgridView.FocusedRowHandle > -1)
                 {
                     var PointSetting = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);//資料庫名稱
                     if (PointSetting != null)
                     {
                         var Table = PointSetting.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);//資料表名稱
                         if (Table != null)
                         {
                             var data = Table.AIs.SingleOrDefault(g => g.FieldName == $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}");//欄位名稱
                             if (data == null)
                             {
                                 AI aI = new AI() { AlarmFlag = false, FieldName = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}", FieldNum = Table.AIs.Count };
                                 Table.AIs.Add(aI);
                                 AIs.Clear();
                                 AIs.AddRange(Table.AIs);
                             }
                             else
                             {
                                 action.Caption = "新增AI點位錯誤";
                                 action.Description = $"已有 {SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)} 欄位存在";
                                 action.Commands.Add(FlyoutCommand.OK);
                                 FlyoutDialog.Show(FindForm().FindForm(), action);
                             }
                         }
                         else
                         {
                             AI aI = new AI() { AlarmFlag = false, FieldName = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}", FieldNum = 0 };
                             DataSheet dataSheet = new DataSheet()
                             {
                                 DataSheetName = TableName,
                                 DataSheetNum = PointSetting.DataSheets.Count
                             };
                             dataSheet.AIs.Add(aI);
                             PointSetting.DataSheets.Add(dataSheet);
                             AIs.Clear();
                             AIs.AddRange(dataSheet.AIs);
                         }
                     }
                     else
                     {
                         PointSetting pointSetting = new PointSetting()
                         {
                             DataBaseName = DataBaseName,
                             DataBaseNum = PointSettings.Count
                         };
                         AI aI = new AI() { AlarmFlag = false, FieldName = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}", FieldNum = 0 };
                         DataSheet dataSheet = new DataSheet()
                         {
                             DataSheetName = TableName,
                             DataSheetNum = 0
                         };
                         dataSheet.AIs.Add(aI);
                         pointSetting.DataSheets.Add(dataSheet);
                         PointSettings.Add(pointSetting);
                         AIs.Clear();
                         AIs.AddRange(dataSheet.AIs);
                     }
                     AIgridControl.RefreshDataSource();
                 }
             };
            #endregion
            #region Delete AI按鈕
            AIDeletesimpleButton.Click += (s, e) =>
             {
                 AIgridView.DeleteSelectedRows();
             };
            #endregion

            #region DI GridControl
            DIgridControl.DataSource = DIs;
            for (int i = 0; i < DIgridView.Columns.Count; i++)
            {
                DIgridView.Columns[i].Visible = false;
                DIgridView.Columns[i].OptionsColumn.AllowEdit = false;
            }
            DIgridView.Columns["FieldName"].Caption = "欄位名稱";
            DIgridView.Columns["FieldName"].Visible = true;
            DIgridView.RowDeleting += (s, e) =>
            {
                ColumnView view = (ColumnView)s;
                string Name = Convert.ToString(view.GetListSourceRowCellValue(e.ListSourceIndex, "FieldName"));
                var PointSetting = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);
                if (PointSetting != null)
                {
                    var Table = PointSetting.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);
                    if (Table != null)
                    {
                        for (int i = 0; i < Table.DIs.Count; i++)
                        {
                            if (Table.DIs[i].FieldName == Name)
                            {
                                Table.DIs.RemoveAt(i);
                            }
                        }
                        if (Table.AIs.Count == 0 && Table.DIs.Count == 0 && Table.Enumses.Count == 0 && (Table.TimeName == null || Table.TimeName == ""))
                        {
                            for (int i = 0; i < PointSetting.DataSheets.Count; i++)
                            {
                                if (PointSetting.DataSheets[i].DataSheetName == TableName)
                                {
                                    PointSetting.DataSheets.RemoveAt(i);
                                }
                            }
                        }
                    }
                    if (PointSetting.DataSheets.Count == 0)
                    {
                        for (int i = 0; i < PointSettings.Count; i++)
                        {
                            if (PointSettings[i].DataBaseName == DataBaseName)
                            {
                                PointSettings.RemoveAt(i);
                            }
                        }
                    }
                }
            };
            #endregion
            #region Add DI按鈕
            Add_DIsimpleButton.Click += (s, e) =>
            {
                if (SearchFieldgridView.FocusedRowHandle > -1)
                {
                    var PointSetting = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);//資料庫名稱
                    if (PointSetting != null)
                    {
                        var Table = PointSetting.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);//資料表名稱
                        if (Table != null)
                        {
                            var data = Table.DIs.SingleOrDefault(g => g.FieldName == $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}");//欄位名稱
                            if (data == null)
                            {
                                DI dI = new DI() { AlarmFlag = false, FieldName = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}", FieldNum = Table.DIs.Count };
                                Table.DIs.Add(dI);
                                DIs.Clear();
                                DIs.AddRange(Table.DIs);
                            }
                            else
                            {
                                action.Caption = "新增DI點位錯誤";
                                action.Description = $"已有 {SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)} 欄位存在";
                                action.Commands.Add(FlyoutCommand.OK);
                                FlyoutDialog.Show(FindForm().FindForm(), action);
                            }
                        }
                        else
                        {
                            DI dI = new DI() { AlarmFlag = false, FieldName = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}", FieldNum = 0 };
                            DataSheet dataSheet = new DataSheet()
                            {
                                DataSheetName = TableName,
                                DataSheetNum = PointSetting.DataSheets.Count
                            };
                            dataSheet.DIs.Add(dI);
                            PointSetting.DataSheets.Add(dataSheet);
                            DIs.Clear();
                            DIs.AddRange(dataSheet.DIs);
                        }
                    }
                    else
                    {
                        PointSetting pointSetting = new PointSetting()
                        {
                            DataBaseName = DataBaseName,
                            DataBaseNum = PointSettings.Count
                        };
                        DI dI = new DI() { AlarmFlag = false, FieldName = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}", FieldNum = 0 };
                        DataSheet dataSheet = new DataSheet()
                        {
                            DataSheetName = TableName,
                            DataSheetNum = 0
                        };
                        dataSheet.DIs.Add(dI);
                        pointSetting.DataSheets.Add(dataSheet);
                        PointSettings.Add(pointSetting);
                        DIs.Clear();
                        DIs.AddRange(dataSheet.DIs);
                    }
                    DIgridControl.RefreshDataSource();
                }
            };
            #endregion
            #region Delete DI按鈕
            DIDeletesimpleButton.Click += (s, e) =>
            {
                DIgridView.DeleteSelectedRows();
            };
            #endregion

            #region Enums GridControl
            EnumsgridControl.DataSource = Enumses;
            for (int i = 0; i < EnumsgridView.Columns.Count; i++)
            {
                EnumsgridView.Columns[i].Visible = false;
                EnumsgridView.Columns[i].OptionsColumn.AllowEdit = false;
            }
            EnumsgridView.Columns["FieldName"].Caption = "欄位名稱";
            EnumsgridView.Columns["FieldName"].Visible = true;
            EnumsgridView.RowDeleting += (s, e) =>
            {
                ColumnView view = (ColumnView)s;
                string Name = Convert.ToString(view.GetListSourceRowCellValue(e.ListSourceIndex, "FieldName"));
                var PointSetting = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);
                if (PointSetting != null)
                {
                    var Table = PointSetting.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);
                    if (Table != null)
                    {
                        for (int i = 0; i < Table.Enumses.Count; i++)
                        {
                            if (Table.Enumses[i].FieldName == Name)
                            {
                                Table.Enumses.RemoveAt(i);
                            }
                        }
                        if (Table.AIs.Count == 0 && Table.DIs.Count == 0 && Table.Enumses.Count == 0 && (Table.TimeName == null || Table.TimeName == ""))
                        {
                            for (int i = 0; i < PointSetting.DataSheets.Count; i++)
                            {
                                if (PointSetting.DataSheets[i].DataSheetName == TableName)
                                {
                                    PointSetting.DataSheets.RemoveAt(i);
                                }
                            }
                        }
                    }
                    if (PointSetting.DataSheets.Count == 0)
                    {
                        for (int i = 0; i < PointSettings.Count; i++)
                        {
                            if (PointSettings[i].DataBaseName == DataBaseName)
                            {
                                PointSettings.RemoveAt(i);
                            }
                        }
                    }
                }
            };
            #endregion
            #region Add Enums按鈕
            Add_EnumssimpleButton.Click += (s, e) =>
            {
                if (SearchFieldgridView.FocusedRowHandle > -1)
                {
                    var PointSetting = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);//資料庫名稱
                    if (PointSetting != null)
                    {
                        var Table = PointSetting.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);//資料表名稱
                        if (Table != null)
                        {
                            var data = Table.Enumses.SingleOrDefault(g => g.FieldName == $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}");//欄位名稱
                            if (data == null)
                            {
                                Enumses Enums = new Enumses() { AlarmFlag = false, FieldName = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}", FieldNum = Table.Enumses.Count };
                                Table.Enumses.Add(Enums);
                                this.Enumses.Clear();
                                this.Enumses.AddRange(Table.Enumses);
                            }
                            else
                            {
                                action.Caption = "新增Enums點位錯誤";
                                action.Description = $"已有 {SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)} 欄位存在";
                                action.Commands.Add(FlyoutCommand.OK);
                                FlyoutDialog.Show(FindForm().FindForm(), action);
                            }
                        }
                        else
                        {
                            Enumses Enums = new Enumses() { AlarmFlag = false, FieldName = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}", FieldNum = 0 };
                            DataSheet dataSheet = new DataSheet()
                            {
                                DataSheetName = TableName,
                                DataSheetNum = PointSetting.DataSheets.Count
                            };
                            dataSheet.Enumses.Add(Enums);
                            PointSetting.DataSheets.Add(dataSheet);
                            Enumses.Clear();
                            Enumses.AddRange(dataSheet.Enumses);
                        }
                    }
                    else
                    {
                        PointSetting pointSetting = new PointSetting()
                        {
                            DataBaseName = DataBaseName,
                            DataBaseNum = PointSettings.Count
                        };
                        Enumses Enums = new Enumses() { AlarmFlag = false, FieldName = $"{SearchFieldgridView.GetListSourceRowCellValue(SearchFieldgridView.FocusedRowHandle, SearchFieldgridView.Columns[0].FieldName)}", FieldNum = 0 };
                        DataSheet dataSheet = new DataSheet()
                        {
                            DataSheetName = TableName,
                            DataSheetNum = 0
                        };
                        dataSheet.Enumses.Add(Enums);
                        pointSetting.DataSheets.Add(dataSheet);
                        PointSettings.Add(pointSetting);
                        Enumses.Clear();
                        Enumses.AddRange(dataSheet.Enumses);
                    }
                    EnumsgridControl.RefreshDataSource();
                }
            };
            #endregion
            #region Delete Enums按鈕
            EnumsDeletesimpleButton.Click += (s, e) =>
            {
                EnumsgridView.DeleteSelectedRows();
            };
            #endregion
        }
        public override void InitialValue()
        {
            PointSettings = InitialMethod.Load_Point();
            if (DataBaseName != "" && TableName != "")
            {
                if (AIs.Count > 0)
                {
                    AIs.Clear();
                }
                if (DIs.Count > 0)
                {
                    DIs.Clear();
                }
                if (Enumses.Count > 0)
                {
                    Enumses.Clear();
                }
                var pointSetting = PointSettings.SingleOrDefault(g => g.DataBaseName == DataBaseName);
                if (pointSetting != null)
                {
                    var Table = pointSetting.DataSheets.SingleOrDefault(g => g.DataSheetName == TableName);
                    if (Table != null)
                    {
                        AIs.AddRange(Table.AIs);
                        DIs.AddRange(Table.DIs);
                        Enumses.AddRange(Table.Enumses);
                        AIgridControl.RefreshDataSource();
                        DIgridControl.RefreshDataSource();
                        EnumsgridControl.RefreshDataSource();
                    }
                }
            }
        }
    }
}
