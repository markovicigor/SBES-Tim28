using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SyslogServer
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/WhitelistFirewallService";

            ServiceHost host = new ServiceHost(typeof(WhitelistFirewallService));
            host.AddServiceEndpoint(typeof(IWhitelistFirewall), binding, address);
            host.Open();

            string address2 = "net.tcp://localhost:9002/EventManagerService";

            NetTcpBinding binding2 = new NetTcpBinding();

            ServiceHost host2 = new ServiceHost(typeof(EventManagerService));
            host2.AddServiceEndpoint(typeof(IEventManager), binding2, address2);
            host2.Open();

            Console.WriteLine("Server je otvoren..");
            Console.ReadLine();
        }
    }
}
