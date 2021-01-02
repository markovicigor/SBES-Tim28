using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerClient
{
    public class Consumer : ChannelFactory<IConsumer>, IConsumer, IDisposable
    {
        IConsumer factory;

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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public string Read()
        {
            try
            {
                return factory.Read();
            }
            catch (Exception e)
            {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
