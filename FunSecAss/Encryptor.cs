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
        


        private List<char[]> PTblockList;
        private List<char[]> ENCblockList;
        private static readonly int[] EncBytePbox = {3,5,1,4,7,0,2,6};
        private static readonly int[] EncColumnPbox = {2,0,3,1};

        public Encryptor()
        {
            //constructor currently empty :)
        }
        
        public string Encrypt(string message, string key)
        {
            PTblockList = new List<char[]>();
            ENCblockList = new List<char[]>();
            
            DivideToBlocks(message);
            for (int iter = 0; iter < 1; iter++)
            {
                ENCblockList.Clear();
                //displayBLtoConsole(PTblockList);
                BytePboxEncrypt();

                ShiftRows(3);

                MixColumns();

                XorWithKey(8, key.ToCharArray());

                PTblockList = ENCblockList;
            }
            return blockListToString();


            

        }

        private void displayBLtoConsole(List<char[]> blockList)
        {
            foreach (char[] block in blockList)
            {
                Console.WriteLine(block);
            }

        }

        private void DivideToBlocks(string message)
        {
            int j = 0;
            PTblockList.Clear();
            
            do
            {
                
                char[] temp = new char[8];
                for (int i = 0; i < 8; i++)
                {
                    if (i + (j * 8) < message.Length)
                        temp[i] = message[i + (j * 8)];
                    else
                        temp[i] = '*';
                }
                
                PTblockList.Add(temp);
                j++;
            }
            while ((j * 8) < message.Length);
        }

        private void BytePboxEncrypt()
        {
            List<char[]> TempblockList = new List<char[]>();
            foreach (char[] block in PTblockList)
            {
               
                char[] temp = new char[8];              
                for(int j = 0; j < 8; j++)
                {
                   temp[j] = block[EncBytePbox[j]];
                }
                TempblockList.Add(temp);              
            }
            ENCblockList = TempblockList;
        }

        private void ShiftRows(int numShifts)
        {
            List<char[]> TempblockList = new List<char[]>();
            foreach (char[] block in ENCblockList)
            {
                char[] temp = new char[8];   
                for (int i = 0; i < 8; i++)
                {
                    temp[i] = block[(i + numShifts) % 8];
                }
                TempblockList.Add(temp);
            }
            ENCblockList = TempblockList;
        }

        private void MixColumns()
        {

        }

        private void XorWithKey(int iterations, char[] key)
        {
            List<char[]> SubKeyList = new List<char[]>();
            List<char[]> TempblockList = new List<char[]>();
            
            //Console.WriteLine("XOR Encrypt:");
            foreach (char[] block in ENCblockList)
            {
                char[] tempBlock = new char[8];

                for (int j = 0; j < 8; j++)
                    tempBlock[j] = (char)((int)block[j] ^ (int)key[j]);

                TempblockList.Add(tempBlock);
                //Console.Write("Before XOR: ");
                //Console.WriteLine(block);
                //Console.Write("After XOR: ");
                //Console.WriteLine(tempBlock);
            }
            ENCblockList = TempblockList;
        }

        private string blockListToString()
        {
            string retMessage = "";
            foreach (char[] block in ENCblockList)
            {
                char[] tempBlock = new char[8];
                for (int j = 0; j < 8; j++)
                    tempBlock[j] = (char)((int)block[j] + 256); //make sure unicode is outside control char range by adding 256 :)
                string tempString = new string(tempBlock);
                retMessage += tempString;
            }
            return retMessage;
        }

	}
}
