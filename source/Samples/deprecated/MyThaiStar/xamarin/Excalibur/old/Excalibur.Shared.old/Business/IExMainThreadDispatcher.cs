using System;

namespace Excalibur.Shared.Business
{
    /// <summary>
    /// Excalibur main thread dispatcher. 
    /// Per platform this will have a different underlying implementation
    /// </summary>
    public interface IExMainThreadDispatcher
    {
        /// <summary>
        /// Schedules an action to be run on the main thread of the application
        /// </summary>
        /// <param name="action">The action to be invoked on the main thread</param>
        /// <returns>The result</returns>
        bool InvokeOnMainThread(Action action);
    }
}
