using System;
using Logic;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Parser();
            
            var str = @"a = 2;b = 3;c = a + b; 2 c ^ 3";
            
            foreach (var token in p.Parse(str))
            {
                Console.WriteLine(token);
            }

            var d = new Driver(str);
            
            Console.WriteLine("Hello World!");
        }
    }
}