using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyslogServer
{
    public class WhitelistFirewallService : IWhitelistFirewall
    {
        public string CheckConfiguration(int port, string protocol)
        {
            throw new NotImplementedException();
        }

        public void establishCommunication()
        {
            Console.WriteLine("Uspesna komunikacija... ");
        }
    }
}
