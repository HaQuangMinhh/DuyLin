using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Exercise1.Model; 

namespace Exercise1.Controllers
{
    [Route("api/todos/[controller]")]
    [ApiController]
    public class ToDoListControllers : ControllerBase
    {
        private readonly IMongoCollection<ToDoItem> _todoCollection;

        public ToDoListControllers() {
            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("YourDatabaseName");
            _todoCollection = database.GetCollection<ToDoItem>("ToDoItem"); 
        }

        [HttpGet("GetAllItems")]
        public async Task<IActionResult> GetAllToDoItems() { 
            
            // Truy van
            var allItems = await _todoCollection.Find( _ => true ).ToListAsync();

            var result = allItems.Select(item => new
            {
                item.Title,
                item.Description,
                item.IsCompleted,
                item.Priority,
                DueDate = item.DueDate.ToString("yyyy-MM-dd HH:mm"),  // Định dạng lại thời gian thành chuỗi theo định dạng mong muốn
                item.Tags,
                item.EstimatedDuration,
                //item.CreateAt   : Cái này là hiện đúng như dưới mongoDB
                CreateAt = item.CreateAt.ToString("yyyy-MM-dd HH:mm"),
            }).ToList();


            return Ok(result);
        }

        // Filter : Get adavanced
        [HttpGet("SearchByTags")]
        public async Task<IActionResult> SearchByTags( [FromQuery] string tag ) {
            // User truyền vào 1 List --> Tìm 

            // Check empty 
            if ( string.IsNullOrEmpty(tag) ) {
                return BadRequest(" Tags parameter is required");
            }

            // split each tag by , + bỏ khoảng trống
            var tagList = tag.Split(',').Select(tag => tag.Trim().ToLower() ).ToList() ;
            
            // Filter to find a tag in list
            var filterTag = Builders<ToDoItem>.Filter.AnyIn( x => x.Tags , tagList);  //AnyIn : tồn tại

            // Truy van 
            var todoTag = await _todoCollection.Find(filterTag).ToListAsync();

            // Check if không tìm thấy 
            if ( todoTag == null || !todoTag.Any() ) {
                return NotFound("No tag found matching in the list"); 
            }

            return Ok(todoTag);
        }


        [HttpPost]
        public async Task<IActionResult> CreateItems(CreateToDoItem newItem) {

            // Use Enum.tryParse to change 

            // Convert String to DateTime
            DateTime convertedDueDate;
            string expectedFormat = "yyyy-MM-dd HH-mm";       // format giờ - phút

            if ( ! DateTime.TryParseExact(newItem.DueDate, expectedFormat,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out convertedDueDate))
            {
                // Error kia
                return BadRequest("Invalid Due Date Format, Please use :yyyy-MM-dd HH-mm");
            }

            // Chỉnh múi giờ Việt Nam
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            // Chuyển đổi thời gian sang múi giờ Việt Nam
            DateTime vietnamTime = TimeZoneInfo.ConvertTime(convertedDueDate, vietnamTimeZone);

            // check if the due date in the future
            if ( !DateValidator.FutureDate(vietnamTime)  ) {
                return BadRequest("Due date must be in the future");
            }


            var itemData = new ToDoItem
            {
                Title = newItem.Title,
                Description = newItem.Description,
                IsCompleted = newItem.IsCompleted,
                Priority = newItem.Priority, // nhận vào số : Conver String to Enum --> Create 
                DueDate = vietnamTime,     // DateOnly or DateTime  
                Tags = newItem.Tags,
                EstimatedDuration = newItem.EstimatedDuration,

                CreateAt = DateTime.Now,
            };

            await _todoCollection.InsertOneAsync(itemData);

            return Ok(itemData);

 
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverDueItems() { 
            
            // Get today time
            var currentDate = DateTime.Now;

            // Truy van MongoDb : Filter
            var overdueTodos = await _todoCollection.Find(v => v.DueDate < currentDate && !v.IsCompleted).ToListAsync();

            return Ok( overdueTodos );
       
        }







    }
}
