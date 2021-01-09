﻿using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
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

        public void Delete(int id)
        {
            CustomPrincipal cp = new CustomPrincipal(Thread.CurrentPrincipal.Identity as WindowsIdentity);
            Dictionary<int, Event> dogadjaji = FileWriter.readFromFile();
            if (cp.IsInRole("Delete"))
            {
                if (!dogadjaji.ContainsKey(id))
                {
                    throw new Exception("Nepostojeci ID.");
                }

               dogadjaji.Remove(id);
                FileWriter.OverWriteEvent(dogadjaji);
            }
            else
            {
                throw new SecurityException("Access is denied.");
            }
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

        public void Update(int id, string newMsg)
        {
            CustomPrincipal cp = new CustomPrincipal(Thread.CurrentPrincipal.Identity as WindowsIdentity);
            Dictionary<int, Event> dogadjaji = FileWriter.readFromFile();
            if (cp.IsInRole("Update"))
            {
                if (!dogadjaji.ContainsKey(id))
                {
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
        }
    }
}
