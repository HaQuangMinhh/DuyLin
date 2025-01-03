using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Demo2
{
    internal class Car
    {
        [BsonId] // Đánh dấu thuộc tính này là Id trong MongoDB
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }

        public List<String> Parts { get; set; } // Phụ tùng

        public void AddParts(string part) { 
            Parts.Add(part);    
        }

        public void DisplayInfor() {
            
            Console.WriteLine( $"Id: {Id} ,Brand: {Brand} ,Model: {Model} ,Year: {Year} ,Price: {Price}" );
            Console.WriteLine( $"Parts: " + string.Join(", " , Parts) );
            Console.WriteLine("---------------------------------------------------");
        }


    }
}
