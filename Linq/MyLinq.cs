using System;
using System.Collections.Generic;
using Linq.Iterators;

namespace Linq
{
    public static class MyLinq
    {
        public static IEnumerable<TResult> Select<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            var iterator = new SelectIterator<TSource, TResult>(source, selector);
            while (iterator.MoveNext())
            {
                yield return iterator.Current;
            }
        }

        public static IEnumerable<TSource> Where<TSource>(
            this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            var iterator = new WhereIterator<TSource>(source, predicate);
            while (iterator.MoveNext())
            {
                yield return iterator.Current;
            }
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null || predicate == null)
            {
                return false;
            }

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource item)
        {
            if (source == null || item == null)
            {
                return false;
            }

            foreach (var element in source)
            {
                if (element.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }
        
        public static List<T> ToList<T>(this IEnumerable<T> source)
        {
            var result = new List<T>();
            foreach (var item in source)
            {
                result.Add(item);
            }
            return result;
        }

        public static int Count<T>(this IEnumerable<T> source)
        {
            int count = 0;
            foreach (var item in source)
            {
                count++;
            }
            return count;
        }
        
        public static int Sum(this IEnumerable<int> source)
        {
            int sum = 0;
            foreach (var item in source)
            {
                sum += item;
            }
            return sum;
        }
        
        public static float Sum(this IEnumerable<float> source)
        {
            float sum = 0;
            foreach (var item in source)
            {
                sum += item;
            }
            return sum;
        }

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, Func<T,T, int> compare)
        {
            if (source == null || compare == null)
            {
                return null;
            }

            return Sort(source, compare);
        }

        private static IEnumerable<T> Sort<T>(IEnumerable<T> source, Func<T,T, int> compare)
        {
            if (source == null)
            {
                return null;
            }

            T buff;
            var list = source.ToList();
            
            for (var i = 1; i < list.Count; i++)
            {
                for (var j = 0; j < list.Count - i; j++)
                {
                    if (compare(list[j], list[j + 1]) < 0)
                    {
                        buff = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = buff;
                    }
                }
            }

            return list;
        }
    }
}