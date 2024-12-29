using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDemo
{
    internal class Student
    {
        [BsonId] // Đánh dấu thuộc tính này là Id trong MongoDB
        [BsonRepresentation(BsonType.ObjectId)] 


        public String Id { get; set; } 
        public String Name { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }

        public List<String> Subjects { get; set; }

        public Student(  String name, String email, String phoneNumber) { 

            Name = name ; 
            Email = email;
            PhoneNumber = phoneNumber;

            Subjects = new List<string>() ;
        }

        // Add subject 
        public void AddSubject(string subject) { 
            Subjects.Add(subject);
        }

        public void DisplayInfor() { 
            Console.WriteLine($"Id: {Id} ,Name: {Name} ,Email: {Email} ,Phone: {PhoneNumber} ");
            Console.WriteLine($"Subjects: ");

            foreach ( var subject in Subjects ) { 
                Console.Write( $"{subject} " );
            }
            Console.WriteLine("\n");


        }



    }
}
