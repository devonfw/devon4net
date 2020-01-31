using Newtonsoft.Json;

namespace Excalibur.Providers.File
{
    /// <summary>
    /// Class that provides custom file serializer settings that should be used when storing a file.
    /// </summary>
    public abstract class ObjectToFileSerializer
    {
        /// <summary>
        /// Method that should be implemented and will return the custom <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <returns>An instance of <see cref="JsonSerializerSettings"/></returns>
        public abstract JsonSerializerSettings JsonSerializerSettings();
    }
}