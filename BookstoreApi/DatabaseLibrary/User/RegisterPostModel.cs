using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.User
{
    public class RegisterPostModel
    {
     
        public string FullName { get; set; }

        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }
    }
}
