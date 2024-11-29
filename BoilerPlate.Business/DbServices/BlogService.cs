using BoilerPlate.Business.DbServices.Base;
using BoilerPlate.DAL.Context;
using BoilerPlate.Entity.Dto.Blog;
using BoilerPlate.Entity.Entities.Concrete;
using BoilerPlate.Entity.Results.Abstract;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Business.DbServices
{
    public class BlogService : BaseService<Blog>
    {
        public BlogService(AppDbContext context) : base(context)
        {
        }

        public async Task<IResult> AddBlogAsync(AddBlogDto blog) 
        {
            var model = blog.Adapt<Blog>();
            var addResult = await AddAsync(model);
            return addResult;
        }
    }
}
