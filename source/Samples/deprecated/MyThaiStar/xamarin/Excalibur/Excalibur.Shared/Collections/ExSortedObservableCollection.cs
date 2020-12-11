using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Excalibur.Shared.Collections
{
    /// <summary>
    /// A base sorted observable collection
    /// </summary>
    /// <typeparam name="T">The type used within the collection</typeparam>
    public class ExSortedObservableCollection<T> : ObservableCollection<T>, ISortedObservableCollection<T>
    {
        private readonly IComparer<T> _comparer;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public ExSortedObservableCollection(IComparer<T> comparer = null)
            : base(new List<T>())
        {
            _comparer = comparer ?? Comparer<T>.Default;
        }

        /// <summary>
        /// Object that will be used to lock() on.
        /// </summary> 
        private readonly object _lock = new object();

        /// <summary>
        /// GetEnumerator implementation for thread safety
        /// </summary>
        /// <returns></returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            // We have to wrap the unsafe enumerator into a thread-safe class
            // And then return it
            var enumerator = new SafeEnumerator<T>(_lock);

            enumerator.SetList(Items.GetEnumerator());

            return enumerator;
        }

        /// <inheritdoc />
        /// <summary>
        /// To make this class actually thread safe, all collection operations should be lock()'ed on.
        /// </summary>
        public new void Add(T item)
        {
            lock (_lock)
            {
                base.Add(item);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// To make this class actually thread safe, all collection operations should be lock()'ed on.
        /// </summary>
        public new bool Remove(T item)
        {
            lock (_lock)
            {
                return base.Remove(item);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// To make this class actually thread safe, all collection operations should be lock()'ed on.
        /// </summary>
        public new T this[int index]
        {
            get
            {
                lock (_lock)
                {
                    return base[index];
                }
            }
            set
            {
                lock (_lock)
                {
                    base[index] = value;
                }
            }
        }

        // Todo, change this to Add?
        /// <inheritdoc />
        public void InsertItem(T item)
        {
            for (var i = 0; i < Count; i++)
            {
                switch (_comparer.Compare(item, Items[i]))
                {
                    case 0:
                    case 1:
                    {
                        InsertAt(i, item);
                        return;
                    }
                    case -1:
                        break;

                }
            }

            InsertAt(Count, item);
        }

        /// <summary>
        /// Another method for inserting an item into the collection, <see cref="InsertItem"/> for the actual implementation
        /// To make this class actually thread safe, all collection operations should be lock()'ed on.
        /// </summary>
        public void InsertAt(int index, T item)
        {
            lock (_lock)
            {
                base.InsertItem(index, item);
            }
        }
    }
}
