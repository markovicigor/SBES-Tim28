using Common;
using System;
using System.Collections.Generic;
<<<<<<< Updated upstream
=======
using System.Diagnostics;
>>>>>>> Stashed changes
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerClient
{
    public class Consumer : ChannelFactory<IConsumer>, IConsumer, IDisposable
    {
        IConsumer factory;

<<<<<<< Updated upstream
        public Consumer(NetTcpBinding binding, EndpointAddress address): base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void Delete(int id)
        {
            try
            {
                factory.Delete(id);
                Console.WriteLine("Dogadjaj[{0}] uspesno obrisan.", id);
=======
        public Consumer(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {

            factory = this.CreateChannel();
        }

        public bool Delete(int id)
        {
            bool uspesno = false;
            try
            {
                if(factory.Delete(id))
                {
                    Console.WriteLine("Dogadjaj[{0}] uspjesno obrisan.", id);
                    uspesno = true;
                }
                
                
>>>>>>> Stashed changes
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
<<<<<<< Updated upstream
        }

        public string Read()
=======
            return uspesno;
        }

    public string Read()
>>>>>>> Stashed changes
        {
            try
            {
                return factory.Read();
            }
            catch (Exception e)
            {
<<<<<<< Updated upstream
                Console.WriteLine(e.Message);
            }
            return "";
        }

        public void Update(int id, string newMsg)
        {
            try
            {
                factory.Update(id, newMsg);
                Console.WriteLine("Dogadjaj[{0}] uspesno update-ovan.", id);
=======
                Console.WriteLine( e.Message);
            }

            return "";
        }

        public bool Update(int id, string newMsg)
        {
            
            bool uspesno = false;
            try
            {
                if (factory.Update(id, newMsg))
                {
                    Console.WriteLine("Dogadjaj[{0}] uspjesno update-ovan.", id);
                    uspesno = true;
                }
>>>>>>> Stashed changes
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
<<<<<<< Updated upstream
=======


            return uspesno;
>>>>>>> Stashed changes
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
            this.Close();
        }
    }
}
