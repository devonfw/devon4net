using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excalibur.Shared.Storage;
using Newtonsoft.Json;
using XLabs.Ioc;

namespace Excalibur.Providers.File
{
    /// <summary>
    /// Provides a <see cref="IObjectStorageProvider{TId,T}"/> implementation for storing objects as files.
    /// Basic implementation.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="T">The type of the object that wants to be stored</typeparam>
    /// <typeparam name="TSerializer">The type of serializer to use, <see cref="ObjectToFileSerializer"/></typeparam>
    public class ObjectAsFileStorageProvider<TId, T, TSerializer> : ObjectAsFileStorageProvider<TId, T>
        where T : StorageDomain<TId>
        where TSerializer : ObjectToFileSerializer, new()
    {
        /// <summary>
        /// Override for the <see cref="JsonSerializerSettings"/> that will Activate a new instance of TSerializer
        ///     and will return the <see cref="ObjectToFileSerializer.JsonSerializerSettings"/> from that type instead.
        /// </summary>
        /// <returns></returns>
        protected override JsonSerializerSettings JsonSerializerSettings()
        {
            return Activator.CreateInstance<TSerializer>().JsonSerializerSettings();
        }
    }

    /// <summary>
    /// Provides a <see cref="IObjectStorageProvider{TId,T}"/> implementation for storing objects as files.
    /// Basic implementation.
    /// </summary>
    /// <typeparam name="TId">  The type of Identifier to use for the database object. Ints, guids,
    ///                         etc. </typeparam>
    /// <typeparam name="T">The type of the object that wants to be stored</typeparam>
    public class ObjectAsFileStorageProvider<TId, T> : IObjectStorageProvider<TId, T>
    where T : StorageDomain<TId>
    {
        private const string DataFolder = "data";
        private const string FileName = "{0}.json";

        private readonly IStorageService _storageService;

        /// <summary>
        /// Initializes a <see cref="ObjectAsFileStorageProvider{TId,T}"/> resolving a <see cref="IStorageService"/> to store files with
        /// </summary>
        public ObjectAsFileStorageProvider()
        {
            _storageService = Resolver.Resolve<IStorageService>();
        }

        /// <summary>
        /// Stores a range of objects to file. 
        /// The <see cref="objectsToStore"/> will overwrite the current file
        /// </summary>
        /// <param name="objectsToStore">The objects to store</param>
        /// <returns>An await able Task</returns>
        public async Task StoreRangeAsync(IList<T> objectsToStore)
        {
            // Delete the file before writing.
            // Sometimes write operation will fail when trying to write to a file that already exisits
            _storageService.DeleteFile(DataFolder, String.Format(FileName, typeof(T).Name));

            var objectAsString = JsonConvert.SerializeObject(objectsToStore, JsonSerializerSettings());
            await _storageService.StoreAsync(DataFolder, String.Format(FileName, typeof(T).Name), objectAsString).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a range of <see cref="T"/> from file storage
        /// </summary>
        /// <returns>An await able Task with the range as result</returns>
        public async Task<IList<T>> GetRangeAsync()
        {
            var objectAsString = await _storageService.ReadAsTextAsync(DataFolder, String.Format(FileName, typeof(T).Name)).ConfigureAwait(false) ?? String.Empty;

            var result = JsonConvert.DeserializeObject<IList<T>>(objectAsString, JsonSerializerSettings()) ?? new List<T>();

            return result;
        }

        /// <summary>
        /// Get a certain <see cref="T"/> from file storage. 
        /// This will, at the moment, retrieve all items and then return just the one.
        /// </summary>
        /// <param name="id">The id of the object that will be retrieved</param>
        /// <returns>An await able Task with the requested object as result</returns>
        public async Task<T> GetAsync(TId id)
        {
            // todo: Seek actual id from disk instead of searching the list
            var items = await GetRangeAsync().ConfigureAwait(false);
            return items.FirstOrDefault(x => x.Id.Equals(id));
        }

        /// <summary>
        /// Adds or updates an object to file storage
        /// </summary>
        /// <param name="objectToStore">The object to store</param>
        /// <returns>An await able Task with the success as result</returns>
        public async Task<bool> AddOrUpdateAsync(T objectToStore)
        {
            // todo: Seek actual id from disk instead of searching the list
            // todo: Actual add/update at correct index and move the bytes
            var items = await GetRangeAsync().ConfigureAwait(false);
            var item = items.FirstOrDefault(x => x.Id.Equals(objectToStore.Id));

            if (item != null)
            {
                items[items.IndexOf(item)] = objectToStore;
            }
            else
            {
                items.Add(objectToStore);
            }

            await StoreRangeAsync(items).ConfigureAwait(false);

            return true;
        }

        /// <summary>
        /// Deletes an object with a certain Id
        /// </summary>
        /// <param name="id">The id of the object that will be deleted</param>
        /// <returns>An await able Task with the success as result</returns>
        public async Task<bool> DeleteAsync(TId id)
        {
            // todo: Seek actual id from disk instead of searching the list
            // todo: Actual delete at correct index and moving the bytes
            var items = await GetRangeAsync().ConfigureAwait(false);
            var item = items.FirstOrDefault(x => x.Id.Equals(id));

            if (item != null)
            {
                items.Remove(item);
                await StoreRangeAsync(items).ConfigureAwait(false);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Base <see cref="JsonSerializerSettings"/> setting <see cref="ReferenceLoopHandling"/> to Ignore.
        /// </summary>
        /// <returns>An instance of <see cref="JsonSerializerSettings"/></returns>
        protected virtual JsonSerializerSettings JsonSerializerSettings()
        {
            return new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        }
    }
}