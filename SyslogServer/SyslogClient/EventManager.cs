using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SyslogClient
{
   public class EventManager : ChannelFactory<IEventManager>, IEventManager, IDisposable
   {
        IEventManager factory;

        public EventManager(NetTcpBinding binding,EndpointAddress address) : base(binding,address)
        {
            factory = this.CreateChannel();

        }

        public void EventLog(string message)
        {
            try
            {
                factory.EventLog(message);
                Console.WriteLine("Event je logovan na serveru");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Test()
        {
            try
            {
                factory.Test();
                Console.WriteLine("Komunikacija sa SysLog Serverom uspesno ostvarena.");
            }catch(Exception e)
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
