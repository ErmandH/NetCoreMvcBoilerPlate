
using BoilerPlate.Entity.Dto.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Business.StorageServices
{
    public interface IStorage
    {
        Task<List<UploadFileDto>> UploadAsync(string pathOrContainerName, IFormFileCollection files);
        Task DeleteAsync(string pathOrContainerName);
        List<string> GetFiles(string pathOrContainerName);
    }
}