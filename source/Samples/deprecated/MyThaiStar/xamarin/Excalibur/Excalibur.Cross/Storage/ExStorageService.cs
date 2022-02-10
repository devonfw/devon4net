using System;
using System.Threading.Tasks;
using Excalibur.Shared.Storage;
using MvvmCross;
using MvvmCross.Logging;
using MvvmCross.Plugin.File;

namespace Excalibur.Cross.Storage
{
    /// <summary>
    /// MvvmCross implementation of the <see cref="IStorageService"/>
    /// </summary>
    public class ExStorageService : StorageServiceBase
    {
        private readonly IMvxFileStore _fileStore;
        private readonly IMvxFileStoreAsync _fileStoreAsync;

        /// <summary>
        /// Initializes a new ExStorageService.
        /// This will resolve internal MvvmCross dependencies <see cref="IMvxFileStore"/> and <see cref="IMvxFileStoreAsync"/>
        /// </summary>
        public ExStorageService()
        {
            _fileStore = Mvx.Resolve<IMvxFileStore>();
            _fileStoreAsync = Mvx.Resolve<IMvxFileStoreAsync>();
        }

        /// <summary>
        /// Store a string on a certain location
        /// </summary>
        /// <param name="folder">The name of the folder</param>
        /// <param name="fullName">The name of the file</param>
        /// <param name="contentAsString">The content that should be written to file</param>
        /// <returns></returns>
        public override async Task<string> StoreAsync(string folder, string fullName, string contentAsString)
        {
            var fullPath = FileChecks(folder, fullName);

            await _fileStoreAsync.WriteFileAsync(fullPath, contentAsString).ConfigureAwait(false);
            return _fileStore.NativePath(fullPath);
        }

        /// <summary>
        /// Store a byte[] on a certain location
        /// </summary>
        /// <param name="folder">The name of the folder</param>
        /// <param name="fullName">The name of the file</param>
        /// <param name="contentAsBytes">The content that should be written to file</param>
        /// <returns></returns>
        public override async Task<string> StoreAsync(string folder, string fullName, byte[] contentAsBytes)
        {
            var fullPath = FileChecks(folder, fullName);

            await _fileStoreAsync.WriteFileAsync(fullPath, contentAsBytes).ConfigureAwait(false);
            return _fileStore.NativePath(fullPath);
        }

        /// <summary>
        /// Read a file as string from a certain location
        /// </summary>
        /// <param name="folder">The name of the folder</param>
        /// <param name="fullName">The name of the file</param>
        /// <returns>File content as string</returns>
        public override async Task<string> ReadAsTextAsync(string folder, string fullName)
        {
            var fullPath = _fileStore.PathCombine(folder, fullName);
            var result = await _fileStoreAsync.TryReadTextFileAsync(fullPath).ConfigureAwait(false);

            return result.Result;
        }

        /// <summary>
        /// Read a file as byte[] from a certain location
        /// </summary>
        /// <param name="folder">The name of the folder</param>
        /// <param name="fullName">The name of the file</param>
        /// <returns>File content as byte[]</returns>
        public override async Task<byte[]> ReadAsBinaryAsync(string folder, string fullName)
        {
            var fullPath = _fileStore.PathCombine(folder, fullName);
            var result = await _fileStoreAsync.TryReadBinaryFileAsync(fullPath).ConfigureAwait(false);

            return result.Result;
        }

        /// <summary>
        /// Delete a file on a certain location
        /// </summary>
        /// <param name="folder">The name of the folder</param>
        /// <param name="fullName">The name of the file</param>
        public override void DeleteFile(string folder, string fullName)
        {
            try
            {
                _fileStore.DeleteFile(_fileStore.PathCombine(folder, fullName));
            }
            catch (Exception ex)
            {
                Mvx.Resolve<IMvxLog>().ErrorException("ExStorageService.DeleteFile", ex);
            }
        }

        /// <summary>
        /// Check if a file exists on a certain location
        /// </summary>
        /// <param name="folder">The name of the folder</param>
        /// <param name="fullName">The name of the file</param>
        /// <returns>True if the file exists, otherwise false.</returns>
        public override bool Exists(string folder, string fullName)
        {
            var fullPath = _fileStore.PathCombine(folder, fullName);
            return _fileStore.Exists(fullPath);
        }

        /// <summary>
        /// Ensures the file can actually be written to the path and will return the full path as string
        /// </summary>
        /// <param name="folder">The name of the folder</param>
        /// <param name="fullName">The name of the file</param>
        /// <returns>Full path of the file location</returns>
        private string FileChecks(string folder, string fullName)
        {
            try
            {
                _fileStore.EnsureFolderExists(folder);
            }
            catch (Exception ex)
            {
                Mvx.Resolve<IMvxLog>().ErrorException("ExStorageService.FileChecks", ex);
            }

            var fullPath = _fileStore.PathCombine(folder, fullName);

            if (_fileStore.Exists(fullPath))
            {
                DeleteFile(folder, fullName);
            }
            return fullPath;
        }
    }
}
