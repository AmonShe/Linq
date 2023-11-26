using System;
using System.Collections.Generic;

namespace Linq
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var list = new List<int>
            {
                11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1
            };

            int count = 1;
            var result = list
                .Where(l => count++ < 5)
                .OrderBy((item1, item2) => item2 - item1);
                   
            list.Insert(0, 12);
              
            foreach (var item in result)
            {
                Console.Write($"{item} ");
            }
            
            //Output: 9 10 11 12
        }
    }
}