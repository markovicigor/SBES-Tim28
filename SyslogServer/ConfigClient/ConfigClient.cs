using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConfigClient
{
   public  class ConfigClient : ChannelFactory<IConfigClient>, IConfigClient, IDisposable
    {
        IConfigClient factory;

        public ConfigClient(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {
            factory = this.CreateChannel();
        }

        public void ChangeConfiguration()
        {
            try
            {
                factory.ChangeConfiguration();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }
    }
}
