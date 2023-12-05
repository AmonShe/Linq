using System;
using System.Collections;
using System.Collections.Generic;

namespace Linq.Iterators
{
    public class OrderByIterator<TSource> : IEnumerator<TSource>, IEnumerable<TSource>
    {
        private IEnumerable<TSource> m_Source;
        private IEnumerator<TSource> m_Enumerator;
        private Func<TSource, TSource, int> m_Compare;
        private bool m_IsSort;
        
        public TSource Current { get; private set; }
        object IEnumerator.Current => Current;

        public OrderByIterator(IEnumerable<TSource> source, Func<TSource, TSource, int> compare)
        {
            if (source == null || compare == null)
            {
                throw new ArgumentNullException();
            }
            
            m_Source = source;
            m_Compare = compare;
            m_IsSort = false;
        }

        public IEnumerator<TSource> GetEnumerator() => this;
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public bool MoveNext()
        {
            if (!m_IsSort)
            {
                m_Source = Sort();
                m_Enumerator = m_Source.GetEnumerator();
                m_IsSort = true;
            }
            
            if (m_Enumerator.MoveNext())
            {
                Current = m_Enumerator.Current;
                return true;
            }

            Dispose();
            return false;
        }

        private IEnumerable<TSource> Sort()
        {
            if (m_Source == null)
            {
                return null;
            }

            TSource buff;
            var list = m_Source.ToList();
            
            for (var i = 1; i < list.Count; i++)
            {
                for (var j = 0; j < list.Count - i; j++)
                {
                    if (m_Compare(list[j], list[j + 1]) < 0)
                    {
                        buff = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = buff;
                    }
                }
            }

            return list;
        }
        
        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            m_Source = null;
            m_Compare = null;
        }
    }
}