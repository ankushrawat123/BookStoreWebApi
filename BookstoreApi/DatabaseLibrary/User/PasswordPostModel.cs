using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.User
{
    public class PasswordPostModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string EmailId { get; set; }
        public string Password { get; set; }    
        public string ConfirmPassword { get; set; }
    }
}
