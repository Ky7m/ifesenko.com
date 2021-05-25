using System;
using System.Diagnostics;
using Cake.Common;
using Cake.Frosting;
using static System.IO.Directory;

namespace Build.Tasks
{
    public sealed class GenerateBlog: FrostingTask<BuildContext>
    {
        public override void Run(BuildContext context)
        {
            ExecuteCommand("\"./node_modules/.bin/hexo clean\"", context.BlogPath);
            ExecuteCommand("\"./node_modules/.bin/hexo generate\"", context.BlogPath);
            
            void ExecuteCommand(string command, string workingDir = null)
            {
                if (string.IsNullOrEmpty(workingDir))
                {
                    workingDir = GetCurrentDirectory();
                }

                var isRunningOnWindows = context.IsRunningOnWindows();
                var processStartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    WorkingDirectory = workingDir,
                    FileName = isRunningOnWindows ? "powershell" : "bash",
                    Arguments = (isRunningOnWindows ? "-Command " : "-c ") + command
                };

                using (var process = Process.Start(processStartInfo))
                {
                    process?.WaitForExit();

                    if (process?.ExitCode != 0)
                    {
                        throw new Exception($"Exit code {process?.ExitCode} from {command}");
                    }
                }
            }
            
        }
    }
}