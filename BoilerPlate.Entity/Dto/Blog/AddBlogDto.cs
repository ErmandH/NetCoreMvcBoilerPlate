﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoilerPlate.Entity.Dto.Blog
{
    public class AddBlogDto
    {
        [Required(ErrorMessage = "Başlık alanı zorunludur")]
        [MinLength(3, ErrorMessage = "Başlık en az 3 karakter olmalıdır")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik alanı zorunludur")]
        [MinLength(10, ErrorMessage = "İçerik en az 10 karakter olmalıdır")]
        public string Description { get; set; }

        [Required(ErrorMessage = "En az bir kategori seçilmelidir")]
        public List<int> CategoryIds { get; set; }

        // Resim dosyaları opsiyonel
        public List<IFormFile> Images { get; set; }
    }
}
