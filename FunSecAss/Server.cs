using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunSecAss
{
      
    
    public abstract class Server
    {
        public Encryptor myEncryptor;
        public Decryptor myDecryptor;
        Random rnd;

        public Server() //generic server constructor
        {
            myEncryptor = new Encryptor();
            myDecryptor = new Decryptor();
            System.Threading.Thread.Sleep(100);
            rnd = new Random();
        }

        /// <summary>
        /// generates a random 8 character key and raplaces asterisks with @ symbols as decryptor uses asterisks for padding.
        /// </summary>
        /// <returns>an 8 character key</returns>
        public string keyGenerator()
        {
            string key = "";
            

            for (int i = 0; i < 8; i++)
            {                
                int randomChar = rnd.Next(33, 126);
                if (randomChar == '*') //replace asterisks as they may get trimmed by the decryptor!
                    randomChar = '@'; 
                char c = (char)randomChar;
                key += c;
            }
            return key;
        }
    }
}
