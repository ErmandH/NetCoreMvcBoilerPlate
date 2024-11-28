using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Business.StorageServices
{
    public interface IStorageService
    {
        public string StorageName { get; }
    }
}