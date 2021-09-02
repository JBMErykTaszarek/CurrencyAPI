using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyAPI.Logging
{
    public static class Logger
    {

        public static void LogAction(string message)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Logging\Logs\", DateTime.Now.ToString("dd-MM-yyyy") + "-Logs.txt");

            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path));
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}--{message}");

            }
            
        }
    }
}

