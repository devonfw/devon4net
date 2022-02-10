using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excalibur.Shared.Business
{
    /// <summary>
    /// An entity to represent a list of items and with it a list of domain objects.
    /// This should be used when for example managing a list of objects. 
    /// For example, users, todos, comments, etc.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TDomain">The type of the object that wants to be represented</typeparam>
    public interface IListBusiness<in TId, TDomain> : IBusiness
    {
        /// <summary>
        /// Get all domain objects that are managed by this business entity
        /// </summary>
        /// <returns>An await able Task with all objects as result</returns>
        Task<IList<TDomain>> GetAllAsync();
        /// <summary>
        /// Get a single domain object from storage by <see cref="TId"/>
        /// </summary>
        /// <param name="id">The Id of the object to get</param>
        /// <returns>An await able Task with the requested object as result</returns>
        Task<TDomain> GetByIdAsync(TId id);
        /// <summary>
        /// Deletes a domain object by <see cref="TId"/>
        /// </summary>
        /// <param name="id">The Id of the object to delete</param>
        /// <returns>An await able Task</returns>
        Task DeleteItemAsync(TId id);
    }
}