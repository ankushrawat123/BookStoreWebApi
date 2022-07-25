using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service.Entity
{
    public class OrderPostModel
    {
        public string FullName { get; set; }

        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }

        public string BookTitle { get; set; }
        public string Author { get; set; }


        public string addressTypeId { get; set; }
        public string Addresses { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Pincode { get; set; }


        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
