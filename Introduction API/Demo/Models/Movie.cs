using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Demo.Models
{
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<String> Films { get; set; }

    }
}
