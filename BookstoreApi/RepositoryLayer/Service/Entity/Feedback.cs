using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Service.Entity
{
    public class Feedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string feedbackID { get; set; }

        public string Comment { get; set; }
        public decimal Rating { get; set; }


        [ForeignKey("Register")]
        public string userId { get; set; }
        public virtual Register register { get; set; }

        [ForeignKey("Book")]
        public string BookId { get; set; }
        public virtual Book book { get; set; }
    }
}
