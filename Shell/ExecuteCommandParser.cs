using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shell
{
    class ExecuteCommandParser
    {
        public string? ProcessFile { get; private set; }
        public bool RunInBackground { get; private set; }
        public string? InputFile { get; private set; }
        public string? OutputFile { get; private set; }

        public ExecuteCommandParser(string commandLine)
        {
            ParseCommandLine(commandLine);
        }

        private void ParseCommandLine(string commandLine)
        {
            RunInBackground = commandLine.EndsWith("&");
            if (RunInBackground)
            {
                commandLine = commandLine.TrimEnd('&').Trim();
            }

            List<string> segments = Regex.Matches(commandLine, @"[\""].+?[\""]|[^ ]+")
                                         .Cast<Match>()
                                         .Select(m => m.Value.Trim())
                                         .ToList();

            ProcessFile = segments[0];

            for (int i = 1; i < segments.Count; i++)
            {
                if (segments[i] == "<")
                {
                    InputFile = segments[++i].Trim('"');
                }
                else if (segments[i] == ">")
                {
                    OutputFile = segments[++i].Trim('"');
                }
            }
        }
    }
}
