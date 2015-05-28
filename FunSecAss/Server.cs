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
            rnd = new Random();
        }

        public string keyGenerator()
        {
            string key = "";
            for (int i = 0; i < 8; i++)
            {                
                int randomChar = rnd.Next(33, 126);
                char c = (char)randomChar;
                key += c;
            }
            return key;
        }
    }
}
