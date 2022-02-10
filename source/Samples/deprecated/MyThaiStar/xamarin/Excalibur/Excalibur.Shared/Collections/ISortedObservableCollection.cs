using System.ComponentModel;

namespace Excalibur.Shared.Collections
{
    /// <summary>
    /// Interface for a sorted Observable Collection
    /// </summary>
    /// <typeparam name="T">The type used within the collection</typeparam>
    public interface ISortedObservableCollection<T> : IObservableCollection<T>, INotifyPropertyChanged
    {
        /// <summary>
        /// To make this class actually thread safe, all collection operations should be lock()'ed on.
        /// </summary>
        void InsertItem(T item);
    }
}
