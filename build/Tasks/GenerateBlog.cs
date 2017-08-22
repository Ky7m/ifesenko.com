﻿using System;
using Cake.Common;
using Cake.Frosting;
using static System.IO.Directory;

namespace Build.Tasks
{
    [Dependency(typeof(NpmInstall))]
    public sealed class GenerateBlog: FrostingTask<Context>
    {
        public override void Run(Context context)
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
                var processStartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    UseShellExecute = false,
                    WorkingDirectory = workingDir,
                    FileName = isRunningOnWindows ? "powershell" : "bash",
                    Arguments = (isRunningOnWindows ? "-Command " : "-c ") + command
                };

                using (var process = System.Diagnostics.Process.Start(processStartInfo))
                {
                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        throw new Exception(string.Format("Exit code {0} from {1}", process.ExitCode, command));
                    }
                }
            }
            
        }
    }
}