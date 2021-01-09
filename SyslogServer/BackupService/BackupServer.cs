using Common;
using SecurityManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BackupService
{
    public class BackupServer : IBackupServer
    {
        
        public string BackupLog(Dictionary<int, Event> dictionary)
        {
            string path = @"..\..\..\BackupService\backupServer.txt";
            TextWriter tw = new StreamWriter(path, false);

            string podaci = "";
            foreach(Event e in dictionary.Values)
            {
            podaci += e.id + ";" + e.criticality + ";" + e.timestamp + ";" + e.source + ";" + e.message + ";" + e.eState + "\n";
             
            tw.WriteLine(e.id + ";" + e.criticality + ";" + e.timestamp + ";" + e.source + ";" + e.message + ";" + e.eState);

            }

            tw.Close();

            return podaci;
        }

        public void SendMessage(string message, byte[] sign)
        {
            
            //kad je u pitanju autentifikacija putem Sertifikata
            string clienName = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

            string clientNameSign = "servis_sign";
           
            X509Certificate2 certificate = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople,
                StoreLocation.LocalMachine, clientNameSign);

            /// Verify signature using SHA1 hash algorithm
            if (DigitalSignature.Verify(message, HashAlgorithm.SHA1, sign, certificate))
            {
                Console.WriteLine("Sign is valid");
            }
            else
            {
                Console.WriteLine("Sign is invalid");
            }
        }

        public void TestCommunication()
        {
            Console.WriteLine("Komunikacija je uspostavljena sa Backup serverom ");
        }
    }
}
