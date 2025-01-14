namespace Demo.Models
{
    public class CreateStudentModel
    {

        public String Name { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }

        public List<String> Subjects { get; set; }
    }
}
