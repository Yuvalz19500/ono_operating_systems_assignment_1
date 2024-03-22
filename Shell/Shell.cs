using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Shell
{
    class Shell
    {

        public void ExecuteSingleProcess(string sCommand)
        {
            ExecuteCommandParser commandParser = new(sCommand);

            ProcessStartInfo startInfo = new()
            {
                UseShellExecute = false,
                RedirectStandardOutput = !string.IsNullOrEmpty(commandParser.OutputFile),
                RedirectStandardInput = !string.IsNullOrEmpty(commandParser.InputFile),
                FileName = commandParser.ProcessFile,
            };

            Process p = new() { StartInfo = startInfo };
            p.Start();

            if (!string.IsNullOrEmpty(commandParser.InputFile))
            {
                var inputText = File.ReadAllText($"{commandParser.InputFile}.txt");
                p.StandardInput.Write(inputText);

                p.StandardInput.Close();
            }

            if (!commandParser.RunInBackground)
            {
                p.WaitForExit();
            }

            if (!string.IsNullOrEmpty(commandParser.OutputFile))
            {
                StreamWriter sw = new($"{commandParser.OutputFile}.txt");
                sw.WriteLine(p.StandardOutput.ReadToEnd());

                sw.Close();
                p.StandardOutput.Close();
            }
        }
        
        public void KillProcess(string sCommand)
        {
            string[] asCommand = sCommand.Split(' ');
            int iPid;
            if (int.TryParse(asCommand[1].Trim(), out iPid))
            {
                Process p = Process.GetProcessById(iPid);
                p.Kill();
            }
            else
            {
                Process[] processes = Process.GetProcessesByName(asCommand[1]);
                foreach ( Process p in processes )
                {
                    p.Kill();
                }
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
                    Environment.Exit(0);
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
                string? sLine = Console.ReadLine();
                Execute(sLine.Trim());
                cLines++;
            }
        }
    }
}
