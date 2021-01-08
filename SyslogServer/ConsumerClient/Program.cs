<<<<<<< Updated upstream
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
=======
﻿using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
=======


>>>>>>> Stashed changes
                int izbor;

                Console.WriteLine("Unesite vas izbor:");
                Console.WriteLine("1.Read:");
                Console.WriteLine("2.Modify: ");
                Console.WriteLine("3.Delete.");
                Console.WriteLine("4.Exit.");
                izbor = Convert.ToInt32(Console.ReadLine());

<<<<<<< Updated upstream
=======
                Dictionary<int, Event> dogadjaji = FileWriter.readFromFile();

>>>>>>> Stashed changes
                switch (izbor)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine(client.Read());
                        }
<<<<<<< Updated upstream
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
=======
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case 2:
                       
                        Console.WriteLine("Unesite ID dogadjaja koji zelite da izmenite: ");
                        int id;
                        id = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Unesite novu poruku: ");
                            string newMsg = Console.ReadLine();
                        if(!dogadjaji.ContainsKey(id))
                        {
                            Console.WriteLine("Nepostojeci id" );
                        }
                        else
                        {
                            client.Update(id, newMsg);
                        }
                        
                        
                       
                        break;
                    case 3:
                        
                            Console.WriteLine("Unesite ID dogadjaja koji zelite da obrisete: ");
                        int id2;
                        id2 = Convert.ToInt32(Console.ReadLine());
                        if (!dogadjaji.ContainsKey(id2))
                        {
                            Console.WriteLine("Nepostojeci id");
                        }
                        else
                        {
                           client.Delete(id2);
                        }
                        
                        break;
                    case 4:
                        break;

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
