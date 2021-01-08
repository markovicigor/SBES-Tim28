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
using System.Diagnostics;

namespace SyslogClient
{
    class Program
    {

        public static EventManager proxyLog;
        public static WhitelistFirewall proxy;
        static void Main(string[] args)
        {
            

            string serverCertCN = "servis";

            #region WhiteList
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, serverCertCN);
            EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/WhitelistFirewallService")
                                      ,new X509CertificateEndpointIdentity(srvCert));

            proxy = new WhitelistFirewall(binding, address);

             proxy.establishCommunication();



            Console.WriteLine($"Klijent WhiteList firewall je uspesno ostvario komunikaciju na adresi: {address}");
             #endregion
            //Console.ReadLine();

            #region ConfigService
            NetTcpBinding binding2 = new NetTcpBinding();
            string address2 = "net.tcp://localhost:9004/ConfigService";
            ServiceHost host = new ServiceHost(typeof(ConfigService));
            host.AddServiceEndpoint(typeof(IConfigClient), binding2, address2);

            ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();

            // if not found - add behavior with setting turned on
            if (debug == null)
            {
                host.Description.Behaviors.Add(
                     new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {
                // make sure setting is turned ON
                if (!debug.IncludeExceptionDetailInFaults)
                {
                    debug.IncludeExceptionDetailInFaults = true;
                }
            }



            host.Open();
            #endregion


            #region Logging
            NetTcpBinding bindingLog = new NetTcpBinding();
            bindingLog.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            EndpointAddress addressLog = new EndpointAddress(new Uri("net.tcp://localhost:9002/EventManagerService")
                                      ,new X509CertificateEndpointIdentity(srvCert));
            Console.WriteLine($"Klijent Event Manager je uspesno ostvario komunikaciju na adresi: {addressLog}");
            proxyLog = new EventManager(binding, addressLog);
            proxyLog.Test();
            #endregion

            Console.ReadLine();
        }
    }
}
