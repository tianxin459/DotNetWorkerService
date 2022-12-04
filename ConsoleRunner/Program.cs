// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;
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


Console.WriteLine("ConsoleRunner");

string command = "ConsoleApp1";

RunCmd(command);
