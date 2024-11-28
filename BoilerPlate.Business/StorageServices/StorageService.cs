
using BoilerPlate.Entity.Dto.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Business.StorageServices
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;
        // services.AddStorage<Azure>() dediysem buraya Azure storage gelicek
        // StorageService classi bizim storage icin kullanilacak olan genel servisimiz yani ister azure olsuni ister local olsun biz bu servicei kullanicaz
        // hangisinin kullanilacagi AddStorage da belirleniyor
        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }

        public async Task DeleteAsync(string pathOrContainerName)
            => await _storage.DeleteAsync(pathOrContainerName);

        public List<string> GetFiles(string pathOrContainerName)
            => _storage.GetFiles(pathOrContainerName);


        public Task<List<UploadFileDto>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
            => _storage.UploadAsync(pathOrContainerName, files);
    }
}