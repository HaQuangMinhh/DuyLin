using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericClassAndMethods
{
    class StringProduct : IProduct<string>
    {
        private List<string> items = new List<string>(); 

    
        public void Add(string item)
        {
            items.Add(item);
        }

        public void PrintAll()
        {
            Console.WriteLine("All products showed here: ");
            
            foreach ( var show in items ) {
                Console.WriteLine( $"product: {show}" );
            }
        }
    }
}
