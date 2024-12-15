using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordPDC50.Model
{
    public class User
    {
        public int ID { get; set; }
        public string Idno { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Contactno { get; set; }
        public string Password { get; set; }

        public class LoginResponse
        {
            public string Message { get; set; }  // Message indicating the result (e.g., success, failure)
            public User User { get; set; }       // User data (optional if the login is successful)
        }
    }
}