using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code2040Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = Registration.Request();
            Console.Read();
            return;
            string revStr = ReverseString.Request(token);
            Console.WriteLine("String Reversed: " + revStr);
            Console.WriteLine("Validation: " + ReverseString.Validate(token, revStr));
            Console.Read();
        }
    }
}
