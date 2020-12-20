using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslogClient
{
    public class ConfigService : IConfigClient
    {
        public void ChangeConfiguration()
        {
            List<string> pp = new List<string>();
            string path = @"..\..\..\SyslogClient\bin\debug\WhiteListFireWall.txt";

            pp.Add("5100");
            pp.Add("5000");
            pp.Add("5050");
            pp.Add("5008");
            pp.Add("6000");
            pp.Add("tcp");
            pp.Add("http");
            pp.Add("udp");


            string upis = "";

            foreach (string s in pp)
            {
                upis += s + Environment.NewLine;
            }

            File.WriteAllText(path, upis);

            // Program.proxy.LogEvent("Konfiguracija je izmjenjena");

            Program.proxyLog.EventLog("Izmenjena konfiguracija.");
        }
    }

}
