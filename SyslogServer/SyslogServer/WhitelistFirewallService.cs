using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslogServer
{
    public class WhitelistFirewallService : IWhitelistFirewall
    {
        public bool CheckConfiguration(string pp)
        {

            bool dozvoljen = false;
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\SyslogClient\bin\debug\WhiteListFireWall.txt");

            List<string> listaSvihPP = new List<string>();

            foreach (string s in lines)
            {
                listaSvihPP.Add(s);
            }

            if (listaSvihPP.Contains(pp))
            {
                dozvoljen = true;

            }
            else
            {
                dozvoljen = false;

            }
            return dozvoljen;
        }

        public void establishCommunication()
        {
            Console.WriteLine("Uspesna komunikacija... ");
        }
    }
}
