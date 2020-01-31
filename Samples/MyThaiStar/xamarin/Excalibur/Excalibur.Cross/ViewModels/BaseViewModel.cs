using Excalibur.Cross.Language;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Localization;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Excalibur.Cross.ViewModels
{
    /// <summary>
    /// BaseViewModel implementation that contains the MvvmCross <see cref="IMvxNavigationService"/>, Text bindings and a go back command.
    /// </summary>
    public abstract class BaseViewModel : MvxViewModel
    {
        /// <summary>
        /// The MvvmCross NavigationService
        /// </summary>
        protected IMvxNavigationService NavigationService { get; } = Mvx.Resolve<IMvxNavigationService>();

        /// <summary>
        /// A TextSource binding for localization
        /// </summary>
        public IMvxLanguageBinder TextSource => new MvxLanguageBinder(ExTextProvider.GeneralNamespace, GetType().Name);

        /// <summary>
        /// The ShareTextSource binding for localization
        /// </summary>
        public IMvxLanguageBinder SharedTextSource => new MvxLanguageBinder(ExTextProvider.GeneralNamespace, ExTextProvider.SharedNamespace);

        /// <summary>
        /// A MvvmCross Navigate back command
        /// This will just call Close(this) to close the current view.
        /// </summary>
        public virtual IMvxAsyncCommand GoBackCommand
        {
            get
            {
                return new MvxAsyncCommand(async () => await NavigationService.Close(this));
            }
        }
    }

    /// <summary>
    /// BaseViewModel implementation that extends the standard BaseViewModel implementation to accept a TParameter on navigation to the viewmodel.
    /// </summary>
    public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter>
    {
        /// <inheritdoc />
        public abstract void Prepare(TParameter parameter);
    }

    /// <summary>
    /// BaseViewModel implementation that extends the standard BaseViewModel implementation to accept a TParameter on navigation to the viewmodel and returns a TResult when the viewmodel is closed
    /// </summary>
    public abstract class BaseViewModel<TParameter, TResult> : BaseViewModelResult<TResult>, IMvxViewModel<TParameter, TResult>
    {
        /// <inheritdoc />
        public abstract void Prepare(TParameter parameter);
    }
}
