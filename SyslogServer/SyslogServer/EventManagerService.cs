using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslogServer
{

    public class EventManagerService : IEventManager
    {
        //Audit audit = new Audit();
        static int i = -1;

        public void EventLog(string message)
        {
            int id = getID();

             while(FileWriter.readFromFile().ContainsKey(id))
            {
                id++;
            }
            Event e = new Event(id,"Criticallity", DateTime.Now, "Source", message, SecurityManager.State.CLOSED);




            FileWriter.WriteToFile(e);

            Console.WriteLine("Event je logovan.");
        }

        public int getID()
        {
            int i = FileWriter.readFromFile().Count - 1;
            return ++i;
        }

        public void Test()
        {
            Console.WriteLine("Komunikacija uspesna.");
        }
    }
}
