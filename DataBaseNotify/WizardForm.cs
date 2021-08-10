using DataBaseNotify.Configuration;
using DataBaseNotify.Methods;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseNotify
{
    public partial class WizardForm : DevExpress.XtraEditors.XtraForm
    {
        public WizardForm()
        {
            InitializeComponent();
        }

        private void wizardControl1_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
            if (e.Page.Text == "DataBase Configuration")
            {
                DataBaseSetting setting = new DataBaseSetting()
                {
                    DataBaseTypeEnum = ServerTypecomboBoxEdit.SelectedIndex,
                    DataSource = ServerNametextEdit.Text,
                    UserID = UsertextEdit.Text,
                    Password = PasswordtextEdit.Text
                };
                InitialMethod.Save_DataBaset(setting);
            }
            else if (e.Page.Text == "Notification Basic Configuration")
            {
                NotifyVisible setting = new NotifyVisible()
                {
                    TelePhoneFlag = TelePhonetoggleSwitch.IsOn,
                    LineNotifyFlag = LinetoggleSwitch.IsOn,
                    TelegramFlag = TelegramtoggleSwitch.IsOn
                };
                InitialMethod.Save_NotifyVisible(setting);
            }
        }

        private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
        {
            Application.Restart();
        }

        private void pictureEdit1_MouseDown(object sender, MouseEventArgs e)
        {
            PasswordtextEdit.Properties.UseSystemPasswordChar = false;
        }

        private void pictureEdit1_MouseUp(object sender, MouseEventArgs e)
        {
            PasswordtextEdit.Properties.UseSystemPasswordChar = true;
        }

        private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }
    }
}