using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericClassAndMethods
{
    class Box<T>
    {
        private T Value { get; set; }

        public void SetValue ( T value )   // constructor
        {
            Value = value;         
        }

        public T GetValue( ) {
            return Value;  
        }

        public void Print() {
            Console.WriteLine( $"Value:  {Value}" );
        }

        public void Swap<T>(ref T a ,ref T b ) {
            T temp = a;
            a = b;
            b = temp; 
        }


    }
}
