using System;
using Collections;

namespace Linq
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var list = new FastList<int>()
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

            /*var fastList = new FastList<int>
            {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8
            };

            for (int i = 0; i < fastList.Count; i++)
            {
                Console.Write($"{fastList[i]} ");
            }
            
            Console.WriteLine("");
            fastList.CopyTo(new[] {995, 996, 997, 998, 999}, 0);
            
            for (int i = 0; i < fastList.Count; i++)
            {
                Console.Write($"{fastList[i]} ");
            }
            
            Console.WriteLine("");
            fastList.Sort((item1, item2) => item2 - item1);
            
            for (int i = 0; i < fastList.Count; i++)
            {
                Console.Write($"{fastList[i]} ");
            }*/
        }
    }
}