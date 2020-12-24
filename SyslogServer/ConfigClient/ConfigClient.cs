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

        public void AllowedConfiguration()
        {
            try
            {
                factory.AllowedConfiguration();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }
        public bool addPP(string pp)
        {
            bool uspesno = false;
            try
            {

                if(factory.addPP(pp))
                {
                    uspesno = true;
                }
                else
                {
                    uspesno = false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return uspesno;
        }
        public bool modifyPP(string pp,string zamjenskiPP)
        {
            bool uspesno = false;
            try
            {

                if(factory.modifyPP(pp,zamjenskiPP))
                {
                    uspesno = true;
                }
                else
                {
                    uspesno = false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return uspesno;
        }
    }
}
