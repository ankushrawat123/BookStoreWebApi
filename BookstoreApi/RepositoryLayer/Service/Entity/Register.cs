using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service.Entity
{
    public class Register
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string UserId { get; set; }
        public string FullName { get; set; }    

        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }

    }
}
