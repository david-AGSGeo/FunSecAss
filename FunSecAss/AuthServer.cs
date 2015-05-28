using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FunSecAss
{
    class AuthServer : Server
    {

        public AuthServer() : base(){}

        public int authenticate()
        {
            string line = "";
            string lines = "";
            string userName = "";
            string password = "";
            bool userExists = false;

            System.IO.StreamReader AuthRequest = new System.IO.StreamReader(@"AuthRequest.txt");
            while ((line = AuthRequest.ReadLine()) != null)
            {
                if (line.StartsWith("Username: "))
                {
                    line = line.Remove(0, 10);
                    line = line.Trim();
                    userName = line;
                    System.IO.StreamReader AuthCheck = new System.IO.StreamReader(@"Users.txt");
                    while ((lines = AuthCheck.ReadLine()) != null)
                    {
                        if (lines.StartsWith(userName))
                        {                            
                            password = lines.Remove(0, userName.Length+1);
                            userExists = true;
                        }
                        else
                        {

                        }
                    }
                    AuthCheck.Close();
                    if(!userExists)
                    {
                        return -1;
                    }
                }
                else if (line.StartsWith("Password: "))
                {
                    line = line.Remove(0, 9);
                    line = line.Trim();
                    if(line == password)
                    {
                        //encrypt();
                        
                    }
                    else
                    {
                        AuthRequest.Close();
                        return -2; // pasword incorrect
                    }
                    //password = line;
                }
                else
                {
                    AuthRequest.Close();

                    return -3; //file corupted 
                }

            }
            AuthRequest.Close();
            
            return 0;
        }
    }
}
