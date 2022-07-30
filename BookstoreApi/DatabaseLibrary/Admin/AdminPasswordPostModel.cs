using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Admin
{
    public class AdminPasswordPostModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
