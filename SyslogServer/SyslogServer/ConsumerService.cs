using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
<<<<<<< Updated upstream
=======
using System.Diagnostics;
>>>>>>> Stashed changes
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyslogServer
{
    public class ConsumerService : IConsumer
    {

<<<<<<< Updated upstream
        public void Delete(int id)
        {
=======
        public bool Delete(int id)
        {
            bool uspesno = false;
>>>>>>> Stashed changes
            CustomPrincipal cp = new CustomPrincipal(Thread.CurrentPrincipal.Identity as WindowsIdentity);
            Dictionary<int, Event> dogadjaji = FileWriter.readFromFile();
            if (cp.IsInRole("Delete"))
            {
                if (!dogadjaji.ContainsKey(id))
                {
                    throw new Exception("Nepostojeci ID.");
                }
<<<<<<< Updated upstream

               dogadjaji.Remove(id);
                FileWriter.OverWriteEvent(dogadjaji);
=======
                else
                {
                    uspesno = true;
                    dogadjaji.Remove(id);
                    FileWriter.OverWriteEvent(dogadjaji);
                }
               
>>>>>>> Stashed changes
            }
            else
            {
                throw new SecurityException("Access is denied.");
            }
<<<<<<< Updated upstream
=======
            return uspesno;
>>>>>>> Stashed changes
        }

        public string Read()
        {
            string ret = "";

            CustomPrincipal cp = new CustomPrincipal(Thread.CurrentPrincipal.Identity as WindowsIdentity);
            Dictionary<int, Event> dogadjaji = FileWriter.readFromFile();
            ret += "Lista dogadjaja:\n";

            foreach(Event e in dogadjaji.Values)
            {
                ret +=$"ID[{e.id.ToString()}]" +"\n" + e.ToString() + "\n";
            }

            return ret;
        }

<<<<<<< Updated upstream
        public void Update(int id, string newMsg)
        {
            CustomPrincipal cp = new CustomPrincipal(Thread.CurrentPrincipal.Identity as WindowsIdentity);
=======
        public bool  Update(int id, string newMsg)
        {
           
            CustomPrincipal cp = new CustomPrincipal(Thread.CurrentPrincipal.Identity as WindowsIdentity);
            bool uspesno = false;
>>>>>>> Stashed changes
            Dictionary<int, Event> dogadjaji = FileWriter.readFromFile();
            if (cp.IsInRole("Update"))
            {
                if (!dogadjaji.ContainsKey(id))
                {
<<<<<<< Updated upstream
                    throw new Exception("Nepostojeci ID.");
                }

                Event temp = dogadjaji[id];
                temp.message = newMsg;

                dogadjaji[id] = temp;
                FileWriter.OverWriteEvent(dogadjaji);
            }
            else
            {
                throw new SecurityException("Access is denied.");
            }
=======
                   
                    throw new Exception("Nepostojeci ID.");

                }
                else
                {
                    Event temp = dogadjaji[id];
                    temp.message = newMsg;

                    dogadjaji[id] = temp;
                    FileWriter.OverWriteEvent(dogadjaji);
                    uspesno = true;
                }
            }
            else
            {
               
                throw new SecurityException("Access is denied.");

            }
            return uspesno;
>>>>>>> Stashed changes
        }
    }
}
