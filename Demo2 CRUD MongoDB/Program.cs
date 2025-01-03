using MongoDB.Driver;
using System; 

namespace Demo2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string connectionString = "mongodb://localhost:27017"; 
            var client = new MongoClient(connectionString);

            // Connect to database and collection
            var database = client.GetDatabase("YourDatabaseName");

            var carCollection = database.GetCollection<Car>("Cars");

            // create 5 car
            var cars = new List<Car> 
            {
                new Car { Brand = "Toyota", Model = "Camry", Year = 2022, Price = 30000.0, Parts = new List<string> { "Engine", "Tires", "Brakes" } },
                new Car { Brand = "Honda", Model = "Civic", Year = 2021, Price = 25000.0, Parts = new List<string> { "Battery", "Lights", "Windows" } },
                new Car { Brand = "Ford", Model = "Focus", Year = 2020, Price = 22000.0, Parts = new List<string> { "Transmission", "Seats", "Steering Wheel" } },
                new Car { Brand = "Chevrolet", Model = "Malibu", Year = 2023, Price = 28000.0, Parts = new List<string> { "Suspension", "Tires", "Airbags" } },
                new Car { Brand = "Tesla", Model = "Model 3", Year = 2023, Price = 45000.0, Parts = new List<string> { "Battery", "Autopilot System", "Wheels" } }
            };

            Console.WriteLine("----Added into MongoDB----");
            // Thêm vào MongoDB
            //carCollection.InsertMany(cars);
            //Console.WriteLine("5 cars added");

            Console.WriteLine("--------Read data from Database---------------");
            
            // Read data
            var allCar = carCollection.Find(car => true).ToList();

            Console.WriteLine("All Cars: ");
            foreach (var car in allCar)
            {
                car.DisplayInfor();
            }


            Console.WriteLine("-------------Update Car----------------");
            // cập nhật xe dựa trên Brand 
            var filter = Builders<Car>.Filter.Eq(c => c.Brand, "Toyota");
            var update = Builders<Car>.Update.Set(c => c.Price, 35000.0);
            carCollection.UpdateOne(filter, update);
            
            Console.WriteLine("Updated Toyota's Price to 35000");


            Console.WriteLine("-------------Delete Car----------------");
            // Xóa xe có Model là "Civic"
            var deleteFilter = Builders<Car>.Filter.Eq(c => c.Model, "Civic");
            carCollection.DeleteOne(deleteFilter);

            Console.WriteLine("Deleted Car with Model Civic");

            Console.WriteLine("--------------Update vào danh sách List : Phụ Tùng ( Parts ) ");
            Console.WriteLine("Method 1: Dùng Add to Set ");

            var updatefilter = Builders<Car>.Filter.Eq(c => c.Brand, "Toyota");
            var updateParts = Builders<Car>.Update.AddToSet(c => c.Parts, "GPS System");
            carCollection.UpdateOne(filter, updateParts);

            Console.WriteLine("GPS System added to Toyota's parts.");

            Console.WriteLine("Method 2: Thêm phụ tùng mới vào danh sách `Parts` cho mỗi xe ");
            
            // Thêm phụ tùng mới vào danh sách `Parts` cho mỗi xe
            //var carsWithUpdatedParts = carCollection.Find(car => true).ToList();

            // Thêm phụ tùng vào từng xe
            //foreach (var car in carsWithUpdatedParts)
            //{
            //    car.Parts.Add("Banh xe 2");
            //    // Cập nhật lại trong MongoDB
            //    var updateParts2 = Builders<Car>.Update.Set(c => c.Parts, car.Parts);
            //    var filterCar = Builders<Car>.Filter.Eq(c => c.Id, car.Id);
            //    carCollection.UpdateOne( filterCar, updateParts2 );
            //}
            //Console.WriteLine( "added to each car" );

            //Console.WriteLine("Method 3 :  ");
            //var updateFilter = Builders<Car>.Filter.And(
            //Builders<Car>.Filter.Eq(c => c.Brand, "Toyota"),
            //Builders<Car>.Filter.Eq(c => c.Model, "Camry")
            //);

            //// Phụ tùng mới cần thêm
            //string newPart = "Sunroof";

            //// Sử dụng Push để thêm phụ tùng vào Parts
            //var updateParts3 = Builders<Car>.Update.Push(c => c.Parts, newPart);

            //// Thực hiện cập nhật
            //var result = carCollection.UpdateOne( updateFilter, updateParts3 );

            // Delete 1 phụ tùng trong List của Car
            Console.WriteLine("-----------Delete 1 phụ tùng trong List của Car------------");

            // Chọn xe cần xóa phụ tùng (ví dụ: Toyota Camry)
            var deletefilter = Builders<Car>.Filter.And(
                Builders<Car>.Filter.Eq(c => c.Brand, "Ford"),
                Builders<Car>.Filter.Eq(c => c.Model, "Focus")
            );

            // Phụ tùng cần xóa
            string partToRemove = "Banh xe 2hgfhfghfghfh";

            // Sử dụng Pull để xóa phụ tùng khỏi Parts
            var update1 = Builders<Car>.Update.Pull(c => c.Parts, partToRemove);

            // Thực hiện cập nhật
            var result = carCollection.UpdateOne( deletefilter, update1 );

            // Kiểm tra kết quả
            if (result.ModifiedCount > 0)
            {
                Console.WriteLine($"Phụ tùng '{partToRemove}' đã được xóa khỏi xe Toyota Camry.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy xe hoặc phụ tùng để xóa.");
            }


        }
    }
}
