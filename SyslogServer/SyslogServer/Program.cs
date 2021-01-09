using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace SyslogServer
{
    public class Program
    {
       
        public static Dictionary<int, Event> dogadjaji = new Dictionary<int, Event>();
        static void Main(string[] args)
        {
            string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            
            #region WhiteListHost
            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            string address = "net.tcp://localhost:9999/WhitelistFirewallService";
            ServiceHost host = new ServiceHost(typeof(WhitelistFirewallService));

            host.AddServiceEndpoint(typeof(IWhitelistFirewall), binding, address);
            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServisCertValidator();
            host.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
			host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);

            #region Debagovanje prvog hosta
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
            #endregion
            try
            {
                host.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska na WhiteListFireWall {e.Message}");
            }


            #endregion

            #region EventMngrHost
            string address2 = "net.tcp://localhost:9002/EventManagerService";

            NetTcpBinding binding2 = new NetTcpBinding();
            binding2.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            ServiceHost host2 = new ServiceHost(typeof(EventManagerService));
            host2.AddServiceEndpoint(typeof(IEventManager), binding2, address2);
            host2.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host2.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServisEventCertValidator();
            host2.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
            host2.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, srvCertCN);
            #region DebugEventManager
            ServiceDebugBehavior debug2 = host2.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (debug2 == null)
            {
                host2.Description.Behaviors.Add(
                     new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {
                // make sure setting is turned ON
                if (!debug2.IncludeExceptionDetailInFaults)
                {
                    debug2.IncludeExceptionDetailInFaults = true;
                }
            }
            #endregion
            try
            {
                host2.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Greska na Eventmanageru {e.Message}");
            }
           
            #endregion

            #region ConsumerService
            NetTcpBinding bindingCS = new NetTcpBinding();
            string addressCS = "net.tcp://localhost:9000/ConsumerService";

            ServiceHost hostCS = new ServiceHost(typeof(ConsumerService));
            hostCS.AddServiceEndpoint(typeof(IConsumer), bindingCS, addressCS);

            hostCS.Authorization.ServiceAuthorizationManager = new CustomAuthorizationManager();

            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            policies.Add(new CustomAuthorizationPolicy());
            hostCS.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();

            #region Debbugovanje consumer hosta
            if (debug == null)
            {
                hostCS.Description.Behaviors.Add(
                     new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {

                if (!debug.IncludeExceptionDetailInFaults)
                {
                    debug.IncludeExceptionDetailInFaults = true;
                }
            }
            #endregion
            try
            {
                hostCS.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error consumer  - " + e.Message);
            }

            #endregion
            #region BackupServer
            Console.WriteLine("Server je otvoren..");
            NetTcpBinding binding3 = new NetTcpBinding();
            binding3.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            /// Use CertManager class to obtain the certificate based on the "srvCertCN" representing the expected service identity.
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, "backupserver");
            EndpointAddress address4 = new EndpointAddress(new Uri("net.tcp://localhost:9003/BackupServer"), new X509CertificateEndpointIdentity(srvCert));
            
            BackupServerService proxy = new BackupServerService(binding3, address4);
            

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(30);
           
            
            string signCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name) + "_sign";
            X509Certificate2 certificateSign = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, signCertCN);
            dogadjaji = FileWriter.readFromFile();
            string message = proxy.BackupLog(dogadjaji);
            byte[] signature = DigitalSignature.Create(message, HashAlgorithm.SHA1, certificateSign);
            //Console.WriteLine("SendMessage() using {0} certificate finished.", signCertCN);

            proxy.SendMessage(message, signature);
          
            var timer = new System.Threading.Timer((e) =>
            {
                dogadjaji = FileWriter.readFromFile();
                message = proxy.BackupLog(dogadjaji);
                signature = DigitalSignature.Create(message, HashAlgorithm.SHA1, certificateSign);
                proxy.SendMessage(message, signature);
                //Console.WriteLine("SendMessage() using {0} certificate finished.", signCertCN);

            }, null, startTimeSpan, periodTimeSpan);
            proxy.TestCommunication();

            



            #endregion


            Console.ReadLine();
            host.Close();
            host2.Close();
            hostCS.Close();
        }
    }
}
