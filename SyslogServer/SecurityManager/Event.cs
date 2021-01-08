using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public enum State
    {
        OPEN,
        CLOSED
    }

    [DataContract]
    public class Event
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string criticality { get; set; }
        [DataMember]
        public DateTime timestamp { get; set; }
        [DataMember]
        public string source { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public State eState { get; set; }

<<<<<<< Updated upstream
        public Event(int id, string c, DateTime t, string s, string m, State es)
=======
        public Event(int id,string c, DateTime t, string s, string m, State es)
>>>>>>> Stashed changes
        {
            this.id = id;
            this.criticality = c;
            this.timestamp = t;
            this.source = s;
            this.message = m;
            this.eState = es;
        }

        public override string ToString()
        {
<<<<<<< Updated upstream
            string ret = "Criticality = " + criticality + "\n" + "Timestamp = " + timestamp.ToString() + "\n" + "Source = " + source + "\n" + "Message = " + message + "\n" + "State = " + eState.ToString() + "\n";
=======
            string ret = "Criticality = " +   criticality + "\n" + "Timestamp = " + timestamp.ToString() + "\n" + "Source = " + source + "\n" + "Message = " + message + "\n" + "State = " + eState.ToString() + "\n";
>>>>>>> Stashed changes

            return ret;
        }


    }
}
