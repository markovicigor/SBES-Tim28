using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SyslogClient
{
    public class WhitelistFirewall : ChannelFactory<IWhitelistFirewall>, IWhitelistFirewall, IDisposable
    {
        IWhitelistFirewall factory;


        public WhitelistFirewall(NetTcpBinding binding, EndpointAddress address)
            : base(binding, address)
        {

            factory = this.CreateChannel();
        }
        public void establishCommunication()
        {
           try
            {
                factory.establishCommunication();
                Console.WriteLine("Komunikacija je ostvarena.. ");
            }
            catch(Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        public void Dispose()
        {
            if(factory != null)
            {
                factory = null;
            }
            this.Close();
        }

        public string CheckConfiguration(int port, string protocol)
        {
            string rez = "";

            rez = factory.CheckConfiguration(port, protocol);

            return rez;
        }
    }
}
