using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poster
{
    public class UserStatistics
    {
        public string NameOfNode { get; set; }
        public string DateTimeOfLastStatistics { get; set; }
        public string VersionOfClient { get; set; }
        public string TypeOfDevice { get; set; }

        public UserStatistics(string nameOfNode, DateTime dateTime, string versionOfClient, string typeOfDevice)
        {
            this.NameOfNode = nameOfNode;
            this.DateTimeOfLastStatistics = dateTime.ToString();
            this.VersionOfClient = versionOfClient;
            this.TypeOfDevice = typeOfDevice;
        }

        public UserStatistics(string nameOfNode, string versionOfClient, string typeOfDevice) : 
            this(nameOfNode, DateTime.Now, versionOfClient, typeOfDevice) { }

        public UserStatistics() { }
    }
}
