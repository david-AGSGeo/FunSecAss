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

        public Server() //generic server constructor
        {
            myEncryptor = new Encryptor();
            myDecryptor = new Decryptor();
        }

    }
}
