using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Shell
{
    class Shell
    {
        public void ExecuteSingleProcess(string sCommand)
        {
            //Execute a single process.
            //Need to handle & at the end of the command line.
            //Need to handle > and < operators
            //your code here
            
            throw new NotImplementedException();
        }
        
        public void KillProcess(string sCommand)
        {
            string[] asCommand = sCommand.Split(' ');
            int iPid = 0;
            if (int.TryParse(asCommand[1].Trim(), out iPid))
            {
                //got process id
                //your code here
                throw new NotImplementedException();
            }
            else
            {
                //kill process by name
                //your code here
                throw new NotImplementedException();
            }
        }
        public void Execute(string sFullCommand)
        {
            try
            {
                if (sFullCommand == "")
                    return;
                if (sFullCommand.StartsWith("kill"))
                {
                    KillProcess(sFullCommand);
                }
                else if (sFullCommand == "exit")
                {
                    //Exit the program here
                }
                else
                {
                    ExecuteSingleProcess(sFullCommand);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Run()
        {
            int cLines = 0;
            while (true)
            {
                Console.Write(cLines + " >> ");
                string sLine = Console.ReadLine();
                Execute(sLine.Trim());
                cLines++;
            }
        }
    }
}
