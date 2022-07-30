using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Admin
{
    public class AdminPostModel
    {
        public string FullName { get; set; }

        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }
    }
}
