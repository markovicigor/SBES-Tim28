using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslogClient
{
    public class ConfigService : IConfigClient
    {

        public void AllowedConfiguration()
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



           // Program.proxyLog.EventLog("Izmenjena konfiguracija.");
        }

        public bool addPP(string pp)
        {
           
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\SyslogClient\bin\debug\WhiteListFireWall.txt");
            bool dozvoljen = false;

            try
            {
                 if(Program.proxy.CheckConfiguration(pp))
                  {
                     lines.ToList().Add(pp);
                     dozvoljen = true;
                    Console.WriteLine("Protokol/port je dozvoljen za dodavanje" );
                    Program.proxyLog.EventLog("Izmenjena konfiguracija.");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message );
            }

            return dozvoljen;
        }

        public bool modifyPP(string pp,string zamjenskiPP)
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\SyslogClient\bin\debug\WhiteListFireWall.txt");

            bool dozvoljen = false;
            if (Program.proxy.CheckConfiguration(pp) && Program.proxy.CheckConfiguration(zamjenskiPP))
            {
                pp = zamjenskiPP;
                lines.ToList().Add(pp);
                Console.WriteLine("Protokol/port je dozvoljen za izmjenu");
                Program.proxyLog.EventLog("Izmenjena konfiguracija.");
                dozvoljen = true;
            }

            return dozvoljen;


        }


    }

}
