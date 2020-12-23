using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;
using SecurityManager;

namespace SyslogClient
{
   public class EventManager : ChannelFactory<IEventManager>, IEventManager, IDisposable
   {
        IEventManager factory;

        public EventManager(NetTcpBinding binding,EndpointAddress address) : base(binding,address)
        {
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);
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
