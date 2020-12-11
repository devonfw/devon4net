using System.Threading.Tasks;

namespace Excalibur.Shared.State
{
    /// <summary>
    /// Interface for managing state of an application.
    /// </summary>
    public interface IBaseState
    {
        /// <summary>
        /// Initialize and load the state
        /// </summary>
        /// <returns>An await-able task</returns>
        Task InitAndLoadAsync();
        /// <summary>
        /// Save the state.
        /// </summary>
        /// <returns>An await-able task</returns>
        Task SaveAsync();
    }
}