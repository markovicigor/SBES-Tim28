using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
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
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }
        public void establishCommunication()
        {
           try
            {
                factory.establishCommunication();

            }
            catch(Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }


        public bool CheckConfiguration(string pp)
        {
            bool rez = false;
            try
            {
                if(factory.CheckConfiguration(pp))
                 {
                   rez = true;
                   
                  }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                

            }


            return rez;
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
