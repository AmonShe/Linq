using System;
using System.Collections.Generic;
using Linq.Iterators;

namespace Linq
{
    public static class MyLinq
    {
        public static IEnumerator<TResult> Select<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            var iterator = new SelectIterator<TSource, TResult>(source, selector);
            while (iterator.MoveNext())
            {
                yield return iterator.Current;
            }
        }

        public static IEnumerator<TSource> Where<TSource>(
            this IEnumerator<TSource> source,
            Predicate<TSource> predicate)
        {
            var iterator = new WhereIterator<TSource>(source, predicate);
            while (iterator.MoveNext())
            {
                yield return iterator.Current;
            }
        }

        public static bool Any<TSource>(this IEnumerator<TSource> source, Predicate<TSource> predicate)
        {
            if (source == null || predicate == null)
            {
                return false;
            }

            while (source.MoveNext())
            {
                if (source.Current != null && predicate(source.Current))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Contains<TSource>(this IEnumerator<TSource> source, TSource item)
        {
            if (source == null || item == null)
            {
                return false;
            }

            while (source.MoveNext())
            {
                if (source.Current != null && source.Current.Equals(item))
                {
                    return true;
                }
            }
            
            source.Dispose();
            return false;
        }
        
        public static List<T> ToList<T>(this IEnumerator<T> source)
        {
            var result = new List<T>();
            while (source.MoveNext())
            {
                result.Add(source.Current);
            }
            source.Dispose();
            return result;
        }

        public static int Count<T>(this IEnumerator<T> source)
        {
            int count = 0;
            while (source.MoveNext())
            {
                count++;
            }
            source.Dispose();
            return count;
        }
        
        public static int Sum(this IEnumerator<int> source)
        {
            int sum = 0;
            while (source.MoveNext())
            {
                sum += source.Current;
            }
            source.Dispose();
            return sum;
        }
        
        public static float Sum(this IEnumerator<float> source)
        {
            float sum = 0;
            while (source.MoveNext())
            {
                sum += source.Current;
            }
            source.Dispose();
            return sum;
        }
    }
}