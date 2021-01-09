using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SyslogServer
{
    public class BackupServerService : ChannelFactory<IBackupServer>, IBackupServer, IDisposable
    {
        IBackupServer factory;

        public BackupServerService(NetTcpBinding binding, EndpointAddress address)
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
        public string BackupLog(Dictionary<int, Event> dictionary)
        {
            string podaci = "";
            try
            {
                podaci = factory.BackupLog(dictionary);
                Console.WriteLine("BackupServer/BackupLog()");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return podaci;
        }

        public void Dispose()
        {
            if (factory != null)
            {
                factory = null;
            }

            this.Close();
        }

        public void SendMessage(string message, byte[] sign)
        {
            try
            {
                factory.SendMessage(message, sign);
            }
            catch (Exception e)
            {
                Console.WriteLine("[SendMessage] ERROR = {0}", e.Message);
            }
        }

        public void TestCommunication()
        {
            try
            {
                factory.TestCommunication();
                Console.WriteLine("Test komunikacija sa BackupServerom.");
            }
            catch (Exception e)
            {
                Console.WriteLine("GRESKA " +  e.Message );
            }
        }
    }
}
