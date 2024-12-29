using System;
using MongoDB.Bson;
using MongoDB.Driver; 

namespace MongoDBDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient( connectionString );

            // Connect to database and collection
            var database = client.GetDatabase("YourDatabaseName");

            var studentCollection = database.GetCollection<Student>("Students");

            // create 5 students
            var students = new List<Student>
            {
                new Student("Minh", "minhvip@gmail.com", "0933333")
                {
                    Subjects = new List<string> { "Math", "Physics", "Chemistry" }
                },
                new Student("Tuan", "tuan@gmail.com", "0911444")
                {
                    Subjects = new List<string> { "English", "History" }
                },
                new Student("Lan", "lan123@gmail.com", "0912121")
                {
                    Subjects = new List<string> { "Biology", "Chemistry", "Math" }
                },
                new Student("Hoa", "hoa456@gmail.com", "0945454")
                {
                    Subjects = new List<string> { "Physics", "Computer Science" }
                },
                new Student("Duc", "duc789@gmail.com", "0987878")
                {
                    Subjects = new List<string> { "English", "Math", "Art" }
                }
            };

            Console.WriteLine("----Added into MongoDB----");
            // Thêm vào MongoDB
            //studentCollection.InsertMany(students);
            //Console.WriteLine("All Students added");


            Console.WriteLine("--------Read data from Database---------------");
            
            // Read data
            var allStudents = studentCollection.Find(student => true ).ToList();

            Console.WriteLine("All Students: ");
            foreach ( var student in allStudents ) {
                student.DisplayInfor(); 
            }

            Console.WriteLine("---------Search name in database-----------------");
            
            // Search Student for Name 
            var studentName = "Duc"; 
            var findStudent = studentCollection.Find( s => s.Name == studentName).FirstOrDefault();

            if (findStudent != null)
            {
                findStudent.DisplayInfor();
            }
            else { 
                Console.WriteLine("This student not found"); 
            }



            // Update data 
            Console.WriteLine("---------Update data in database-----------------");

            // Find student need to update
            var filterUpdateSubject = Builders<Student>.Filter.Eq( s => s.Name , "Tuan" );

            // Update any Subject // change 1 môn học luôn 
            var updateSpecificSubject = Builders<Student>.Update.Set(s => s.Subjects[0], "Sleeping");

            studentCollection.UpdateOne( filterUpdateSubject, updateSpecificSubject);
            
            Console.WriteLine("Subject changed successfully ");


            // Update any Subject   // update này là add
            //var updateSubject = Builders<Student>.Update.AddToSet(s => s.Subjects, "Literature" );

            //studentCollection.UpdateOne(filter , updateSubject );

            //Console.WriteLine("Subjects added to Minh's subject ");

            Console.WriteLine("---------Update Phone Number in database -----------------");
           
            // Update Phone Number 
            var filterUpdate = Builders<Student>.Filter.Eq(s => s.Name, "Minh");

            var updatePhone = Builders<Student>.Update.Set(s => s.PhoneNumber, "01111111");

            studentCollection.UpdateOne(filterUpdate, updatePhone);

            Console.WriteLine("Phone number updated for Minh");


            // Delete data 
            Console.WriteLine("---------Delete data in database-----------------");

            // Delete theo Name 
            var deleteFilter = Builders<Student>.Filter.Eq(s => s.Name, "nguyen");

            studentCollection.DeleteOne(deleteFilter);
            Console.WriteLine("Student Minh Deleted");















            







        }
    }
}
