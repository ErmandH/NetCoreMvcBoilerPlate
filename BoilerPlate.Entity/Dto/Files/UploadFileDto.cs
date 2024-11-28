using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Entity.Dto.Files
{
    public class UploadFileDto
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
    }
}
