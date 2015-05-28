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


        public TicketGrantingServer() : base(){}

        public void respondToClient()
        {
            string line = "";
            string encryptedMessage = "";
            
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
            encryptedMessage = myEncryptor.Encrypt(keyCS, keyTGS);
            writeEncryptedToFile(encryptedMessage);
            writeEncryptedToFile1(encryptedMessage);
        }
        
        
        public void generateKeyCS()
        {
            keyCS = keyGenerator();
            Console.WriteLine("KeyCS: " + keyCS);
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
                TGSServerComms.WriteLine("Encrypted Message: " + encryptedMessage);
                TGSServerComms.Close();
            }
        }
    }
}
