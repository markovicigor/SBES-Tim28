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
        public static void WriteToFile(Event e)
        {
            string path = @"..\..\..\SyslogServer\logDatoteka.txt";
            TextWriter tw = new StreamWriter(path, true);



            tw.WriteLine(e.id + ";" + e.criticality + ";"  + e.timestamp + ";" +  e.source + ";" + e.message + ";"  +  e.eState);

            tw.Close();
        }
        public static void OverWriteEvent(Dictionary<int, Event> dogadjaji)
        {
            string path = @"..\..\..\SyslogServer\logDatoteka.txt";
            TextWriter tw = new StreamWriter(path, false);


            foreach (Event e in dogadjaji.Values)
            {
                tw.WriteLine(e.id + ";" + e.criticality + ";" + e.timestamp + ";" + e.source + ";" + e.message + ";" + e.eState);
            }
            tw.Close();
        }
        public static Dictionary<int, Event> readFromFile()
        {
            Dictionary<int, Event> noviDogadjaji = new Dictionary<int, Event>();

            string path = @"..\..\..\SyslogServer\logDatoteka.txt";

            FileStream stream = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string line = "";

            while ((line = sr.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                Event e = new Event(Convert.ToInt32(tokens[0]), tokens[1], Convert.ToDateTime(tokens[2]), tokens[3], tokens[4], ((State)Enum.Parse(typeof(State), tokens[5])));

                noviDogadjaji.Add(e.id, e);
            }
            sr.Close();
            stream.Close();

            return noviDogadjaji;


        }
    }
}
