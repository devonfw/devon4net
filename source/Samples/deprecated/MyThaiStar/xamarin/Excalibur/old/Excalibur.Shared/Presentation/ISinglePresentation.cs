using Excalibur.Shared.Observable;

namespace Excalibur.Shared.Presentation
{
    /// <summary>
    /// Presentation will make it possible to use one entity for sharing observable objects. 
    /// Presentation base will map domain objects to observable object which can be passed by reference to view models. 
    /// 
    /// Using a presentation sharing of lists and updating views is easier, since mapping and managing will be done in one entity.
    /// 
    /// This interface is used for <see cref="BaseSinglePresentation{TId,TDomain,TObservable}"/> as well as a sub interface for <see cref="BaseListPresentation{TId,TDomain,TObservable,TSelectedObservable}"/> 
    /// and <see cref="BaseSortedPresentation{TId,TDomain,TObservable,TSelectedObservable,TComparer}"/>
    /// 
    /// Single presentation will manage the one object. 
    /// The selected observable will be used by the List implementation <see cref="IListPresentation{TId,TObservable,TSelectedObservable}"/> as the reference
    /// that will be updated when selecting a Observable.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TSelectedObservable">The type that should be used for details information</typeparam>
    public interface ISinglePresentation<TId, TSelectedObservable>
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        /// <summary>
        /// Property used for indicating that the presentation is currently busy
        /// </summary>
        bool IsLoading { get; set; }
        /// <summary>
        /// The selected observable
        /// </summary>
        TSelectedObservable SelectedObservable { get; set; }

        /// <summary>
        /// Initialize method that might be needed.
        /// </summary>
        void Initialize();
    }
}