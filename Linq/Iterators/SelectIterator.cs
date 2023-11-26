using System;
using System.Collections;
using System.Collections.Generic;

namespace Linq.Iterators
{
    public class SelectIterator<TSource, TResult>: IEnumerator<TResult>, IEnumerable<TResult>
    {
        private Func<TSource, TResult> m_Selector;
        private IEnumerator<TSource> m_Enumerator;
        private bool m_AlreadyDisposed;

        public SelectIterator(IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (selector == null || source == null)
            {
                throw new ArgumentNullException();
            }
            
            m_Selector = selector;
            m_Enumerator = source.GetEnumerator();
        }

        public TResult Current { get; private set; }
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (m_AlreadyDisposed)
            {
                return false;
            }

            if (m_Enumerator.MoveNext())
            {
                var item = m_Enumerator.Current;
                Current = m_Selector(item);
                return true;
            }

            Dispose();
            return false;
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            m_Selector = null;
            m_Enumerator = null;
            m_AlreadyDisposed = true;
        }

        public IEnumerator<TResult> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}