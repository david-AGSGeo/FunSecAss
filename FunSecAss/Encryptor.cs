using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace FunSecAss
{
	public class Encryptor
	{
		
        //public string Key;
        private List<char[]> blockList;
        private static readonly int[] EncBytePbox = {3,5,1,4,7,0,2,6};
        private static readonly int[] EncColumnPbox = {2,0,3,1};

        public Encryptor()
        {
            blockList = new List<char[]>();
        }
        
        public string Encrypt(string message, string key)
        {
            
            //string Encrypted = "";
            DividToBlocks(message);
            //Console.WriteLine(blockList.ElementAt(0));
            BytePboxEncrypt();

            return message;
        }

        private void DividToBlocks(string message)
        {
            int i = 0, j = 0;

            blockList.Clear();
            
            do
            {
                char[] temp = new char[8];
                for (i = 0; i < 8; i++)
                {
                    if (i + (j * 8) < message.Length)
                        temp[i] = message[i + (j * 8)];
                    else
                        temp[i] = '*';
                }
                blockList.Add(null);
                blockList[j] = temp;
                //Console.WriteLine(blockList.ElementAt(j));
                j++;
            }
            while ((j * i) < message.Length);


        }

        private void BytePboxEncrypt()
        {
            char[] temp = new char[8];
            foreach (char[] block in blockList)
            {
                   temp = block;
                   Console.WriteLine(temp);
                for(int j = 0; j < 8; j++)
                {
                    temp = block;
                    block[j] = temp[EncBytePbox[j]];
                }
                Console.WriteLine(block);
            }

        }

	}
}
