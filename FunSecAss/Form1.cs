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
        private AuthServer myAuthServer;
        
        public Form1()
        {
            InitializeComponent();
            myAuthServer = new AuthServer();
        }

        private void requestAuthenticationButton_Click(object sender, EventArgs e)
        {
            string userName = userNameTextBox.Text;
            string password = passwordTextBox.Text;
            

            using (System.IO.StreamWriter AuthRequest = new System.IO.StreamWriter(@"AuthRequest.txt"))
            {
                AuthRequest.WriteLine("Username: " + userName);
                AuthRequest.WriteLine("Password: " + password);
                AuthRequest.Close();
            }
            
            switch(myAuthServer.authenticate())
            {
                case 0:
                    ASreplyTextBox.Text = "Success";
                    break;
                case -1:
                    ASreplyTextBox.Text = "User Name doesn't exist";
                    break;
                case -2:
                    ASreplyTextBox.Text = "Incorrect Password";
                    break;
                case -3:
                    ASreplyTextBox.Text = "File corrupted";
                    break;
            }
            //ASreplyTextBox.Text = myAuthServer.authenticate().ToString();

            //ASreplyTextBox.Text = password;
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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = myAuthServer.myEncryptor.Encrypt("hello world, this is my test message", "testing1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myAuthServer.keyGenerator();
        }

    }
}
