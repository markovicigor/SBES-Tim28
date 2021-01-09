using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class ServisEventCertValidator : X509CertificateValidator
    {

        public override void Validate(X509Certificate2 certificate)
        {

           if((DateTime.Now - certificate.NotBefore).TotalDays>30)
           {
                throw new Exception("Certificate is not old enough!");
           }
        }
    }
}
