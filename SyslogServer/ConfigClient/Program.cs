using SyslogClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConfigClient
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9004/ConfigService";

            using (ConfigClient proxy = new ConfigClient(binding, new EndpointAddress(new Uri(address))))
            {
                proxy.ChangeConfiguration();
            }
            Console.WriteLine("Konfiguracija izmenjena.");
            Console.ReadLine();

        }
    }
}
