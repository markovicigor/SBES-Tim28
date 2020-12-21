using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using SecurityManager;

namespace SyslogClient
{
    class Program
    {
        public static EventManager proxyLog;
        static void Main(string[] args)
        {
            string serverCertCN = "servis";

            #region WhiteList
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, serverCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/WhitelistFirewallService"),
                                      new X509CertificateEndpointIdentity(srvCert));

            

            using (WhitelistFirewall proxy = new WhitelistFirewall(binding, address))
            {
                proxy.establishCommunication();
            }

            #endregion
            //Console.ReadLine();

            #region ConfigService
            NetTcpBinding binding2 = new NetTcpBinding();
            string address2 = "net.tcp://localhost:9004/ConfigService";



            ServiceHost host = new ServiceHost(typeof(ConfigService));
            host.AddServiceEndpoint(typeof(IConfigClient), binding2, address2);



            host.Open();

            #endregion

            #region Logging
            NetTcpBinding bindingLog = new NetTcpBinding();
            bindingLog.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

           
            EndpointAddress addressLog = new EndpointAddress(new Uri("net.tcp://localhost:9002/EventManagerService"),
                                      new X509CertificateEndpointIdentity(srvCert));
            

            proxyLog = new EventManager(binding, addressLog);
            proxyLog.Test();
            #endregion

            Console.ReadLine();
        }
    }
}
