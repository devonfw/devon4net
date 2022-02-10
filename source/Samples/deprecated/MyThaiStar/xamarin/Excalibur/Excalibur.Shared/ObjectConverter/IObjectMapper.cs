namespace Excalibur.Shared.ObjectConverter
{
    /// <summary>
    /// Interface for an object mapper
    /// </summary>
    /// <typeparam name="TSource">The type of the source object</typeparam>
    /// <typeparam name="TDestination">The type of the destination object</typeparam>
    public interface IObjectMapper<TSource, TDestination>
    {
        /// <summary>
        /// Maps a TSource to a TDestination object
        /// </summary>
        /// <param name="source">The instance of a source object</param>
        /// <returns>The mapped object as destination</returns>
        TDestination Map(TSource source);
        /// <summary>
        /// Updates the destination object with content from source
        /// </summary>
        /// <param name="source">The instance of a source object</param>
        /// <param name="destination">The instance of a destination object</param>
        void UpdateDestination(TSource source, TDestination destination);
        /// <summary>
        /// Updates the source object with content from destination
        /// </summary>
        /// <param name="destination">The instance of a destination object</param>
        /// <param name="source">The instance of a source object</param>
        void UpdateSource(TDestination destination, TSource source);
    }
}
