using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace SyslogClient
{
    class Program
    {
        public static EventManager proxyLog;
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/WhitelistFirewallService";

            using (WhitelistFirewall proxy = new WhitelistFirewall(binding, new EndpointAddress(new Uri(address))))
            {
                proxy.establishCommunication();
            }


            //Console.ReadLine();

            #region ConfigService
            NetTcpBinding binding2 = new NetTcpBinding();
            string address2 = "net.tcp://localhost:9004/ConfigService";



            ServiceHost host = new ServiceHost(typeof(ConfigService));
            host.AddServiceEndpoint(typeof(IConfigClient), binding2, address2);



            host.Open();

            #endregion

            string addressLog = "net.tcp://localhost:9002/EventManagerService";

            proxyLog = new EventManager(binding, new EndpointAddress(new Uri(addressLog)));
            proxyLog.Test();


            Console.ReadLine();
        }
    }
}
