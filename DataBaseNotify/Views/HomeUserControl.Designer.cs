
namespace DataBaseNotify.Views
{
    partial class HomeUserControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeUserControl));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.SavesimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.AutoActiontoggleSwitch = new DevExpress.XtraEditors.ToggleSwitch();
            this.StatuslabelControl = new DevExpress.XtraEditors.LabelControl();
            this.TimelabelControl = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ComponentpictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.ComponentimageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AutoActiontoggleSwitch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentpictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentimageCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.SavesimpleButton);
            this.panelControl1.Controls.Add(this.AutoActiontoggleSwitch);
            this.panelControl1.Controls.Add(this.StatuslabelControl);
            this.panelControl1.Controls.Add(this.TimelabelControl);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.ComponentpictureEdit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(924, 708);
            this.panelControl1.TabIndex = 1;
            // 
            // SavesimpleButton
            // 
            this.SavesimpleButton.AllowFocus = false;
            this.SavesimpleButton.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.SavesimpleButton.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.SavesimpleButton.Appearance.Options.UseBackColor = true;
            this.SavesimpleButton.Appearance.Options.UseFont = true;
            this.SavesimpleButton.Location = new System.Drawing.Point(575, 76);
            this.SavesimpleButton.Name = "SavesimpleButton";
            this.SavesimpleButton.Size = new System.Drawing.Size(60, 26);
            this.SavesimpleButton.TabIndex = 15;
            this.SavesimpleButton.Text = "Save";
            this.SavesimpleButton.Click += new System.EventHandler(this.SavesimpleButton_Click);
            // 
            // AutoActiontoggleSwitch
            // 
            this.AutoActiontoggleSwitch.Location = new System.Drawing.Point(366, 76);
            this.AutoActiontoggleSwitch.Name = "AutoActiontoggleSwitch";
            this.AutoActiontoggleSwitch.Properties.AllowFocused = false;
            this.AutoActiontoggleSwitch.Properties.Appearance.Font = new System.Drawing.Font("微軟正黑體", 12F);
            this.AutoActiontoggleSwitch.Properties.Appearance.Options.UseFont = true;
            this.AutoActiontoggleSwitch.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.AutoActiontoggleSwitch.Properties.OffText = "自動通訊停止";
            this.AutoActiontoggleSwitch.Properties.OnText = "自動通訊啟用";
            this.AutoActiontoggleSwitch.Size = new System.Drawing.Size(183, 26);
            this.AutoActiontoggleSwitch.TabIndex = 14;
            // 
            // StatuslabelControl
            // 
            this.StatuslabelControl.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.StatuslabelControl.Appearance.Options.UseFont = true;
            this.StatuslabelControl.Location = new System.Drawing.Point(270, 75);
            this.StatuslabelControl.Name = "StatuslabelControl";
            this.StatuslabelControl.Size = new System.Drawing.Size(44, 28);
            this.StatuslabelControl.TabIndex = 4;
            this.StatuslabelControl.Text = "停止";
            // 
            // TimelabelControl
            // 
            this.TimelabelControl.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.TimelabelControl.Appearance.Options.UseFont = true;
            this.TimelabelControl.Location = new System.Drawing.Point(270, 26);
            this.TimelabelControl.Name = "TimelabelControl";
            this.TimelabelControl.Size = new System.Drawing.Size(9, 27);
            this.TimelabelControl.TabIndex = 3;
            this.TimelabelControl.Text = "-";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(165, 74);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(99, 28);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "運行狀態 :";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微軟正黑體", 16F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(165, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(99, 28);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "系統時間 :";
            // 
            // ComponentpictureEdit
            // 
            this.ComponentpictureEdit.Location = new System.Drawing.Point(29, 10);
            this.ComponentpictureEdit.Name = "ComponentpictureEdit";
            this.ComponentpictureEdit.Properties.AllowFocused = false;
            this.ComponentpictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ComponentpictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.ComponentpictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ComponentpictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.ComponentpictureEdit.Size = new System.Drawing.Size(100, 100);
            this.ComponentpictureEdit.TabIndex = 0;
            this.ComponentpictureEdit.Click += new System.EventHandler(this.ComponentpictureEdit_Click);
            // 
            // ComponentimageCollection
            // 
            this.ComponentimageCollection.ImageSize = new System.Drawing.Size(100, 100);
            this.ComponentimageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ComponentimageCollection.ImageStream")));
            this.ComponentimageCollection.Images.SetKeyName(0, "Play");
            this.ComponentimageCollection.Images.SetKeyName(1, "Stop");
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // HomeUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "HomeUserControl";
            this.Size = new System.Drawing.Size(924, 708);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AutoActiontoggleSwitch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentpictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentimageCollection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton SavesimpleButton;
        private DevExpress.XtraEditors.ToggleSwitch AutoActiontoggleSwitch;
        private DevExpress.XtraEditors.LabelControl StatuslabelControl;
        private DevExpress.XtraEditors.LabelControl TimelabelControl;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.PictureEdit ComponentpictureEdit;
        private DevExpress.Utils.ImageCollection ComponentimageCollection;
        private System.Windows.Forms.Timer timer1;
    }
}
