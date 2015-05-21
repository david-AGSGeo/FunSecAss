using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunSecAss
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void requestAuthenticationButton_Click(object sender, EventArgs e)
        {
            string userName = userNameTextBox.Text;
            string password = passwordTextBox.Text;

            ASreplyTextBox.Text = password;
        }

        private void hidePasswordCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.hidePasswordCheckbox.Checked == true)
            {
                this.passwordTextBox.PasswordChar = '*';
            }
            else 
            {
                this.passwordTextBox.PasswordChar = '\0';
            }
        }

    }
}
