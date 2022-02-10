using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Excalibur.Shared.Collections
{
    /// <summary>
    /// A base observable collection
    /// </summary>
    /// <typeparam name="T">The type used within the collection</typeparam>
    public class ExObservableCollection<T> : ObservableCollection<T>, IObservableCollection<T>
    {
        public ExObservableCollection(IList<T> source)
            : base(source)
        {
        }

        // this is the object we shall use as a lock. 
        private readonly object _lock = new object();

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            // instead of returning an unsafe enumerator,
            // we wrap it into our thread-safe class
            var enumerator = new SafeEnumerator<T>(_lock);

            enumerator.SetList(Items.GetEnumerator());

            return enumerator;
        }

        // To be actually thread-safe, our collection
        // must be locked on all other operations
        // For example, this is how Add() method should look
        public new void Add(T item)
        {
            lock (_lock)
            {
                base.Add(item);
            }
        }

        public new bool Remove(T item)
        {
            lock (_lock)
            {
                return base.Remove(item);
            }
        }

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
    }
}
