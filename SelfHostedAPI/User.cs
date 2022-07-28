using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostedAPI
{
    public class User
    {
        public User()
        {
        }

        public User(int v1, string v2, string v3, string v4, string v5)
        {
            this.id = v1;
            this.firstName = v2;
            this.lastName = v3;
            this.emailAddress = v4;
            this.notes = v5;
            DateTime dateTime = DateTime.Now;
            this.creationTime = dateTime.ToString("MM/dd/yyyy hh:mm tt");
        }
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string notes { get; set; }
        public string creationTime { get; set; }
    }
}
