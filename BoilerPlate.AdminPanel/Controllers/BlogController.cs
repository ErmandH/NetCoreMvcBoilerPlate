using BoilerPlate.Business.DbServices;
using BoilerPlate.DAL.Context;
using BoilerPlate.Entity.Dto.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.AdminPanel.Controllers
{
	[Authorize]
	[Route("blog")]
	public class BlogController : Controller
	{
		private readonly BlogService _blogService;
		private readonly AppDbContext _context;

		public BlogController(BlogService blogService, AppDbContext context)
		{
			_blogService = blogService;
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var blogs = await _context.Blogs
				.Include(b => b.BlogCategories)
					.ThenInclude(bc => bc.Category)
				.Include(b => b.BlogImages)
					.ThenInclude(bi => bi.Image)
				.ToListAsync();

			return View(blogs);
		}

		[HttpGet("add")]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost("add")]
		public async Task<IActionResult> Create([FromForm] AddBlogDto blogDto)
		{
			if (!ModelState.IsValid)
				return Json(new { success = false, message = "Form verileri geçerli değil" });

			var result = await _blogService.AddBlogAsync(blogDto);
			return Json(new { success = result.ResultStatus == Entity.Results.ComplexTypes.ResultStatus.Success, message = result.Message });
		}

		[HttpGet("get-categories")]
		public async Task<IActionResult> GetCategories(string search = "")
		{
			var categories = await _context.Categories
				.Where(c => string.IsNullOrEmpty(search) || c.Name.Contains(search))
				.Select(c => new { id = c.Id, name = c.Name })
				.ToListAsync();

			return Json(categories);
		}

		[HttpPost("delete")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _blogService.DeleteBlogAsync(id);
			return Json(new { success = result.ResultStatus == Entity.Results.ComplexTypes.ResultStatus.Success, message = result.Message });
		}
	}
}