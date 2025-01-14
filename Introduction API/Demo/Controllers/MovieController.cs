using Demo.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver; 

namespace Demo.Controllers
{
    [ApiController]
    [Route("CRUD_Movie/api/[controller]")]
    public class MovieController : Controller
    {
        // Create a new controller . Need to remember 
        private readonly IMongoCollection<Movie> _movieCollection ;

        public MovieController()
        {
            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("YourDatabaseName");
            _movieCollection = database.GetCollection<Movie>("Movies");
        }

        [HttpGet] 
        public IActionResult GetAllMovies()
        {
            var allMovies = _movieCollection.Find(_ => true).ToList();
            return Ok(allMovies);   
        }

        [HttpPost]
        public IActionResult CreateMovies( CreateMovieModel newMovie ) {

            var movieData = new Movie
            {
                Name = newMovie.Name,
                Description = newMovie.Description, 
                Films = newMovie.Films,
            };
            
            _movieCollection.InsertOne(movieData); 
            return Ok(newMovie);
        }

        // Update Movie by name   
        [HttpPut("{name}")]
        public IActionResult UpdateMovieByName(string name ,[FromBody] CreateMovieModel updatedMovie)
        {
            var findMovie = _movieCollection.Find(s => s.Name == name).FirstOrDefault();

            if ( findMovie == null ) {
                return NotFound($"Movie: '{name}' not found.");
            }

            // nếu tìm thấy 
            var filterUpdate = Builders<Movie>.Filter.Eq(s => s.Name, name); 

            var movieData = new Movie
            {
                Id = findMovie.Id,
                Name = updatedMovie.Name,
                Description = updatedMovie.Description,
                Films = updatedMovie.Films,
            };

            var updateResult = _movieCollection.ReplaceOne(filterUpdate, movieData);

            if ( name == null ) {
                return NotFound($"Movie: {name} not found");
            }
            
            return Ok($" Movie: {name} updated successfully");
        }

        // Delete
        [HttpDelete("{name}")]
        public IActionResult DeleteMovie( string name ) {
            
            // Nếu không tìm thấy 
            var findMovie = _movieCollection.Find(s => s.Name == name).FirstOrDefault();
            if ( findMovie == null ) {
                return NotFound($"Movie: {name} not found" );
            }

            // nếu tìm thấy 
            var deleteMovie = _movieCollection.DeleteOne(s => s.Name == name);
            return Ok( $" Movie: {name} deleted successfully" );
        }


        // 3 Get data : lấy data ( 1 cùng tên mới trả về ) 
        





    }
}
