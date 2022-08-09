using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlowardApp.Services.CatalogService.Models
{
    /// <summary>
    /// Product table
    /// </summary>
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Cost { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Image { get; set; }
    }
}
