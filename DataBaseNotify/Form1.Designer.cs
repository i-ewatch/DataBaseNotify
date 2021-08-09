
namespace DataBaseNotify
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.Home = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.Report = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.Search = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.PointSetting = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.Group = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.Setting = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.LoginbarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.TokenbarButtonItem = new DevExpress.XtraBars.BarButtonItem();
            this.fluentFormDefaultManager1 = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(this.components);
            this.DisplaypanelControl = new DevExpress.XtraEditors.PanelControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.Tokentimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisplaypanelControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.Home,
            this.Report,
            this.Search,
            this.PointSetting,
            this.Group,
            this.Setting});
            this.accordionControl1.Location = new System.Drawing.Point(0, 31);
            this.accordionControl1.Margin = new System.Windows.Forms.Padding(2);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.False;
            this.accordionControl1.OptionsMinimizing.State = DevExpress.XtraBars.Navigation.AccordionControlState.Minimized;
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl1.Size = new System.Drawing.Size(48, 708);
            this.accordionControl1.TabIndex = 1;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // Home
            // 
            this.Home.Hint = "首頁";
            this.Home.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Home.ImageOptions.Image")));
            this.Home.Name = "Home";
            this.Home.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.Home.Tag = "0";
            // 
            // Report
            // 
            this.Report.Hint = "報表";
            this.Report.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Report.ImageOptions.Image")));
            this.Report.Name = "Report";
            this.Report.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.Report.Tag = "1";
            // 
            // Search
            // 
            this.Search.Hint = "資料庫搜尋";
            this.Search.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Search.ImageOptions.Image")));
            this.Search.Name = "Search";
            this.Search.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.Search.Tag = "2";
            // 
            // PointSetting
            // 
            this.PointSetting.Hint = "點位設定";
            this.PointSetting.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("PointSetting.ImageOptions.Image")));
            this.PointSetting.Name = "PointSetting";
            this.PointSetting.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.PointSetting.Tag = "3";
            // 
            // Group
            // 
            this.Group.Hint = "群組設定";
            this.Group.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Group.ImageOptions.Image")));
            this.Group.Name = "Group";
            this.Group.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.Group.Tag = "4";
            // 
            // Setting
            // 
            this.Setting.Hint = "推播設定";
            this.Setting.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("Setting.ImageOptions.Image")));
            this.Setting.Name = "Setting";
            this.Setting.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.Setting.Tag = "5";
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.LoginbarButtonItem,
            this.TokenbarButtonItem});
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Manager = this.fluentFormDefaultManager1;
            this.fluentDesignFormControl1.Margin = new System.Windows.Forms.Padding(2);
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(972, 31);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            this.fluentDesignFormControl1.TitleItemLinks.Add(this.LoginbarButtonItem);
            this.fluentDesignFormControl1.TitleItemLinks.Add(this.TokenbarButtonItem);
            // 
            // LoginbarButtonItem
            // 
            this.LoginbarButtonItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.LoginbarButtonItem.Id = 0;
            this.LoginbarButtonItem.Name = "LoginbarButtonItem";
            this.LoginbarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.LoginbarButtonItem_ItemClick);
            // 
            // TokenbarButtonItem
            // 
            this.TokenbarButtonItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.TokenbarButtonItem.Id = 1;
            this.TokenbarButtonItem.Name = "TokenbarButtonItem";
            this.TokenbarButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.TokenbarButtonItem_ItemClick);
            // 
            // fluentFormDefaultManager1
            // 
            this.fluentFormDefaultManager1.DockingEnabled = false;
            this.fluentFormDefaultManager1.Form = this;
            this.fluentFormDefaultManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.LoginbarButtonItem,
            this.TokenbarButtonItem});
            this.fluentFormDefaultManager1.MaxItemId = 2;
            // 
            // DisplaypanelControl
            // 
            this.DisplaypanelControl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.DisplaypanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplaypanelControl.Location = new System.Drawing.Point(48, 31);
            this.DisplaypanelControl.Name = "DisplaypanelControl";
            this.DisplaypanelControl.Size = new System.Drawing.Size(924, 708);
            this.DisplaypanelControl.TabIndex = 4;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "Logout.png");
            this.imageCollection1.Images.SetKeyName(1, "Customer.png");
            this.imageCollection1.Images.SetKeyName(2, "Employee.png");
            this.imageCollection1.Images.SetKeyName(3, "Key.png");
            // 
            // Tokentimer
            // 
            this.Tokentimer.Tick += new System.EventHandler(this.Tokentimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 739);
            this.Controls.Add(this.DisplaypanelControl);
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.fluentDesignFormControl1);
            this.FluentDesignFormControl = this.fluentDesignFormControl1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.NavigationControl = this.accordionControl1;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DisplaypanelControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement Home;
        private DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager fluentFormDefaultManager1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement Report;
        private DevExpress.XtraBars.Navigation.AccordionControlElement Search;
        private DevExpress.XtraBars.Navigation.AccordionControlElement PointSetting;
        private DevExpress.XtraBars.Navigation.AccordionControlElement Group;
        private DevExpress.XtraBars.Navigation.AccordionControlElement Setting;
        private DevExpress.XtraEditors.PanelControl DisplaypanelControl;
        private DevExpress.XtraBars.BarButtonItem LoginbarButtonItem;
        private DevExpress.XtraBars.BarButtonItem TokenbarButtonItem;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private System.Windows.Forms.Timer Tokentimer;
    }
}

