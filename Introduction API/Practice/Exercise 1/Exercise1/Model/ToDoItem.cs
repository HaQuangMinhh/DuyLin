using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Exercise1.Model
{
    public class ToDoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Title { get; set; }

        
        [MaxLength(500)]
        public string Description { get; set; }
       
        public Boolean IsCompleted { get; set; }
        
        [Required(ErrorMessage = "Priority is required.")]
        public Priority Priority { get; set; }

        [Required(ErrorMessage = "Due date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        public List<String> Tags { get; set; }    //work , personal 

        [Range(1, 1000, ErrorMessage = "Estimated duration must be between 1 and 1000.")]
        public int EstimatedDuration { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

    }
}
