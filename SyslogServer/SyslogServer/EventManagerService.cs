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
            Event e = new Event("Criticallity", DateTime.Now, "Source", message, SecurityManager.State.CLOSED);
            int id = getID();

            //audit.Log(e, id);

            FileWriter.WriteToFile(e, id);

            Console.WriteLine("Event je logovan.");
        }

        public int getID()
        {
            return ++i;
        }

        public void Test()
        {
            Console.WriteLine("Komunikacija uspesna.");
        }
    }
}
