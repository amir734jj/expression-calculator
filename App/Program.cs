using System;
using Logic;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Parser();
            
            var str = @"
a = 1 + 2 * c * d;
b = c + a;
";
            
            foreach (var token in p.Parse(str))
            {
                Console.WriteLine(token);
            }

            var d = new Driver(str);
            
            Console.WriteLine("Hello World!");
        }
    }
}