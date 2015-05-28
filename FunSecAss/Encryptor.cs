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
        private List<char[]> PTblockList;
        private List<char[]> ENCblockList;
        private static readonly int[] EncBytePbox = {3,5,1,4,7,0,2,6};
        private static readonly int[] EncColumnPbox = {2,0,3,1};

        public Encryptor()
        {
            PTblockList = new List<char[]>();
            ENCblockList = new List<char[]>();
        }
        
        public string Encrypt(string message, string key)
        {
            

            DivideToBlocks(message);

            BytePboxEncrypt();

            ShiftRows(3);

            MixColumns();

            return blockListToString();

        }

        private void DivideToBlocks(string message)
        {
            int i = 0, j = 0;

            PTblockList.Clear();
            
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
                PTblockList.Add(temp);
                j++;
            }
            while ((j * i) < message.Length);


        }

        private void BytePboxEncrypt()
        {
            
            foreach (char[] block in PTblockList)
            {
                Console.WriteLine(block);
                char[] temp = new char[8];              
                for(int j = 0; j < 8; j++)
                {
                   temp[j] = block[EncBytePbox[j]];
                }
                ENCblockList.Add(temp);              
            }

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

        private string blockListToString()
        {
            string retMessage = "";
            foreach (char[] block in ENCblockList)
            {
                string tempString = new string(block);
                retMessage += tempString;
            }
            return retMessage;
        }

	}
}
