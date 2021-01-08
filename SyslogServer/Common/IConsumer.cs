using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
<<<<<<< Updated upstream
    public interface IConsumer
    {
=======
   public  interface IConsumer
    {

>>>>>>> Stashed changes
        [OperationContract]
        string Read();

        [OperationContract]
<<<<<<< Updated upstream
        void Update(int id, string newMsg);

        [OperationContract]
        void Delete(int id);
=======
        bool Update(int id, string newMsg);

        [OperationContract]
        bool Delete(int id);
>>>>>>> Stashed changes
    }
}
