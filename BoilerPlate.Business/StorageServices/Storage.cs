using BoilerPlate.Business.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Business.StorageServices
{
    public class Storage
    {
        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName)
        {
            return await Task.Run<string>(() =>
            {
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                string extension = Path.GetExtension(fileName);
                string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
                bool fileExists = false;
                int fileIndex = 0;
                do
                {
                    // eger dosya zaten varsa sonuna -1 koy eger -1 varsa -2 koy gibi kac kere duplicate ise devam ettiriyorum islemi
                    if (File.Exists($"{pathOrContainerName}\\{newFileName}"))
                    {
                        fileExists = true;
                        fileIndex++;
                        newFileName = $"{NameOperation.CharacterRegulatory(oldName + "-" + fileIndex)}{extension}";
                    }
                    else
                    {
                        fileExists = false;
                    }
                } while (fileExists);

                return newFileName;
            });
        }
    }
}