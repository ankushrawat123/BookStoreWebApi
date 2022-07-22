using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service.Entity
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BookId { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public decimal Rating { get; set; }
        public int totalRating { get; set; }
        public int DiscountPrice { get; set; }
        public int ActualPrice { get; set; }
        public string BookImage { get; set; }
        public int BookQuantity { get; set; }
    }
}
