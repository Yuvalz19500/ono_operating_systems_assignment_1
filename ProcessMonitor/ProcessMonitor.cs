using System.Diagnostics;



namespace ProcessMonitor
{
    class ProcessMonitor
    {
        private static int BYTES_TO_KB = 1024;
        private static int BYTES_TO_MB = 1024 * 1024;


        private void PrintProcessesInfo(Process[] processes)
        {
            Console.WriteLine("Id\tMemory\tName");
            foreach (Process process in processes)
            {
                string memoryUsage = FormatMemoryUsage(process.WorkingSet64);
                Console.WriteLine($"{process.Id}\t{memoryUsage}\t{process.ProcessName}");
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

        private string FormatMemoryUsage(long memory)
        {
            if (memory < BYTES_TO_KB)
            {
                return memory + " Bytes";
            }

            if (memory < BYTES_TO_MB)
            {
                return (memory / BYTES_TO_KB) + " KB";
            }
            
            return (memory / BYTES_TO_KB) + " MB";
        }
    }
}
 
