using System.Threading.Tasks;

namespace Excalibur.Shared.Business
{
    /// <summary>
    /// An entity to represent a single instance and single domain object. 
    /// This should be used when for example managing a 1 object instead of a list of objects. 
    /// For example, about information, app info, etc.
    /// </summary>
    /// <typeparam name="TDomain">The type of the object that wants to be represented</typeparam>
    public interface ISingleBusiness<TDomain> : IBusiness
    {
        /// <summary>
        /// Gets the represented object
        /// </summary>
        /// <returns>An await able Task with the requested object as result</returns>
        Task<TDomain> GetAsync();
        /// <summary>
        /// Deletes the represented object
        /// </summary>
        /// <returns>An await able task</returns>
        Task DeleteAsync();
    }
}