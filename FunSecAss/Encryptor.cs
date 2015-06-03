

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
        private const int ITERATIONS = 8;

        private List<char[]> PTblockList;
        private List<char[]> ENCblockList;
        private List<char[]> SubKeyList;
        private static readonly int[] EncBytePbox = {3,5,1,4,7,0,2,6};
        private static readonly int[] EncColumnPbox = {2,0,3,1};

        public Encryptor()
        {
            
        }
        
        public string Encrypt(string message, string key, bool debugFlag)
        {
            PTblockList = new List<char[]>();
            ENCblockList = new List<char[]>();
            SubKeyList = new List<char[]>();

            GetSubKeys(ITERATIONS, key);
            Console.WriteLine("Get the list of Subkeys for each iteration: ");
            displayBLtoConsole(SubKeyList);
            Console.WriteLine("");
            
            DivideToBlocks(message);

            if (debugFlag)
            {
                Console.WriteLine("********* ENCRYPTION ********");
                Console.WriteLine("");
                 
                Console.WriteLine("message divided into 8 char blocks:");
                displayBLtoConsole(PTblockList);
                Console.WriteLine("");
            }
            for (int iter = 0; iter < ITERATIONS; iter++)
            {
                ENCblockList.Clear();

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

                XorWithKey(8, SubKeyList.ElementAt(iter));
                

                PTblockList = ENCblockList.ToList();
            }
            return blockListToString();


            

        }


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
            Console.WriteLine("message blocks XORed with key (bit level):");
            Console.WriteLine("with 256 added to all Unicode chars in order to skip control chars");
                 
            foreach (char[] block in ENCblockList)
            {
                char[] tempBlock = new char[8];
                for (int j = 0; j < 8; j++)
                    tempBlock[j] = (char)((int)block[j] + 256); //make sure unicode is outside control char range by adding 256 :)
                

                    Console.WriteLine(tempBlock);

                
                string tempString = new string(tempBlock);
                retMessage += tempString;
            }
            return retMessage;
        }

	}
}
