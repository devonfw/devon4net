using System.Threading.Tasks;

namespace Excalibur.Shared.Storage
{
    /// <summary>
    /// This interface provides an implementation for storing files.
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Store a string on a certain location
        /// </summary>
        /// <param name="folder">The name of the folder</param>
        /// <param name="fullName">The name of the file</param>
        /// <param name="contentAsString">The content that should be written to file</param>
        /// <returns></returns>
        Task<string> StoreAsync(string folder, string fullName, string contentAsString);
        /// <summary>
        /// Store a byte[] on a certain location
        /// </summary>
        /// <param name="folder">The name of the folder</param>
        /// <param name="fullName">The name of the file</param>
        /// <param name="contentAsBytes">The content that should be written to file</param>
        /// <returns></returns>
        Task<string> StoreAsync(string folder, string fullName, byte[] contentAsBytes);
        /// <summary>
        /// Read a file as string from a certain location
        /// </summary>
        /// <param name="folder">The name of the folder</param>
        /// <param name="fullName">The name of the file</param>
        /// <returns>File content as string</returns>
        Task<string> ReadAsTextAsync(string folder, string fullName);
        /// <summary>
        /// Read a file as byte[] from a certain location
        /// </summary>
        /// <param name="folder">The name of the folder</param>
        /// <param name="fullName">The name of the file</param>
        /// <returns>File content as byte[]</returns>
        Task<byte[]> ReadAsBinaryAsync(string folder, string fullName);
        /// <summary>
        /// Delete a file on a certain location
        /// </summary>
        /// <param name="folder">The name of the folder</param>
        /// <param name="fullName">The name of the file</param>
        void DeleteFile(string folder, string fullName);
        /// <summary>
        /// Check if a file exists on a certain location
        /// </summary>
        /// <param name="folder">The name of the folder</param>
        /// <param name="fullName">The name of the file</param>
        /// <returns>True if the file exists, otherwise false.</returns>
        bool Exists(string folder, string fullName);
    }
}
