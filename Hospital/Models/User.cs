using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class User
    {
        public string email { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public User()
        {
            
        }

        public User(string email, string password, string firstName, string lastName)
        {
            this.email = email;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
