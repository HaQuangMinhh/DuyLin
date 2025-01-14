using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Demo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentApiController : ControllerBase
    {

        private readonly IMongoCollection<Student> _studentCollection;

        public StudentApiController()
        {
            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("YourDatabaseName");
            _studentCollection = database.GetCollection<Student>("Students");
        }

        // GET: api/StudentsApi
        [HttpGet]
        public async Task<IActionResult> GetAllStudents() =>
            Ok (await _studentCollection.Find(_ => true).ToListAsync());



        // GET: api/StudentsApi/{name}
        [HttpGet("{name}")]
        public async Task<IActionResult> GetStudentByName(string name)
        {
            var student = await _studentCollection.Find(s => s.Name == name).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound($"Student with name {name} not found.");
            }
            return Ok(student);
        }

        // POST: api/StudentsApi
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] CreateStudentModel newStudent)
        {
            var studentData = new Student
            {
                Name = newStudent.Name,
                Email = newStudent.Email,
                PhoneNumber = newStudent.PhoneNumber,
                Subjects = newStudent.Subjects
            };

            await _studentCollection.InsertOneAsync(studentData);
            return CreatedAtAction(nameof(GetStudentByName), new { name = newStudent.Name }, studentData);
        }

        // PUT: api/StudentsApi/{name}
        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateStudent(string name, [FromBody] Student updatedStudent)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.Name, name);
            var updateResult = await _studentCollection.ReplaceOneAsync(filter, updatedStudent);

            if (updateResult.MatchedCount == 0)
            {
                return NotFound($"Student with name {name} not found.");
            }
            return NoContent();
        }

        // PATCH: api/StudentsApi/{name}/update-phone
        [HttpPatch("{name}/update-phone")]
        public async Task<IActionResult> UpdatePhoneNumber(string name, [FromBody] string newPhoneNumber)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.Name, name);
            var update = Builders<Student>.Update.Set(s => s.PhoneNumber, newPhoneNumber);

            var result = await _studentCollection.UpdateOneAsync(filter, update);
            if (result.MatchedCount == 0)
            {
                return NotFound($"Student with name {name} not found.");
            }
            return Ok($"Phone number updated for student {name}");
        }

        // DELETE: api/StudentsApi/{name}
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteStudent(string name)
        {
            var deleteResult = await _studentCollection.DeleteOneAsync(s => s.Name == name);
            if (deleteResult.DeletedCount == 0)
            {
                return NotFound($"Student with name {name} not found.");
            }
            return Ok($"Student {name} deleted successfully.");
        }

        //GET /api/StudentsApi: Lấy danh sách tất cả sinh viên.
        //GET /api/StudentsApi/{id}: Lấy thông tin một sinh viên theo ID.
        //POST /api/StudentsApi: Tạo mới một sinh viên (kèm dữ liệu trong body).
        //PUT /api/StudentsApi/{id}: Cập nhật thông tin sinh viên theo ID.
        //DELETE /api/StudentsApi/{id}: Xóa sinh viên theo ID.

    }
}
