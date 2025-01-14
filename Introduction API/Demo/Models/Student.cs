using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Demo.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }

        public List<String> Subjects { get; set; }
    }
}
