using Excalibur.Shared.Collections;
using Excalibur.Shared.Observable;

namespace Excalibur.Shared.Presentation
{
    /// <summary>
    /// Presentation will make it possible to use one entity for sharing observable objects. 
    /// Presentation base will map domain objects to observable object which can be passed by reference to view models. 
    /// 
    /// Using a presentation sharing of lists and updating views is easier, since mapping and managing will be done in one entity.
    /// 
    /// This presentation will manage the all objects with the possibility to sort objects.
    /// References to observables will be updated when an update is published and will be updated with new information when needed.
    /// The selected observable will be used by when navigating to detail view models.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="TObservable">The type that should be used for the collections of objects</typeparam>
    /// <typeparam name="TSelectedObservable">The type that should be used for details information</typeparam>
    public interface ISortedPresentation<TId, TObservable, TSelectedObservable> : ISinglePresentation<TId, TSelectedObservable>
        where TObservable : ObservableBase<TId>, new()
        where TSelectedObservable : ObservableBase<TId>, new()
    {
        /// <summary>
        /// The sorted observable collection that contains mapped domain objects
        /// </summary>
        ISortedObservableCollection<TObservable> Observables { get; set; }

        /// <summary>
        /// Sets the <see cref="SetSelectedObservable"/> to the object with corresponding Id
        /// </summary>
        /// <param name="observableId">The id of the object that should be set as <see cref="SetSelectedObservable"/></param>
        void SetSelectedObservable(TId observableId);
    }
}