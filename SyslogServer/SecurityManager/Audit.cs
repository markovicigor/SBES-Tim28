using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class Audit : IDisposable
    {
        EventLog log = null;

        public Audit()
        {
            try
            {
                if (!EventLog.SourceExists("SBES_Projekat"))
                {
                    EventLog.CreateEventSource("SBES_Projekat", "Application");
                }

                log = new EventLog("Application", Environment.MachineName, "SBES_Projekat");
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR = {0}",e.Message);
            }
        }

        public void Log(Event e,int id)
        {
            log.WriteEntry(e.message, EventLogEntryType.Information, id,0);
        }

        public void Dispose()
        {
            if (log != null)
            {
                log.Dispose();
                log = null;
            }


        }
    }
}
