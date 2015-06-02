using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunSecAss
{
    class TicketGrantingServer :Server
    {
        string keyCS = "";
        string keyTGS = "";
        string keyTGSS = "";


        public TicketGrantingServer() : base(){}

        public void respondToClient()
        {
            string line = "";
            string encryptedMessageForClient = "";
            string encryptedMessageForServer = "";

            generateKeyTGSS();
            generateKeyCS();

            System.IO.StreamReader ASTGSComms = new System.IO.StreamReader(@"ASTGSComms.txt");
            while ((line = ASTGSComms.ReadLine()) != null)
            {
                if (line.StartsWith("KeyTGS: "))
                {
                    keyTGS = line.Remove(0, 8);
                }
                else
                {

                }
            }
            ASTGSComms.Close();
            encryptedMessageForClient = myEncryptor.Encrypt(keyCS, keyTGS);
            Console.WriteLine("KEYCS1: " + keyCS);
            encryptedMessageForServer = myEncryptor.Encrypt(keyCS, keyTGSS);
            Console.WriteLine("KEYCS2: " + keyCS);
            Console.WriteLine("Client: " + encryptedMessageForClient);
            Console.WriteLine("Server: " + encryptedMessageForServer);
            writeEncryptedToFile(encryptedMessageForClient);
            writeEncryptedToFile1(encryptedMessageForServer);
        }
        
        private void generateKeyTGSS()
        {
            keyTGSS = keyGenerator();
        }

        public void generateKeyCS()
        {
            keyCS = keyGenerator();
        }

        public void writeEncryptedToFile(string encryptedMessage)
        {
            using (System.IO.StreamWriter TGSReply = new System.IO.StreamWriter(@"TGSReply.txt"))
            {
                TGSReply.WriteLine("Encrypted Message: " + encryptedMessage);
                TGSReply.Close();
            }
        }

        public void writeEncryptedToFile1(string encryptedMessage)
        {
            using (System.IO.StreamWriter TGSServerComms = new System.IO.StreamWriter(@"TGSServerComms.txt"))
            {
                TGSServerComms.WriteLine("KeyTGSS: " + keyTGSS);
                TGSServerComms.WriteLine("Encrypted Message: " + encryptedMessage);
                TGSServerComms.Close();
            }
        }
    }
}
