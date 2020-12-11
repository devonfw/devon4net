namespace Excalibur.Shared.Business
{
    /// <summary>
    /// Enumeration for domain state when publishing
    /// </summary>
    public enum EDomainState
    {
        /// <summary>
        /// Created state for the entity. Means that the entity has been newly added.
        /// </summary>
        Created,

        /// <summary>
        /// Updated state for the entity. Means that the entity has been changed.
        /// </summary>
        Updated,

        /// <summary>
        /// Created state for the entity. Means that the entity has been deleted.
        /// </summary>
        Deleted
    }
}
