using SecurityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IBackupServer
    {
        [OperationContract]
        void TestCommunication();

        [OperationContract]
        string BackupLog(Dictionary<int, Event> dic);

        [OperationContract]
        void SendMessage(string message, byte[] sign);



    }
}
