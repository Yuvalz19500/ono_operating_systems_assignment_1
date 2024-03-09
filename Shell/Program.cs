using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Shell
{
    class Program
    {
        static void Main(string[] args)
        {
            Shell shell = new Shell();
            shell.Run();
        }
    }
}
