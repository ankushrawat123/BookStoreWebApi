using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Service.Entity
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AddressId { get; set; }   

        [ForeignKey("Register")]
        public string UserId  { get; set; }
        public virtual Register register { get; set; }

        [ForeignKey("AddressModel")]
        public string addressTypeId { get; set; }
         public string Addresses { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Pincode { get; set; }

    }
}
