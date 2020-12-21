using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace SyslogServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            #region WhiteListHost
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            string address = "net.tcp://localhost:9999/WhitelistFirewallService";

            ServiceHost host = new ServiceHost(typeof(WhitelistFirewallService));
            host.AddServiceEndpoint(typeof(IWhitelistFirewall), binding, address);

            ///Custom validation mode enables creation of a custom validator - CustomCertificateValidator
			host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServisCertValidator();

            ///If CA doesn't have a CRL associated, WCF blocks every client because it cannot be validated
            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            ///Set appropriate service's certificate on the host. Use CertManager class to obtain the certificate based on the "srvCertCN"
			host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

            host.Open();
            #endregion

            #region EventMngrHost
            string address2 = "net.tcp://localhost:9002/EventManagerService";

            NetTcpBinding binding2 = new NetTcpBinding();
            binding2.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            ServiceHost host2 = new ServiceHost(typeof(EventManagerService));
            host2.AddServiceEndpoint(typeof(IEventManager), binding2, address2);

            ///Custom validation mode enables creation of a custom validator - CustomCertificateValidator
            host2.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host2.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServisEventCertValidator();

            ///If CA doesn't have a CRL associated, WCF blocks every client because it cannot be validated
            host2.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            ///Set appropriate service's certificate on the host. Use CertManager class to obtain the certificate based on the "srvCertCN"
			host2.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

            host2.Open();
            #endregion

            Console.WriteLine("Server je otvoren..");
            Console.ReadLine();
        }
    }
}
