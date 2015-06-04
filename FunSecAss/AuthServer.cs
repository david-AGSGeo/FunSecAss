using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FunSecAss
{
    class AuthServer : Server
    {
        string ticket = "";
        string keyTGS = "";
        string encryptedMessage = "";

        public AuthServer() : base(){}
        /// <summary>
        /// authenticates the user with the username and password enttered.
        /// </summary>
        /// <returns> returns 0 if successfully authenticates
        ///           returns -1 if user doesn't exist
        ///           returns -2 if password is incorrect
        ///           returns -3 if file is corrupted
        /// </returns>
        public int authenticate()
        {
            string line = "";
            string lines = "";
            string userName = "";
            string password = "";
            bool userExists = false;

            System.IO.StreamReader AuthRequest = new System.IO.StreamReader(@"AuthRequest.txt");
            while ((line = AuthRequest.ReadLine()) != null)
            {
                if (line.StartsWith("Username: "))
                {
                    line = line.Remove(0, 10);
                    line = line.Trim();
                    userName = line;
                    System.IO.StreamReader AuthCheck = new System.IO.StreamReader(@"Users.txt");
                    while ((lines = AuthCheck.ReadLine()) != null)
                    {
                        if (lines.StartsWith(userName))
                        {                            
                            password = lines.Remove(0, userName.Length+1);
                            userExists = true;
                        }
                        else
                        {

                        }
                    }
                    AuthCheck.Close();
                    if(!userExists)
                    {
                        AuthRequest.Close();
                        return -1; // user doesn't exist
                    }
                }
                else if (line.StartsWith("Password: "))
                {
                    line = line.Remove(0, 9);
                    line = line.Trim();
                    if(line == password)
                    {
                        generateTicket();
                        generateKeyTGS();
                        writeTicketKeyTGSToFile();
                        encryptMessage(password);
                        writeEncrytedToFile();                       
                    }
                    else
                    {
                        AuthRequest.Close();
                        return -2; // password incorrect
                    }
                }
                else
                {
                    AuthRequest.Close();

                    return -3; //file corupted 
                }

            }
            AuthRequest.Close();
            
            return 0;
        }

        public void generateTicket()
        {
            ticket = keyGenerator();           
        }

        public void generateKeyTGS()
        {
            keyTGS = keyGenerator();
        }

        public void writeTicketKeyTGSToFile()
        {
            using (System.IO.StreamWriter ASTGSComms = new System.IO.StreamWriter(@"ASTGSComms.txt"))
            {
                ASTGSComms.WriteLine("Ticket: " + ticket);
                ASTGSComms.WriteLine("KeyTGS: " + keyTGS);
                ASTGSComms.Close();
            }
        }

        public void writeEncrytedToFile()
        {
            using (System.IO.StreamWriter AuthReply = new System.IO.StreamWriter(@"AuthReply.txt"))
            {
                AuthReply.WriteLine("Encrypted Message: " + encryptedMessage);
                AuthReply.Close();
            }
        }

        public string encryptMessage(string password)
        {
            string message = "";
            string key = "";

            message += "Ticket: ";
            message += ticket;
            message += " KeyTGS: ";
            message += keyTGS;

            key += password;

            encryptedMessage = myEncryptor.Encrypt(message, key, false);
            return encryptedMessage;
        }
    }    
}
