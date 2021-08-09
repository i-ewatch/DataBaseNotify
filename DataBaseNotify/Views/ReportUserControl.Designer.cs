
namespace DataBaseNotify.Views
{
    partial class ReportUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.timeEdit1 = new DevExpress.XtraEditors.TimeEdit();
            this.SearchsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ReportgridControl = new DevExpress.XtraGrid.GridControl();
            this.ReportgridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportgridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportgridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.timeEdit1);
            this.panelControl1.Controls.Add(this.SearchsimpleButton);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(924, 34);
            this.panelControl1.TabIndex = 2;
            // 
            // timeEdit1
            // 
            this.timeEdit1.Dock = System.Windows.Forms.DockStyle.Left;
            this.timeEdit1.EditValue = new System.DateTime(2021, 6, 1, 0, 0, 0, 0);
            this.timeEdit1.Location = new System.Drawing.Point(125, 2);
            this.timeEdit1.Name = "timeEdit1";
            this.timeEdit1.Properties.AllowFocused = false;
            this.timeEdit1.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.timeEdit1.Properties.Appearance.Options.UseFont = true;
            this.timeEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit1.Properties.Mask.EditMask = "yyyy/MM";
            this.timeEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.timeEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.timeEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.timeEdit1.Properties.TimeEditStyle = DevExpress.XtraEditors.Repository.TimeEditStyle.TouchUI;
            this.timeEdit1.Size = new System.Drawing.Size(99, 30);
            this.timeEdit1.TabIndex = 5;
            // 
            // SearchsimpleButton
            // 
            this.SearchsimpleButton.AllowFocus = false;
            this.SearchsimpleButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.SearchsimpleButton.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.SearchsimpleButton.Appearance.Options.UseBackColor = true;
            this.SearchsimpleButton.Appearance.Options.UseFont = true;
            this.SearchsimpleButton.AppearancePressed.Font = new System.Drawing.Font("Tahoma", 14F);
            this.SearchsimpleButton.AppearancePressed.Options.UseFont = true;
            this.SearchsimpleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.SearchsimpleButton.Location = new System.Drawing.Point(857, 2);
            this.SearchsimpleButton.Name = "SearchsimpleButton";
            this.SearchsimpleButton.Size = new System.Drawing.Size(65, 30);
            this.SearchsimpleButton.TabIndex = 4;
            this.SearchsimpleButton.Text = "查詢";
            this.SearchsimpleButton.Click += new System.EventHandler(this.SearchsimpleButton_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 18F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl1.Location = new System.Drawing.Point(2, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(123, 30);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "查詢條件 : ";
            // 
            // ReportgridControl
            // 
            this.ReportgridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportgridControl.Location = new System.Drawing.Point(0, 34);
            this.ReportgridControl.MainView = this.ReportgridView;
            this.ReportgridControl.Name = "ReportgridControl";
            this.ReportgridControl.Size = new System.Drawing.Size(924, 674);
            this.ReportgridControl.TabIndex = 7;
            this.ReportgridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ReportgridView});
            // 
            // ReportgridView
            // 
            this.ReportgridView.GridControl = this.ReportgridControl;
            this.ReportgridView.Name = "ReportgridView";
            // 
            // ReportUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ReportgridControl);
            this.Controls.Add(this.panelControl1);
            this.Name = "ReportUserControl";
            this.Size = new System.Drawing.Size(924, 708);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportgridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportgridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TimeEdit timeEdit1;
        private DevExpress.XtraEditors.SimpleButton SearchsimpleButton;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl ReportgridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView ReportgridView;
    }
}
