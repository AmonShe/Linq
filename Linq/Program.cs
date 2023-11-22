using System;

namespace Linq
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var list = new[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10
            };

            var result = list.Select(l => l * l)
                .Where(l => l % 3 == 0)
                .ToList();

            foreach (var item in result)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine("");
            
            int sum = list.Select(l => l * l)
                .Where(l => l % 3 == 0)
                .Sum();
            
            int count = list.Select(l => l * l)
                .Where(l => l % 3 == 0)
                .Count();
            
            bool any = list.Select(l => l * l)
                .Where(l => l % 3 == 0)
                .Any(i => i > 50);
            
            var contains = list.Select(l => l * l)
                .Where(l => l % 3 == 0)
                .Contains(4);
            
            Console.WriteLine(any);
            Console.WriteLine(contains);
            Console.WriteLine(count);
            Console.WriteLine(sum);
        }
    }
}