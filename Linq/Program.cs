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
                .Where(l => l % 2 == 0);

            foreach (var item in result)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine("");
            
            Console.WriteLine(result.Any(i => i > 50));
            Console.WriteLine(result.Contains(4));
        }
    }
}