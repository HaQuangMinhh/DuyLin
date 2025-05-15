namespace GenericClassAndMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Run Demo Generic Class : Box
            Box<int> intBox = new Box<int>();
            intBox.SetValue(500);
            intBox.Print(); 
            

            Box<String> stringBox = new Box<string>();
            stringBox.SetValue("hehe");
            stringBox.Print();


            Console.WriteLine("----------------Generic Method-------------------");
            var a = 10;
            var b = 50;

            intBox.Swap( ref a , ref b); // Using ref 
            Console.WriteLine( $"a = {a} , b = {b}" );

            var c = "Good Afternoon";
            var d = "Hello";
            stringBox.Swap( ref c , ref d );
            Console.WriteLine( $"c = {c} , d = {d}" );


            Console.WriteLine("----------------Generic Interface-------------------");
            IProduct<String> product = new StringProduct();

            product.Add("Banana");
            product.Add("Apple");

            product.PrintAll();


            Console.WriteLine("----------------Generic Collection-------------------");
            List<int> numbers = new List<int> { 10, 20, 30, 40, 50 };
            numbers.Add(60);

            Console.WriteLine("List of numbers:");
            foreach (int num in numbers)
            {
                Console.Write($"{num} , ");
            }


        }
    }
}
