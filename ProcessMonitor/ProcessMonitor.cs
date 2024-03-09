using System.Diagnostics;



namespace ProcessMonitor
{
    class ProcessMonitor
    {
        private void PrintProcessesInfo(Process[] processes)
        {
            Console.WriteLine("Id\tMemory\tName");
            foreach (Process process in processes)
            {
                string memoryUsage = FormatMemoryUsage(process.WorkingSet64);
                Console.WriteLine("{0}\t{1}\t{2}", process.Id, memoryUsage, process.ProcessName);
            }
        }

        private string FormatMemoryUsage(long memory)
        {
            const int KB = 1024;
            const int MB = KB * 1024;

            if (memory < KB)
            {
                return memory + " Bytes";
            }
            else if (memory < MB)
            {
                return (memory / KB) + " KB";
            }
            else
            {
                return (memory / MB) + " MB";
            }
        }

        public void Run()
        {
            while (true)
            {
                Console.Write("Enter a number (1: PID Asc, 2: PID Desc, 3: Memory Usage Asc, 4: Memory Usage Desc, 0: Quit): ");
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int choice) || choice < 0 || choice > 4)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 0 and 4.");
                    continue;
                }

                if (choice == 0)
                {
                    break;
                }

                Process[] processes = Process.GetProcesses();

                switch (choice)
                {
                    case 1:
                        processes = processes.OrderBy(p => p.Id).ToArray();
                        break;
                    case 2:
                        processes = processes.OrderByDescending(p => p.Id).ToArray();
                        break;
                    case 3:
                        processes = processes.OrderBy(p => p.WorkingSet64).ToArray();
                        break;
                    case 4:
                        processes = processes.OrderByDescending(p => p.WorkingSet64).ToArray();
                        break;
                }

                PrintProcessesInfo(processes);
                Console.WriteLine();
            }
        }
    }
}
 
