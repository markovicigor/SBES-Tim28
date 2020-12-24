using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class FileWriter
    {
        public static void WriteToFile(Event e, int id)
        {
            string path = @"..\..\..\SyslogServer\logDatoteka.txt";
            TextWriter tw = new StreamWriter(path, true);



            tw.WriteLine(id + "-" + "{" + e.criticality + "}" + "{" + e.timestamp + "}" + "{" + e.source + "}" +  "{" + e.message + "}" + "{" +  e.eState + "}");

            tw.Close();
        }
    }
}
