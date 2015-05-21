using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace FunSecAss
{
	class Encryptor
	{
		public string Message = "hello world, this is my test message";
        //public string Key;
        private List<char[]> blockList;
        private static readonly int[] EncBytePbox = {3,5,1,4,7,0,2,6};
        private static readonly int[] EncColumnPbox = {2,0,3,1};

  
        
        public string Encrypt()
        {
            blockList = new List<char[]>();
            string Encrypted = "";
            DividToBlocks();
            Console.WriteLine(blockList.ElementAt(0));
            //BytePboxEncrypt();

            Encrypted = Message;

            return Encrypted;
        }

        private void DividToBlocks()
        {
            int i = 0, j = 0;
            char[] temp = new char[8];
           // blockList = new List<char[]>();
            do
            {

                for (i = 0; i < 8; i++)
                {
                    if (i + (j * 8) < Message.Length)
                        temp[i] = Message[i + (j * 8)];
                    else
                        temp[i] = '*';
                }
                blockList.Add(temp);
                Console.WriteLine(blockList.ElementAt(j));
                j++;
            }
            while ((j * i) < Message.Length);
        }

        private void BytePboxEncrypt()
        {
            char[] temp = new char[8];
            for (int i = 0; i < blockList.Count; i++)
            {
                   temp = blockList.ElementAt(i);
                   Console.WriteLine(temp);
                //for(int j = 0; j < 8; j++)
               // {
               //     temp = block;
              //      block[j] = temp[EncBytePbox[j]];
              //  }
                
            }

        }

	}
}
