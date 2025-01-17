namespace Exercise1.Controllers
{
    public class DateValidator
    {
        public static bool FutureDate(DateTime date) {
            return date > DateTime.Now;
        }
    }
}
