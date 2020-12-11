namespace Excalibur.Shared.Observable
{
    /// <summary>
    /// Base observable that is used for entities that want to be used by Excalibur. 
    /// This base provides a default implementation containing the Id as TId.
    /// </summary>    
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    public abstract class ObservableBase<TId> : ObservableObjectBase
    {
        private TId _id = default(TId);

        /// <summary>
        /// The Id of the observable entity
        /// </summary>
        public TId Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        /// <summary>
        /// A simple check to see if the object is Transient at this moment.
        /// Current implementation checks Id == null || same as default(TId)
        /// </summary>
        /// <returns>True if the object is transient, was otherwise</returns>
        public virtual bool IsTransient()
        {
            return Id == null || Id.Equals(default(TId));
        }
    }
}
