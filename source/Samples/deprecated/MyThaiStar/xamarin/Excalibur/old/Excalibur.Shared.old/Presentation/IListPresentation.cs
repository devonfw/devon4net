using Excalibur.Shared.Collections;
using Excalibur.Shared.Observable;

namespace Excalibur.Shared.Presentation
{
    /// <inheritdoc />
    public interface IListPresentation<TId, TObservable> : IListPresentation<TId, TObservable, TObservable>
        where TObservable : ObservableBase<TId>, new()
    {
    }

    /// <summary>
    /// Presentation will make it possible to use one entity for sharing observable objects. 
    /// Presentation base will map domain objects to observable object which can be passed by reference to view models. 
    /// 
    /// Using a presentation sharing of lists and updating views is easier, since mapping and managing will be done in one entity.
    /// 
    /// This presentation will manage the all objects. 
    /// References to observables will be updated when an update is published and will be updated with new information when needed.
    /// The selected observable will be used by when navigating to detail view models.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TObservable">The type that should be used for the collections of objects</typeparam>
    /// <typeparam name="TSelectedObservable">The type that should be used for details information</typeparam>
    public interface IListPresentation<TId, TObservable, TSelectedObservable> : ISinglePresentation<TId, TSelectedObservable>
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        /// <summary>
        /// The observable collection that contains mapped domain objects
        /// </summary>
        IObservableCollection<TObservable> Observables { get; set; }

        /// <summary>
        /// Sets the <see cref="SetSelectedObservable"/> to the object with corresponding Id
        /// </summary>
        /// <param name="observableId">The id of the object that should be set as <see cref="SetSelectedObservable"/></param>
        void SetSelectedObservable(TId observableId);
        /// <summary>
        /// If needed, a certain <see cref="TObservable"/> can be requested.
        /// </summary>
        /// <param name="observableId">The id of the object that should be returned</param>
        /// <returns>The requested <see cref="TObservable"/></returns>
        TObservable GetObservable(TId observableId);
    }
}
