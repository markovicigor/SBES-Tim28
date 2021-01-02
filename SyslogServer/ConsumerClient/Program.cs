using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ConsumerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9000/ConsumerService";
            Consumer client = new Consumer(binding, new EndpointAddress(new Uri(address)));

            while (true)
            {
                int izbor;

                Console.WriteLine("Unesite vas izbor:");
                Console.WriteLine("1.Read:");
                Console.WriteLine("2.Modify: ");
                Console.WriteLine("3.Delete.");
                Console.WriteLine("4.Exit.");
                izbor = Convert.ToInt32(Console.ReadLine());

                switch (izbor)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine(client.Read());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case 2:
                        try
                        {
                            Console.WriteLine("Unesite ID dogadjaja koji zelite da izmenite: ");
                            int id = -1;
                            Int32.TryParse(Console.ReadLine(), out id);

                            Console.WriteLine("Unesite novu poruku: ");
                            string newMsg = Console.ReadLine();

                            client.Update(id, newMsg);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case 3:
                        try
                        {
                            Console.WriteLine("Unesite ID dogadjaja koji zelite da obrisete: ");
                            int id = -1;
                            Int32.TryParse(Console.ReadLine(), out id);

                            client.Delete(id);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case 4:
                        break;
                }
                if (izbor == 4)
                {
                    break;
                }
                Console.WriteLine("\n");
            }
            Console.ReadLine();
        }
    }
}

