﻿using System;
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
        private Encryptor myEncryptor;
        private Decryptor myDecryptor;
        
        public Form1()
        {
            InitializeComponent();
            myAuthServer = new AuthServer();
            myEncryptor = new Encryptor();
            myDecryptor = new Decryptor();
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
                    //ASreplyTextBox.Text = "Success";
                    //getAuthReply();
                     string authReply = myDecryptor.Decrypt(getAuthReply(), password);
                     splitAuthReply(authReply);
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

            ////ASreplyTextBox.Text = password;
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
            string s = myAuthServer.myEncryptor.Encrypt("hello world, this is my test message", "testing1");
            textBox1.Text = s;
            textBox1.Text += myAuthServer.myDecryptor.Decrypt(s, "testing1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myAuthServer.keyGenerator();
        }

        public string getAuthReply()
        {
            string line = "";
            string error = "error";

            System.IO.StreamReader AuthReply = new System.IO.StreamReader(@"AuthReply.txt");
            while ((line = AuthReply.ReadLine()) != null)
            {
                if (line.StartsWith("Encrypted Message: "))
                {
                    line = line.Remove(0, 19);
                    ASreplyTextBox.Text = line;
                    return line;
                }
                else
                {
                    ASreplyTextBox.Text = "error";
                    AuthReply.Close();
                    return error;
                }
            }
            AuthReply.Close();
            return error;
        }

        public void splitAuthReply(string authReply)
        {
            string ticket = "";
            string keyTGS = "";
            string temp = "";
            string temp1 = "";
            
            if (authReply.StartsWith("Ticket: "))
            {
                temp = authReply;
                Console.WriteLine(temp); ;
                temp1 = authReply;
                temp = temp.Remove(0, 8);
                temp = temp.Remove(9, 23);
                ticket = temp;
                ticketTextBox.Text = ticket;
                temp1 = temp1.Remove(0, 17);
                if(temp1.StartsWith("KeyTGS: "))
                {
                    temp1 = temp1.Remove(0, 8);
                    keyTGS = temp1;
                    KtgsTextBox.Text = keyTGS;
                }
                else
                {
                    keyTGS = "error";
                    Console.WriteLine("error");
                }
            }
            else
            {
                ticket = "error";
                Console.WriteLine("error");
            }
        }

    }
}
