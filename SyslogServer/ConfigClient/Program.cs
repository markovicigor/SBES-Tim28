using SyslogClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

           

            ConfigClient proxy = new ConfigClient(binding, new EndpointAddress(new Uri(address)));
            proxy.AllowedConfiguration();

            while (true)
            {

               
                int izbor;

                Console.WriteLine("Unesite vas izbor:");
                Console.WriteLine("1.Dodajte protokol ili port:");
                Console.WriteLine("2.Izmenite protokol ili port: ");
                Console.WriteLine("3.Izadjite.");
                izbor = Convert.ToInt32(Console.ReadLine());


                string pp = "";
                string zamjenskiPP = "";
                switch (izbor)
                {
                    case 1:

                        Console.WriteLine("Unesite port ili protokol koji zelite da dodate:");
                        pp = Console.ReadLine();
                       
                        if(proxy.addPP(pp))
                        {
                            Console.WriteLine("Konfiguracija je uspesno izmenjena ");
                        }
                        else
                        {
                            Console.WriteLine("Protokol/port nije dozvoljen");
                        }

                        break;
                    case 2:

                        Console.WriteLine("Unesite protokol/port koji zelite da izmenite:");
                        pp = Console.ReadLine();
                        Console.WriteLine("Unesite protokol/port kojim zelite da izmenite postojeci:");
                        zamjenskiPP = Console.ReadLine();
                        if(proxy.modifyPP(pp, zamjenskiPP))
                        {
                            Console.WriteLine("Konfiguracija je uspesno izmenjena ");
                        }
                        else
                        {
                            Console.WriteLine("Protokol/port nije dozvoljen");
                        }
                        break;
                    case 3:
                        break;

                }
                if(izbor == 3)
                {
                    break;
                }
                Console.WriteLine("\n");
            }
            Console.ReadLine();

        }
    }
}
