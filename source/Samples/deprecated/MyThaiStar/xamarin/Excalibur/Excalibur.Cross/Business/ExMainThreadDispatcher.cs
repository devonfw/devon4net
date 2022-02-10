using System;
using System.Threading.Tasks;
using Excalibur.Shared.Business;
using MvvmCross.Base;

namespace Excalibur.Cross.Business
{
    /// <summary>
    /// Excalibur MvvmCross based main thread dispatcher. 
    /// </summary>
    public class ExMainThreadDispatcher : IExMainThreadDispatcher
    {
        /// <summary>
        /// Schedules an action to be run on the main thread of the application using MvvmCross <see cref="IMvxMainThreadDispatcher"/>
        /// </summary>
        /// <param name="action">The action to be invoked on the main thread</param>
        /// <returns>The result</returns>
        public Task InvokeOnMainThread(Action action)
        {
            return MvvmCross.Mvx.Resolve<IMvxMainThreadAsyncDispatcher>().ExecuteOnMainThreadAsync(action);
        }
    }
}
