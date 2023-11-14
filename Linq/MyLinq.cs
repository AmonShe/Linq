using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    public static class MyLinq
    {
        public static IEnumerable<TResult> Select<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            if (source == null || selector == null)
            {
                return null;
            }
            
            List<TResult> result = new List<TResult>();

            foreach (var item in source)
            {
                result.Add(selector.Invoke(item));
            }

            return result;
        }

        public static IEnumerable<TSource> Where<TSource>(
            this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            if (source == null || predicate == null)
            {
                return null;
            }

            List<TSource> result = new List<TSource>();

            foreach (var item in source)
            {
                if (predicate.Invoke(item))
                {
                    result.Add(item);
                }
            }
            
            result.Sum()

            return result;
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

            foreach (var sourceItem in source)
            {
                if (sourceItem.GetHashCode().Equals(item.GetHashCode()))
                {
                    return true;
                }
            }

            return false;
        }
        
        

        public static List<T> ToList<T>(this IEnumerable<T> source)
        {
            return new List<T>(source);
        }
    }
}