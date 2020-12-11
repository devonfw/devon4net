using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Excalibur.Shared.Storage;
using MyThaiStar.Core.Business.Interfaces;
using Newtonsoft.Json;
using XLabs.Ioc;

namespace MyThaiStar.Core.Business
{
    public class StorableObject<T> : IStorableObject<T> where T : class
    {
        public List<T> StorableInstance { get; set; }
        private IStorageService StorageService { get; set; }
        private readonly string _fileName = $"{typeof(T).Name}Stored.data";
        private string FileFullPath { get; }

        public StorableObject(string fileFullPath = "")
        {
            try
            {
                FileFullPath = fileFullPath;
                StorageService = Resolver.Resolve<IStorageService>();
                StorableInstance = GetItemsFromStorage().Result;
            }
            catch (Exception ex)
            {
                var msg = $"{ex.Message} : {ex.InnerException}";
            }
        }

        private async Task<List<T>> GetItemsFromStorage()
        {
            var result = new List<T>();
            try
            {
                if (!StorageService.Exists(FileFullPath, _fileName)) return result;

                var storedItems = await StorageService.ReadAsTextAsync(FileFullPath, _fileName).ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(storedItems))
                {
                    //result = JsonConvert.DeserializeObject<List<T>>(storedItems);
                    var b= JsonConvert.DeserializeObject<List<Observable.ShoppingCartItem>>(storedItems);
                    result = JsonConvert.DeserializeObject<List<T>>(storedItems);
                }
            }
            catch (Exception ex)
            {
                StorageService.DeleteFile(FileFullPath, _fileName);
                var msg = $"{ex.Message} : {ex.InnerException}";
            }
            return result;          
        }

        private async Task<bool> StoreItems()
        {

            try
            {


                var configAsString = JsonConvert.SerializeObject(StorableInstance);
                var b = JsonConvert.DeserializeObject<List<Observable.ShoppingCartItem>>(configAsString);


                if (StorageService.Exists(FileFullPath, _fileName))
                {
                    StorageService.DeleteFile(FileFullPath, _fileName);
                }

                await StorageService.StoreAsync(FileFullPath, _fileName, configAsString).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var msg = $"{ex.Message} : {ex.InnerException}";                
            }

            return true;
        }

        public async Task AddItem(T item)
        {
            StorableInstance.Add(item);
            await StoreItems();
        }

        public async Task DeleteItem(T item)
        {
            StorableInstance.Remove(item);
            await StoreItems();
        }

        public List<T> GetItems()
        {
            return StorableInstance;
        }

    }
}
