

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
        private const int ITERATIONS = 6;

        private List<char[]> PTblockList;
        private List<char[]> ENCblockList;
        private List<char[]> SubKeyList;
        private static readonly int[] EncBytePbox = {3,5,1,4,7,0,2,6};

        public Encryptor()
        {
            
        }
        
        /// <summary>
        /// Called from the server, this encrypts the plaintext message and returns the encrypted string
        /// </summary>
        /// <param name="message">the message to encrypt</param>
        /// <param name="key">the key to use</param>
        /// <param name="debugFlag">whether to display to the debug console or not</param>
        /// <returns>the encrypted message</returns>
        public string Encrypt(string message, string key, bool debugFlag)
        {
            PTblockList = new List<char[]>();
            ENCblockList = new List<char[]>();
            SubKeyList = new List<char[]>();

            if (debugFlag)
            {
                Console.WriteLine("");
                Console.WriteLine("NOTE: non-displayable Unicode chars are bumped up by 256 to put them in range.");
                Console.WriteLine("This means there are alot of repeat chars displayed that are actually unique. This is");
                Console.WriteLine("for displaying only: the unique values are still used for encryption and decryption!");
                Console.WriteLine("");
            }

            DivideToBlocks(message);

            if (debugFlag)
            {

                Console.WriteLine("********* ENCRYPTION ********");
                Console.WriteLine("");

                Console.WriteLine("message divided into 8 char blocks:");
                displayBLtoConsole(PTblockList);
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
                ENCblockList.Clear();
                
                if (debugFlag)
                {
                    Console.WriteLine("ITERATION NUMBER " + (iter + 1).ToString());
                    //Console.Write("SUBKEY: ");
                    //Console.WriteLine(SubKeyList.ElementAt(iter));
                    Console.WriteLine("");
                }

                BytePboxEncrypt();
                
                if (debugFlag)
                {
                    Console.WriteLine("message blocks put through a P-Box (byte level):");
                    displayBLtoConsole(ENCblockList);
                    Console.WriteLine("");
                }

                ShiftRows();
                
                if (debugFlag)
                {
                    Console.WriteLine("message blocks shifted by varying numbers (byte level):");
                    displayBLtoConsole(ENCblockList);
                    Console.WriteLine("");
                }

                XorWithKey(SubKeyList.ElementAt(iter));

                if (debugFlag)
                {
                    Console.WriteLine("message blocks XORed with iteration subkey");
                    displayBLtoConsole(ENCblockList);
                    Console.WriteLine("");
                }

                PTblockList = ENCblockList.ToList();
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
            for (int iter = 0; iter < ITERATIONS-1; iter++) //add ITERATIONS -1 subkeys
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
        /// divides the message into blocks of 8 charachters. Padds the last block with asterisks 
        /// </summary>
        /// <param name="message"></param>
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

        /// <summary>
        /// puts each block through a P-box defined at the top of the class
        /// </summary>
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

        /// <summary>
        /// shifts each row (block) of the message right by a different ammount
        /// </summary>
        private void ShiftRows()
        {
            List<char[]> TempblockList = new List<char[]>();
            int numShifts = 1;
            foreach (char[] block in ENCblockList)
            {
                char[] temp = new char[8];   
                for (int i = 0; i < 8; i++)
                {
                    temp[i] = block[(i + numShifts) % 8];
                }
                TempblockList.Add(temp);
                numShifts++;
            }
            ENCblockList = TempblockList;
        }

        /// <summary>
        /// XORs each block with the current subkey
        /// </summary>
        /// <param name="key"></param>
        private void XorWithKey(char[] key)
        {
            List<char[]> SubKeyList = new List<char[]>();
            List<char[]> TempblockList = new List<char[]>();
            
            foreach (char[] block in ENCblockList)
            {
                char[] tempBlock = new char[8];

                for (int j = 0; j < 8; j++)
                    tempBlock[j] = (char)((int)block[j] ^ (int)key[j]);

                TempblockList.Add(tempBlock);

            }
            ENCblockList = TempblockList;
        }

        /// <summary>
        /// takes the final encrypted blocklist and returns it as a single string
        /// </summary>
        /// <returns> the blocklist as a single string</returns>
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
