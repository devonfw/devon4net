using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace Excalibur.Cross.ViewModels
{
    /// <summary>
    /// BaseViewModel implementation that returns a TResult when the viewmodel is closed.
    /// </summary>
    public abstract class BaseViewModelResult<TResult> : BaseViewModel, IMvxViewModelResult<TResult>
    {
        /// <inheritdoc />
        public abstract TaskCompletionSource<object> CloseCompletionSource { get; set; }

        /// <inheritdoc />
        public override void ViewDestroy(bool viewFinishing = true)
        {
            if (viewFinishing && CloseCompletionSource != null && !CloseCompletionSource.Task.IsCompleted && !CloseCompletionSource.Task.IsFaulted)
                CloseCompletionSource?.TrySetCanceled();

            base.ViewDestroy(viewFinishing);
        }
    }
}