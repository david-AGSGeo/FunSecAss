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
        private TicketGrantingServer myTicketGrantingServer;
        private MailServer myMailServer;
        private Encryptor myEncryptor;
        private Decryptor myDecryptor;
        
        public Form1()
        {
            InitializeComponent();
            myAuthServer = new AuthServer();
            myTicketGrantingServer = new TicketGrantingServer();
            myMailServer = new MailServer();
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
                    encrypt();
                    myTicketGrantingServer.respondToClient();
                    string DecryptedTGSReply = myDecryptor.Decrypt(getTGSReply(), KtgsTextBox.Text);
                    showTGSReply(DecryptedTGSReply);
                    button2.Enabled = true;    
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
                    AuthReply.Close();
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
                
                temp1 = authReply;
                temp = temp.Remove(0, 8);
                temp = temp.Remove(9, temp.Length-9);
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
                    ticketTextBox.Text = "error";
                    KtgsTextBox.Text = "error";
                }
            }
            else
            {
                ticketTextBox.Text = "error";
                KtgsTextBox.Text = "error";
            }
        }

        public void encrypt()
        {
            string message = "";
            string encryptedMessage = "";
            DateTime timestamp;

            timestamp = DateTime.Now;

            message += "Ticket: ";
            message += ticketTextBox.Text;
            message += " Timestamp: ";
            message += System.DateTime.Now;
            message += " Server Name: ";
            message += "MailServer";

            plaintextTextBox.Text = message;

            encryptedMessage = myEncryptor.Encrypt(message, KtgsTextBox.Text);
            encryptedKtgsTextBox.Text = encryptedMessage;
            writeEncryptedToFile(encryptedMessage);
        }   
    
        public void writeEncryptedToFile(string encryptedMessage)
        {
            using (System.IO.StreamWriter TGSRequest = new System.IO.StreamWriter(@"TGSRequest.txt"))
            {
                TGSRequest.WriteLine("Encrypted Message: " + encryptedMessage);
                TGSRequest.Close();
            }
        }

        public string getTGSReply()
        {
            string line = "";
            string error = "error";


            System.IO.StreamReader TGSReply = new System.IO.StreamReader(@"TGSReply.txt");
            while ((line = TGSReply.ReadLine()) != null)
            {
                if (line.StartsWith("Encrypted Message: "))
                {
                    line = line.Remove(0, 19);
                    textBox2.Text = line;
                    return line;
                }
                else
                {
                    textBox2.Text = "error";
                    TGSReply.Close();
                    return error;
                }
            }
            TGSReply.Close();
            return error;
        }

        public void showTGSReply(string tgsReply)
        {
            textBox3.Text = tgsReply;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (System.IO.StreamWriter MailServerRequest = new System.IO.StreamWriter(@"MailServerRequest.txt"))
            {
                MailServerRequest.WriteLine("KeyCS: " + textBox3.Text);
                MailServerRequest.Close();
            }

            switch(myMailServer.authenticate())
            {
                case 0:
                    Console.WriteLine("Success");
                    break;
                case -1:
                    Console.WriteLine("Key doesn't match");
                    break;
                case -2:
                    Console.WriteLine("Key doesn't exist");
                    break;
                case -3:
                    Console.WriteLine("file unable to read");
                    break;
            }
        }
    }
}
