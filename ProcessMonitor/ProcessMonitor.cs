using System.Diagnostics;

// Define the namespace for the ProcessMonitor
namespace ProcessMonitor
{
    // Class that monitors and displays information about the system processes.
    class ProcessMonitor
    {
        // Constant for converting bytes to kilobytes.
        private static int BYTES_TO_KB = 1024;
        
        // Constant for converting bytes to megabytes.
        private static int BYTES_TO_MB = 1024 * 1024;

        // Prints the information of the given processes to the console.
        // Displays the process ID, memory usage, and process name in a tabular format.
        private void PrintProcessesInfo(Process[] processes)
        {
            Console.WriteLine("Id\tMemory\tName");
            foreach (Process process in processes)
            {
                string memoryUsage = FormatMemoryUsage(process.WorkingSet64);
                Console.WriteLine($"{process.Id}\t{memoryUsage}\t{process.ProcessName}");
            }
        }

        // Starts the process monitoring loop, allowing the user to sort and view processes based on different criteria.
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

        // Formats the memory usage of a process for display, converting it into a readable string in terms of Bytes, KB, or MB.
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
