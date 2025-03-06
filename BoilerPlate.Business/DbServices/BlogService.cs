using BoilerPlate.Business.DbServices.Base;
using BoilerPlate.Business.StorageServices;
using BoilerPlate.DAL.Context;
using BoilerPlate.Entity.Dto.Blog;
using BoilerPlate.Entity.Dto.Files;
using BoilerPlate.Entity.Entities.Concrete;
using BoilerPlate.Entity.Entities.Concrete.File;
using IResult = BoilerPlate.Entity.Results.Abstract.IResult;
using BoilerPlate.Entity.Results.ComplexTypes;
using BoilerPlate.Entity.Results.Concrete;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Business.DbServices
{
    public class BlogService : BaseService<Blog>
    {
        private readonly AppDbContext _context;
        private readonly IStorage _storage;

        public BlogService(AppDbContext context, IStorage storage) : base(context)
        {
            _context = context;
            _storage = storage;
        }

        public async Task<IResult> AddBlogAsync(AddBlogDto blogDto)
        {
            try
            {
                // Blog entity'sini oluştur
                var blog = blogDto.Adapt<Blog>();

                // Kategorileri ekle
                blog.BlogCategories = blogDto.CategoryIds.Select(categoryId => new BlogCategory
                {
                    CategoryId = categoryId
                }).ToList();

                // Blog'u ekle
                await _context.Blogs.AddAsync(blog);
                await _context.SaveChangesAsync();

                // Resimleri kaydet
                if (blogDto.Images != null && blogDto.Images.Any())
                {
                    var formFiles = new FormFileCollection();
                    foreach (var image in blogDto.Images)
                        formFiles.Add(image);

                    var uploadResults = await _storage.UploadAsync("blog-images", formFiles);

                    foreach (var uploadResult in uploadResults)
                    {
                        var image = new ImageFile
                        {
                            FileName = uploadResult.FileName,
                            FilePath = uploadResult.Path,
                            FileSize = formFiles.First(f => f.FileName == uploadResult.FileName).Length,
                            ContentType = formFiles.First(f => f.FileName == uploadResult.FileName).ContentType,
                            FileType = "Image"
                        };

                        await _context.ImageFiles.AddAsync(image);
                        await _context.BlogImages.AddAsync(new BlogImage
                        {
                            BlogId = blog.Id,
                            Image = image
                        });
                    }

                    await _context.SaveChangesAsync();
                }

                return new Result(ResultStatus.Success, "Blog başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, "Blog eklenirken bir hata oluştu.", ex);
            }
        }

        public async Task<IResult> UpdateBlogAsync(UpdateBlogDto blogDto)
        {
            try
            {
                var blog = await _context.Blogs
                    .Include(b => b.BlogCategories)
                    .Include(b => b.BlogImages)
                    .ThenInclude(bi => bi.Image)
                    .FirstOrDefaultAsync(b => b.Id == blogDto.Id);

                if (blog == null)
                    return new Result(ResultStatus.Error, "Blog bulunamadı.");

                // Blog bilgilerini güncelle
                blog.Title = blogDto.Title;
                blog.Description = blogDto.Description;

                // Kategorileri güncelle
                _context.BlogCategories.RemoveRange(blog.BlogCategories);
                blog.BlogCategories = blogDto.CategoryIds.Select(categoryId => new BlogCategory
                {
                    BlogId = blog.Id,
                    CategoryId = categoryId
                }).ToList();

                // Silinecek resimleri kaldır
                if (blogDto.DeletedImageIds != null && blogDto.DeletedImageIds.Any())
                {
                    var deletedImages = blog.BlogImages
                        .Where(bi => blogDto.DeletedImageIds.Contains(bi.ImageId))
                        .ToList();

                    foreach (var deletedImage in deletedImages)
                    {
                        // Storage'dan resmi sil
                        await _storage.DeleteAsync(deletedImage.Image.FilePath);

                        _context.BlogImages.Remove(deletedImage);
                        _context.ImageFiles.Remove(deletedImage.Image);
                    }
                }

                // Yeni resimleri ekle
                if (blogDto.NewImages != null && blogDto.NewImages.Any())
                {
                    var formFiles = new FormFileCollection();
                    foreach (var image in blogDto.NewImages)
                        formFiles.Add(image);

                    var uploadResults = await _storage.UploadAsync("blog-images", formFiles);

                    foreach (var uploadResult in uploadResults)
                    {
                        var image = new ImageFile
                        {
                            FileName = uploadResult.FileName,
                            FilePath = uploadResult.Path,
                            FileSize = formFiles.First(f => f.FileName == uploadResult.FileName).Length,
                            ContentType = formFiles.First(f => f.FileName == uploadResult.FileName).ContentType,
                            FileType = "Image"
                        };

                        await _context.ImageFiles.AddAsync(image);
                        await _context.BlogImages.AddAsync(new BlogImage
                        {
                            BlogId = blog.Id,
                            Image = image
                        });
                    }
                }

                await _context.SaveChangesAsync();
                return new Result(ResultStatus.Success, "Blog başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, "Blog güncellenirken bir hata oluştu.", ex);
            }
        }

        public async Task<IResult> DeleteBlogAsync(int blogId)
        {
            try
            {
                var blog = await _context.Blogs
                    .Include(b => b.BlogImages)
                    .ThenInclude(bi => bi.Image)
                    .FirstOrDefaultAsync(b => b.Id == blogId);

                if (blog == null)
                    return new Result(ResultStatus.Error, "Blog bulunamadı.");

                // Resimleri storage'dan sil
                foreach (var blogImage in blog.BlogImages)
                {
                    await _storage.DeleteAsync(blogImage.Image.FilePath);
                    _context.ImageFiles.Remove(blogImage.Image);
                }

                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();

                return new Result(ResultStatus.Success, "Blog başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, "Blog silinirken bir hata oluştu.", ex);
            }
        }
    }
}
