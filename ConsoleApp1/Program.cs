// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Text;

Console.WriteLine("running");
var dir = new DirectoryInfo("logfiles");
if(!dir.Exists)dir.Create();
var filename = DateTime.UtcNow.ToFileTime();
var filePath = $"{dir}/{filename}";
var file = new FileInfo(filePath);
if (!file.Exists) file.Create();

Console.WriteLine("any key to close");
//Console.ReadKey();
