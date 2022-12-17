using MyTestWokerService;
using System.Diagnostics;
using System.Text;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WriteLogFile _writeLogFile;

        public Worker(ILogger<Worker> logger, WriteLogFile writeLogFile)
        {
            _logger = logger;
            _writeLogFile = writeLogFile;
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
        //启动是执行
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _writeLogFile.WriteLog("启动时间为: " + DateTimeOffset.Now);
            await base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var path = Environment.GetEnvironmentVariable("SCM_Path2", EnvironmentVariableTarget.Machine);
                var pathUser = Environment.GetEnvironmentVariable("SCM_Path2", EnvironmentVariableTarget.User);
                _writeLogFile.WriteLog("执行时间为: " + DateTimeOffset.Now+ $"###|env={path}|env2={pathUser}|###");
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var cmd = "D:\\source\\WorkerService1\\WorkerService1\\bin\\Release\\net6.0\\publish\\ConsoleApp1.exe";
                RunCmd(cmd);
                await Task.Delay(5000, stoppingToken);
            }
        }

        //停止时执行
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _writeLogFile.WriteLog("停止时间为: " + DateTimeOffset.Now);
            await base.StopAsync(cancellationToken);
        }
    }
}


