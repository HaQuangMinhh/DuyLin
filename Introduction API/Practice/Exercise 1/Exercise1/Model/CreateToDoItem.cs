namespace Exercise1.Model
{
    public class CreateToDoItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Boolean IsCompleted { get; set; }
        public Priority Priority { get; set; }
        public String DueDate { get; set; }  // nhận vào 1 String 
        public List<String> Tags { get; set; }    //work , personal 
        public int EstimatedDuration { get; set; }
    }
}
