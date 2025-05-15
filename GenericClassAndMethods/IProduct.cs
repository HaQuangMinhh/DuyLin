using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericClassAndMethods
{
    interface IProduct<T>
    {
        void Add(T item);
        void PrintAll();
    }
}
