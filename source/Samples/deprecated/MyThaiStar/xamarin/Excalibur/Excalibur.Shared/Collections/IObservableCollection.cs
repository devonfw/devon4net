using System.Collections.Generic;
using System.Collections.Specialized;

namespace Excalibur.Shared.Collections
{
    /// <summary>
    /// Interface for an Observable Collection
    /// </summary>
    /// <typeparam name="T">The type used within the collection</typeparam>
    public interface IObservableCollection<T> : IList<T>, INotifyCollectionChanged
    {
    }
}
