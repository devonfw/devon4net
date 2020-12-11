using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyThaiStar.Core.Business.Interfaces
{
    public interface IStorableObject<T> where T : class
    {
        List<T> GetItems();
        Task DeleteItem(T item);
        Task AddItem(T item);
    }
}
