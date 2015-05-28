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
            PTblockList = new List<char[]>();
            ENCblockList = new List<char[]>();
        }


        public string Decrypt(string message, string key)
        {

            
            DividToBlocks(message);
            
            BytePboxDecrypt();

            return blockListToString();
        }



        private void DividToBlocks(string message)
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
            
            foreach (char[] block in ENCblockList)
            {
                Console.WriteLine(block);
                char[] temp = new char[8];              
                for(int j = 0; j < 8; j++)
                {
                   temp[j] = block[DecBytePbox[j]];
                }
                PTblockList.Add(temp);              
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
            return retMessage;
        }

	}
    
}
