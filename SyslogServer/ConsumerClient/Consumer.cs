using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerClient
{
    public class Consumer : ChannelFactory<IConsumer>, IConsumer, IDisposable
    {
        IConsumer factory;

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
                
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return uspesno;
        }

    public string Read()
        {
            try
            {
                return factory.Read();
            }
            catch (Exception e)
            {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return uspesno;
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }
    }
}
