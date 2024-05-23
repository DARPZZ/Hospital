using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Drawer
    {
        public string email { get; set; }
        public int id { get; set; }
        public string? ip { get; set; }
        public Drawer()
        {
            
        }
        public Drawer(int id, string email)
        {
            this.id = id;
            this.email = email;
        }



       
 
    }
}
