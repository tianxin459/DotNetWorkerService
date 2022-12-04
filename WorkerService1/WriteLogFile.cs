using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestWokerService
{
    public class WriteLogFile
    {

        public void WriteLog(string str)
        {

            string path = System.Environment.CurrentDirectory;
            using System.IO.StreamWriter streamWriter = new("d:\\logfiles\\log.txt", true);

            streamWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  ffff") + "   ====   " + str+"|"+ path);
        }


    }


}