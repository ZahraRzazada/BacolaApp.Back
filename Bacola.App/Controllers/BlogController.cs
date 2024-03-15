using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bacola.Core.DTOS;
using Bacola.Data.Contexts;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bacola.App.Controllers
{
    public class BlogController : Controller
    {
        readonly BacolaDbContext _context;
        readonly IBlogService _blogService;
        public BlogController(BacolaDbContext context, IBlogService blogService)
        {
            _context = context;
            _blogService = blogService;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<BlogGetDto> blogGetDtos = await _blogService.GetAllAsync();
            ViewBag.Tags = _context.Tags.Where(x => !x.IsDeleted)
           .Include(x => x.TagBlogs)
           .ThenInclude(x => x.Blog)
           .Select(tag => new
           {
               Name = tag.Name
           }).AsNoTrackingWithIdentityResolution();
            ViewBag.Categories = _context.Categories.Where(x => !x.IsDeleted).Select(c => new CategoryGetDto { Name = c.Name }).AsNoTrackingWithIdentityResolution();
            return View(blogGetDtos);
        }
        public async Task<IActionResult> Detail(int id)
        {
            ViewBag.Tags = _context.Tags.Where(x => !x.IsDeleted)
          .Include(x => x.TagBlogs)
          .ThenInclude(x => x.Blog)
          .Select(tag => new TagGetDto
          {
              Name = tag.Name
          }).AsNoTrackingWithIdentityResolution();
            ViewBag.Categories = _context.Categories.Where(x => !x.IsDeleted).Select(c => new CategoryGetDto { Name = c.Name }).AsNoTrackingWithIdentityResolution();
            var blogGetDto = await _blogService.GetAsync(id);

            return View(blogGetDto.Data);
        }
    }
}

