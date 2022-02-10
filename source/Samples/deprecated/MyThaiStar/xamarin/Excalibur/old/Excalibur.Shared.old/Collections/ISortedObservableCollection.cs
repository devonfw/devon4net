using System.ComponentModel;

namespace Excalibur.Shared.Collections
{
    /// <summary>
    /// Interface for a sorted Observable Collection
    /// </summary>
    /// <typeparam name="T">The type used within the collection</typeparam>
    public interface ISortedObservableCollection<T> : IObservableCollection<T>, INotifyPropertyChanged
    {
        void InsertItem(T item);
    }
}
