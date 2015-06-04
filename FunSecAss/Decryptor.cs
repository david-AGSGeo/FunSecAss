using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunSecAss
{
    public class Decryptor
    {
        private const int ITERATIONS = 6;
        private List<char[]> PTblockList;
        private List<char[]> ENCblockList;
        private List<char[]> SubKeyList;
        private static readonly int[] DecBytePbox = {5,2,6,0,3,1,7,4};

        public Decryptor()
        {

        }

        /// <summary>
        /// Called from the server, this decrypts the cyphertext and returns the decrypted plaintext
        /// </summary>
        /// <param name="message">cyphertext to decrypt</param>
        /// <param name="key">Duuuuh....</param>
        /// <param name="debugFlag"> Whether to display to debug console</param>
        /// <returns>plaintext as a string</returns>
        public string Decrypt(string message, string key, bool debugFlag)
        {
            PTblockList = new List<char[]>();
            ENCblockList = new List<char[]>();
            SubKeyList = new List<char[]>();

            string myMessage = "";
            foreach (char mychar in message) //drop the unicode chars back into the original range
            {
                myMessage += (char)((int)mychar - 256);
            }


            DivideToBlocks(myMessage);
            if (debugFlag)
            {
                Console.WriteLine("********* DECRYPTION ********");
                Console.WriteLine("");

                Console.WriteLine("message divided into blocks:");
                displayBLtoConsole(ENCblockList);
                Console.WriteLine("");
            }
            
            GetSubKeys(ITERATIONS, key);

            if (debugFlag)
            {
                Console.WriteLine("Get the list of Subkeys for each iteration: ");
                displayBLtoConsole(SubKeyList);
                Console.WriteLine("");
            }
            


            for (int iter = 0; iter < ITERATIONS; iter++)
            {
                if (debugFlag)
                {
                    Console.WriteLine("ITERATION NUMBER " + (iter + 1).ToString());
                    //Console.Write("SUBKEY: ");
                    //Console.WriteLine(SubKeyList.ElementAt((ITERATIONS - 1) - iter));
                    Console.WriteLine("");
                }
                XorWithKey(SubKeyList.ElementAt((ITERATIONS - 1) - iter)); //note: for decryption, use the last key first!

                if (debugFlag)
                {
                    

                    Console.WriteLine("message XORed with iteration subkey:");
                    displayBLtoConsole(ENCblockList);
                    Console.WriteLine("");
                }
                ShiftRows();
                if (debugFlag)
                {
                    Console.WriteLine("Rows shifted left by varying ammounts:");
                    displayBLtoConsole(ENCblockList);
                    Console.WriteLine("");
                }

                BytePboxDecrypt();
                if (debugFlag)
                {
                    Console.WriteLine("Rows put through reverse P-box:");
                    displayBLtoConsole(PTblockList);
                    Console.WriteLine("");
                }

                ENCblockList = PTblockList.ToList();
            }
            return blockListToString();
        }

        /// <summary>
        /// generates a subkey for each iteration, based on XORing with a shifted version
        /// of the previous subkey
        /// </summary>
        /// <param name="iterations"> how many keys to generate</param>
        /// <param name="key">initial key</param>
        void GetSubKeys(int iterations, string key)
        {
            int numShifts = 1;
            SubKeyList.Add(key.ToCharArray()); //first subkey is the key
            for (int iter = 0; iter < ITERATIONS - 1; iter++) //add ITERATIONS -1 subkeys
            {
                char[] temp = new char[8];
                char[] newKey = new char[8];
                char[] lastKey = SubKeyList.ElementAt(iter);

                for (int i = 0; i < 8; i++) // temp is the last key right shifted by 1
                {
                    temp[i] = lastKey[(i + numShifts) % 8];
                }

                for (int j = 0; j < 8; j++) //the new subkey is the last subkey XORed with temp 
                {
                    newKey[j] = (char)((int)lastKey[j] ^ (int)temp[j]);
                }
                SubKeyList.Add(newKey);
            }
        }

        /// <summary>
        /// divides the message into blocks of 8 charachters. Padds the last block with asterisks 
        /// </summary>
        /// <param name="message"></param>
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

        /// <summary>
        /// puts each block through a P-box defined at the top of the class
        /// </summary>
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
            PTblockList = TempblockList;
        }


        /// <summary>
        /// shifts each row (block) of the message left by a different ammount
        /// </summary>
        private void ShiftRows()
        {
            int numShifts = 1;
            List<char[]> TempblockList = new List<char[]>();
            foreach (char[] block in ENCblockList)
            {
                char[] temp = new char[8];
                for (int i = 0; i < 8; i++)
                {
                    temp[i] = block[((((i + (8-numShifts)) % 8)+ 8)% 8 )];
                }
                TempblockList.Add(temp);
                numShifts++;            //each block is shifted by a different ammount
            }
            ENCblockList = TempblockList;
        }

        /// <summary>
        /// XORs each block with the current subkey
        /// </summary>
        /// <param name="key"></param>
        private void XorWithKey(char[] key)
        {
            List<char[]> TempblockList = new List<char[]>();

                foreach (char[] block in ENCblockList)
                {
                    char[] tempBlock = new char[8];
                    for (int j = 0; j < 8; j++)
                        tempBlock[j] = (char)((int)block[j] ^ (int)key[j]);

                    TempblockList.Add(tempBlock);
                
                ENCblockList = TempblockList;
            }
                
        }


        /// <summary>
        /// displays a block list to the debug console, changing any non-displayable 
        /// unicode chars to displayable ones
        /// </summary>
        /// <param name="blockList"></param>
        private void displayBLtoConsole(List<char[]> blockList)
        {
            foreach (char[] block in blockList)
            {
                string s = "";
                for (int j = 0; j < 8; j++) //check that each char is displayable, else add 256. 
                {
                    if (block[j] <= 31 || block[j] >= 127)
                        s += (char)(block[j] + 256);
                    else
                        s += block[j];
                }

                Console.WriteLine(s);
            }

        }


        /// <summary>
        /// takes the final encrypted blocklist and returns it as a single string
        /// </summary>
        /// <returns> the blocklist as a single string</returns>
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
