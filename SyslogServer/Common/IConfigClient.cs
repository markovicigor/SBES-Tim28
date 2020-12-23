using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IConfigClient
    {
        [OperationContract]
        void AllowedConfiguration();

        [OperationContract]
        bool addPP(string pp);

        [OperationContract]
        bool modifyPP(string pp, string zamjenskiPP);
    }
}
