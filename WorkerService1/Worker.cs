using System.Diagnostics;
using System.Text;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }


        #region private method

        void RunCmd(string cmdstr)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe", // use cmd for example, you can run your own test.bat
                Verb = "runas", // run as administrator
                Arguments = "/C " + cmdstr,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            var cmd = new Process();
            cmd.StartInfo = startInfo;

            var output = new StringBuilder();
            cmd.OutputDataReceived += (sender, args) => output.AppendLine(args.Data);
            string stdError = "";

            try
            {
                cmd.Start();
                cmd.BeginOutputReadLine();
                stdError = cmd.StandardError.ReadToEnd();
                cmd.WaitForExit();
            }
            catch (Exception e)
            {
                throw new Exception("OS error while executing: " + e.Message);
            }

            if (cmd.ExitCode != 0)
            {
                throw new Exception("Finished with exit code = " + cmd.ExitCode + ": " + stdError);
            }

            string stdOut = output.ToString();
        }

        #endregion private method

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string path = System.Environment.CurrentDirectory;
                _logger.LogInformation("Worker running at: {time} {path}", DateTimeOffset.Now, path);

                string command = "ConsoleApp1";
                RunCmd(command);
                await Task.Delay(1000, stoppingToken);

            }
        }
    }
}