using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoilerPlate.Entity.Dto.Files;

namespace BoilerPlate.Business.StorageServices.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        //public async Task DeleteAsync(string path, string fileName)
        //    => await Task.Run(() => File.Delete($"{path}\\{fileName}"));
        public async Task DeleteAsync(string path)
        {
            string deletePath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            await Task.Run(() => File.Delete(deletePath));
        }

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }


        async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }
        }
        public async Task<List<UploadFileDto>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<UploadFileDto> datas = new();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName);

                await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add(new UploadFileDto
                {
                    FileName = fileNewName,
                    Path = $"{path}\\{fileNewName}",
                    Extension = Path.GetExtension(file.FileName)
                });
            }

            return datas;
        }
    }
}