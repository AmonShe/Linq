using System;
using System.Collections;
using System.Collections.Generic;

namespace Linq.Iterators
{
    public class WhereIterator<TSource>: IEnumerator<TSource>, IEnumerable<TSource>
    {
        private Predicate<TSource> m_Predicate;
        private IEnumerator<TSource> m_Enumerator;
        private bool m_AlreadyDisposed;

        public WhereIterator(IEnumerable<TSource> source, Predicate<TSource> predicate)
        {
            m_Enumerator = source.GetEnumerator();
            m_Predicate = predicate;
        }

        public TSource Current { get; private set; }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (m_AlreadyDisposed)
            {
                return false;
            }

            while (m_Enumerator.MoveNext())
            {
                if (m_Predicate(m_Enumerator.Current))
                {
                    Current = m_Enumerator.Current;
                    return true;
                }
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
            m_Predicate = null;
            m_Enumerator = null;
            m_AlreadyDisposed = true;
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}