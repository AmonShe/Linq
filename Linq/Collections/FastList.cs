using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    public class FastList<T>: IList<T>
    {
        private T[] m_Items;
        private int m_Size;
        private int m_Capacity;
        private int m_LeftFreeItems;
        private int m_RightFreeItems;
        private int m_Version;

        public int Count => m_Size;
        public int Capacity => m_Capacity;
        public bool IsReadOnly { get; }

        internal int Version => m_Version;
        internal int LeftFreeItems => m_LeftFreeItems;

        public FastList()
        {
            m_Items = Array.Empty<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new FastListIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }
            
            if (m_Size == 0)
            {
                if (m_Capacity == 0)
                {
                    m_Capacity = 8;
                    m_Items = new T[m_Capacity];
                }

                m_RightFreeItems = m_Capacity / 2;
                m_LeftFreeItems = m_RightFreeItems - 1;
                m_Items[m_LeftFreeItems] = item;
                m_Size = 1;
                m_Version++;
                return;
            }
            
            if (m_RightFreeItems <= 0)
            {
                IncreaseArray();
                m_Items[m_LeftFreeItems + m_Size - 1] = item;
            }
            else
            {
                m_Items[m_LeftFreeItems + m_Size] = item;
                m_Size++;
                m_RightFreeItems--;
            }

            m_Version++;
        }

        private void IncreaseArray()
        {
            m_Capacity *= 2;
            m_Size++;
            var items = new T[m_Capacity];
            int rightFreeItems = m_Capacity / 2 - m_Size / 2;
            int leftFreeItems = m_Capacity / 2 - (m_Size - m_Size / 2);

            Array.ConstrainedCopy(m_Items,
                m_LeftFreeItems,
                items,
                leftFreeItems,
                m_Size - 1);
            m_Items = items;
            m_LeftFreeItems = leftFreeItems;
            m_RightFreeItems = rightFreeItems;
        }

        public void Clear()
        {
            for (int i = 0; i < m_Size; i++)
            {
                this[i] = default;
            }
            m_Size = 0;
            m_Version++;
        }

        public bool Contains(T item)
        {
            foreach (var mItem in m_Items)
            {
                if (mItem.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
            }
            
            if (arrayIndex < 0 || arrayIndex > m_Size)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (array.Length == 0)
            {
                return;
            }

            if (m_RightFreeItems >= array.Length)
            {
                Array.Copy(m_Items, 
                    m_LeftFreeItems + arrayIndex, 
                    m_Items, 
                    m_LeftFreeItems + arrayIndex + array.Length, 
                    m_Size - arrayIndex);
                Array.Copy(array,
                    0,
                    m_Items,
                    m_LeftFreeItems + arrayIndex,
                    array.Length);

                m_RightFreeItems -= array.Length;
                m_Size += array.Length;
                m_Version++;
            }
            else
            {
                do
                {
                    m_Capacity *= 2;
                } while (m_Capacity < m_Size + array.Length);
                
                
                var items = new T[m_Capacity];
                int newSize = m_Size + array.Length;
                int rightFreeItems = m_Capacity / 2 - newSize / 2;
                int leftFreeItems = m_Capacity / 2 - (newSize - newSize / 2);

                Array.Copy(m_Items,
                    m_LeftFreeItems,
                    items,
                    leftFreeItems,
                    arrayIndex);
                
                Array.Copy(array,
                    0,
                    items,
                    leftFreeItems + arrayIndex,
                    array.Length);
                
                Array.Copy(m_Items,
                    m_LeftFreeItems + arrayIndex,
                    items,
                    leftFreeItems + array.Length + arrayIndex,
                    m_Size - arrayIndex);
                
                m_Items = items;
                m_LeftFreeItems = leftFreeItems;
                m_RightFreeItems = rightFreeItems;
                m_Size = newSize;
                m_Version++;
            }
        }

        public bool Remove(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            for (int i = 0; i < m_Size; i++)
            {
                if (this[i].Equals(item))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            
            return false;
        }

        
        public int IndexOf(T item)
        {
            for (int i = 0; i < m_Size; i++)
            {
                if (this[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index > m_Size)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (index == m_Size)
            {
                Add(item);
            }
            else if(index == 0)
            {
                InsertFirst(item);
            }
            else
            {
                InsertInCenter(index, item);
            }

            m_Version++;
        }

        private void InsertInCenter(int index, T item)
        {
            if (m_RightFreeItems > 0)
            {
                Array.Copy(m_Items, 
                    index, 
                    m_Items, 
                    index + 1, 
                    m_Size - index);
                
                m_Items[index] = item;
                m_RightFreeItems--;
                m_Size++;
            }
            else
            {
                IncreaseArray();
                Array.Copy(m_Items, 
                    index, 
                    m_Items, 
                    index + 1, 
                    m_Size - index - 1);
                
                m_Items[index] = item;
            }
        }

        private void InsertFirst(T item)
        {
            if (m_LeftFreeItems == 0)
            {
                IncreaseArray();
            }
            
            m_Items[m_LeftFreeItems - 1] = item;
            m_LeftFreeItems--;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= m_Size)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            Array.Copy(m_Items,
                m_LeftFreeItems + index + 1,
                m_Items,
                m_LeftFreeItems + index,
                m_Size - index);
            m_Size--;
            m_RightFreeItems++;
            m_Version++;
        }

        public void TrimExcess()
        {
            m_Items = Array.Empty<T>();
            m_Capacity = 0;
            m_Size = 0;
            m_LeftFreeItems = 0;
            m_RightFreeItems = 0;
            m_Version++;
        }

        public void Sort(Func<T,T, int> compare)
        {
            T buff;
            
            for (var i = 1; i < m_Size; i++)
            {
                for (var j = 0; j < m_Size - i; j++)
                {
                    if (compare(this[j], this[j + 1]) < 0)
                    {
                        buff = this[j];
                        this[j] = this[j + 1];
                        this[j + 1] = buff;
                    }
                }
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= m_Size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return m_Items[m_LeftFreeItems + index];
            }
            set
            {
                if (index < 0 || index >= m_Size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                
                m_Items[m_LeftFreeItems + index] = value;
            }
        }
        
        private struct FastListIterator: IEnumerable<T>, IEnumerator<T>
        {
        
            public T Current { get; private set; }
            object IEnumerator.Current => Current;

            private FastList<T> m_List;
            private int m_Version;
            private int m_Index;
            private bool m_IsDisposed;
        
            public FastListIterator(FastList<T> source)
            {
                m_List = source;
                m_Version = source.m_Version;
                m_Index = -1;
                Current = default;
                m_IsDisposed = false;
            }

            public IEnumerator<T> GetEnumerator() => this;
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public void Dispose()
            {
                m_IsDisposed = true;
                m_List = null;
            }

            public bool MoveNext()
            {
                if (m_List.m_Version != m_Version)
                {
                    throw new Exception();
                }

                if (m_IsDisposed)
                {
                    return false;
                }

                m_Index++;
                
                if (m_Index < m_List.Count)
                {
                    Current = m_List[m_Index];
                    return true;
                }
                
                Dispose();
                return false;
            }

            public void Reset()
            {
                throw new System.NotImplementedException();
            }

        }
    }
}