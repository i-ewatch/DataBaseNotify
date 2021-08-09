using DevExpress.XtraBars.Docking2010.Customization;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseNotify.Views
{
    public partial class ReportUserControl : Field4UserControl
    {
        /// <summary>
        /// CSV彙整資料
        /// </summary>
        private List<Dictionary<string, string>> listObjResult { get; set; }
        /// <summary>
        /// Json資料整理
        /// </summary>
        private object dataInformation { get; set; }
        public ReportUserControl()
        {
            InitializeComponent();
            timeEdit1.Time = DateTime.Now;
            //ReportgridControl.BackgroundImage = Image.FromFile($"{MyWorkPath}//Images//backgroundImage.png");
            ReportgridControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            ReportgridView.Appearance.Empty.BackColor = Color.Transparent;
            ReportgridView.Appearance.Row.BackColor = Color.Transparent;
        }
        private void SearchsimpleButton_Click(object sender, EventArgs e)
        {
            #region CSV報表彙整
            string ReportPath = $"{MyWorkPath}Report\\{Convert.ToDateTime(timeEdit1.EditValue):yyyy}\\Alarm_{Convert.ToDateTime(timeEdit1.EditValue):MM}.csv";
            var csv = new List<string[]>();
            if (File.Exists(ReportPath))
            {
                var lines = File.ReadAllLines(ReportPath, encoding: Encoding.Default);
                foreach (string line in lines)
                    csv.Add(line.Split(','));

                var properties = lines[0].Split(',');

                listObjResult = new List<Dictionary<string, string>>();

                for (int i = 1; i < lines.Length; i++)
                {
                    var objResult = new Dictionary<string, string>();
                    for (int j = 0; j < properties.Length; j++)
                        objResult.Add(properties[j], csv[i][j]);

                    listObjResult.Add(objResult);
                }
            }
            else
            {
                listObjResult = null;
            }
            #endregion
            if (ReportgridView.Columns.Count > 0)
            {
                ReportgridView.Columns.Clear();
            }
            if (listObjResult != null)
            {
                if (listObjResult.Count > 0)
                {
                    var data = JsonConvert.SerializeObject(listObjResult);
                    dataInformation = JsonConvert.DeserializeObject(data);
                    #region 報表           
                    ReportgridControl.DataSource = dataInformation;
                    ReportgridView.OptionsView.ColumnAutoWidth = false;
                    for (int i = 0; i < ReportgridView.Columns.Count; i++)
                    {
                        ReportgridView.Columns[i].BestFit();
                        ReportgridView.Columns[i].OptionsColumn.AllowEdit = false;
                    }
                    #endregion
                }
            }
            else
            {
                FlyoutAction action = new FlyoutAction();
                action.Caption = "Search - Error!!";
                action.Description = "Please select complete conditions.";
                action.Commands.Add(FlyoutCommand.OK);
                FlyoutDialog.Show(FindForm(), action);
            }
        }
    }
}
