using System;
using System.Collections.Generic;
using Collections;

namespace Linq
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            FastList<int> fastlist = new FastList<int>() { 1, 2, 3, 4, 5, 6 };
            List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6 };
            
            fastlist.Add(7);
            fastlist.Insert(0, 0);
            fastlist.Insert(0, 0);
            fastlist.Add(7);
            fastlist.Insert(0, 0);
            fastlist.Add(7);
            
            list.Add(7);
            list.Insert(0, 0);
            list.Insert(0, 0);
            list.Add(7);
            list.Insert(0, 0);
            list.Add(7);

            fastlist.Remove(7);
            list.Remove(7);
            
            foreach (var i in fastlist)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
            
            foreach (var i in list)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
                        
                        

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