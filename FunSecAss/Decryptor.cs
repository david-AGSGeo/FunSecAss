﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunSecAss
{
    public class Decryptor
    {
        private List<char[]> PTblockList;
        private List<char[]> ENCblockList;
        private static readonly int[] DecBytePbox = {5,2,6,0,3,1,7,4};
        private static readonly int[] DecColumnPbox = {1,3,0,2};

        public Decryptor()
        {

        }


        public string Decrypt(string message, string key)
        {
            PTblockList = new List<char[]>();
            ENCblockList = new List<char[]>();

            
            DivideToBlocks(message);

            //XorWithKey(8, key.ToCharArray());

            MixColumns();
            
            ShiftRows(3);
            
            BytePboxDecrypt();

            PTblockList = ENCblockList;
            return blockListToString();
        }



        private void DivideToBlocks(string message)
        {
            int i = 0, j = 0;

            ENCblockList.Clear();
            
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
                ENCblockList.Add(temp);
                j++;
            }
            while ((j * i) < message.Length);


        }

        private void BytePboxDecrypt()
        {
            List<char[]> TempblockList = new List<char[]>();
            foreach (char[] block in ENCblockList)
            {
                //Console.WriteLine(block);
                char[] temp = new char[8];              
                for(int j = 0; j < 8; j++)
                {
                   temp[j] = block[DecBytePbox[j]];
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
                    temp[i] = block[((i + (8-numShifts)) % 8)];
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
            List<char[]> TempblockList = new List<char[]>();
            
            //Console.WriteLine("XOR Decrypt:");

                foreach (char[] block in ENCblockList)
                {
                    char[] tempBlock = new char[8];
                    for (int j = 0; j < 8; j++)
                        tempBlock[j] = (char)((int)block[j] ^ (int)key[j]);

                    TempblockList.Add(tempBlock);
                    //Console.WriteLine("Before XOR: ");
                    //Console.WriteLine(block);
                    //Console.WriteLine("After XOR: ");
                    //Console.WriteLine(tempBlock);
                
                ENCblockList = TempblockList;
            }
                
        }

        private string blockListToString()
        {
            string retMessage = "";
            foreach (char[] block in PTblockList)
            {
                string tempString = new string(block);
                retMessage += tempString;
            }
            retMessage = retMessage.TrimEnd('*');
            return retMessage;
        }

	}
    
}
