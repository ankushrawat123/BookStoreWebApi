using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Service.Entity
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string orderId { get; set; }

        [ForeignKey("Book")]
        public string BookId { get; set; }
        public virtual Book book { get; set; }

        [ForeignKey("Register")]
        public string UserId { get; set; }
        public virtual Register register { get; set; }

        [ForeignKey("Address")]
        public string AddressId { get; set; }    
        public virtual Address address { get; set; }

        public int Quantity { get; set; }
        public int Price { get; set; }

    }
}
