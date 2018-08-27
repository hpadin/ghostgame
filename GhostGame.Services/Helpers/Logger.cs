using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostGame.Services.Helpers
{
    public class Logger
    {
        public static void log(string err)
        {
            // Write the string to a file.append mode is enabled so that the log

            System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\temp\\log.txt", true);
            file.WriteLine(err);

            file.Close();
        }
    }
}

