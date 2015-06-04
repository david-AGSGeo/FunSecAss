using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunSecAss
{
    class MailServer : Server
    {

        string keyCS = "";
        string keyTGSS = "";

         public MailServer() : base(){}

        public int authenticate()
         {
            string line = "";
            string clientKeyCS = "";

            getKeyCS();

            System.IO.StreamReader MailServerRequest = new System.IO.StreamReader(@"MailServerRequest.txt");
            while ((line = MailServerRequest.ReadLine()) != null)
            {
                if (line.StartsWith("KeyCS: "))
                {
                    clientKeyCS = line.Remove(0, 7);
                    if (clientKeyCS == keyCS)
                    {
                        MailServerRequest.Close();
                        return 0; //success
                    }
                    else
                    {
                        MailServerRequest.Close();
                        return -1; //keys don't match
                    }                    
                }
                else
                {
                    MailServerRequest.Close();
                    return -2; //key doesn't exist
                }
            }
            return -3; //file never read
         }

        private void getKeyCS()
        {
            string line = "";
            string encryptedKeyCS = "";


            System.IO.StreamReader TGSServerComms = new System.IO.StreamReader(@"TGSServerComms.txt");
            while ((line = TGSServerComms.ReadLine()) != null)
            {
                if (line.StartsWith("KeyTGSS: "))
                {
                    keyTGSS = line.Remove(0, 9);
                    Console.WriteLine("KeyTGSS: " + keyTGSS);

                }                
                else if (line.StartsWith("Encrypted Message: "))
                {
                    Console.WriteLine(line);
                    encryptedKeyCS = line.Remove(0, 19);
                    Console.WriteLine("Encrypted KeyCS: " + encryptedKeyCS);
                }
                else
                {
                    Console.WriteLine("error");
                }
            }

            TGSServerComms.Close();

            keyCS = myDecryptor.Decrypt(encryptedKeyCS, keyTGSS, false);
            Console.WriteLine("KeyCS: " + keyCS);
        }

        public string respondToClient()
        {
            string line = "";
            string encryptedMessage = "";
            string originalMessage = "";
            
            System.IO.StreamReader ClientServerComms = new System.IO.StreamReader(@"ClientServerComms.txt");
            while ((line = ClientServerComms.ReadLine()) != null)
            {
                if (line.StartsWith("Encrypted Message: "))
                {
                    encryptedMessage = line.Remove(0, 19);
                }
            }
            ClientServerComms.Close();
            originalMessage = myDecryptor.Decrypt(encryptedMessage, keyCS);
            return originalMessage;
        }
    }
}
