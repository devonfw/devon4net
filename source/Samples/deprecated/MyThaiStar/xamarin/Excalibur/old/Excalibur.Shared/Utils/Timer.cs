using System;
using System.Threading;
using System.Threading.Tasks;

namespace Excalibur.Shared.Utils
{
    /// <summary>
    /// PCL111 does not have a Timer implementation. This provides one.
    /// </summary>
    public sealed class Timer : CancellationTokenSource
    {
        /// <summary>
        /// Initializes a new timer using ints to measure time intervals. 
        /// </summary>
        /// <param name="callback">A delegate that represents a method that will be executed</param>
        /// <param name="state">Object (or null) containing information to be used in the callback</param>
        /// <param name="millisecondsDueTime">The amount of time to wait before the callback is initially invoked.</param>
        /// <param name="millisecondsPeriod">The interval time between invocations</param>
        /// <param name="waitForCallbackBeforeNextPeriod">Used to set if the period interval should wait until previous execution finises</param>
        public Timer(Action<object> callback, object state, int millisecondsDueTime, int millisecondsPeriod, bool waitForCallbackBeforeNextPeriod = false)
        {
            Task.Delay(millisecondsDueTime, Token).ContinueWith(async (t, s) =>
            {
                var tuple = (Tuple<Action<object>, object>)s;

                while (!IsCancellationRequested)
                {
                    if (waitForCallbackBeforeNextPeriod)
                        tuple.Item1(tuple.Item2);
                    else
                        Task.Run(() => tuple.Item1(tuple.Item2)).ConfigureAwait(false);

                    if (millisecondsPeriod <= 0)
                        break;
                    await Task.Delay(millisecondsPeriod, Token).ConfigureAwait(false);
                }

            }, Tuple.Create(callback, state), CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);
        }

        /// <summary>
        /// Releases all resources used by the current instance of Timer and will Cancel the token source.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Cancel();

            base.Dispose(disposing);
        }
    }
}
