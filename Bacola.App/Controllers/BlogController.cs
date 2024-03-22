using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bacola.App.ViewModels;
using Bacola.Core.DTOS;
using Bacola.Data.Contexts;
using Bacola.Service.Services.Implementations;
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
        readonly ICommentService _commentService;
        public BlogController(BacolaDbContext context, IBlogService blogService, ICommentService commentService)
        {
            _context = context;
            _blogService = blogService;
            _commentService = commentService;
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
            BlogViewModel blogViewModel = new BlogViewModel();
            ViewBag.Tags = _context.Tags.Where(x => !x.IsDeleted)
          .Include(x => x.TagBlogs)
          .ThenInclude(x => x.Blog)
          .Select(tag => new TagGetDto
          {
              Name = tag.Name
          }).AsNoTrackingWithIdentityResolution();
            ViewBag.Categories = _context.Categories.Where(x => !x.IsDeleted).Select(c => new CategoryGetDto { Name = c.Name }).AsNoTrackingWithIdentityResolution();
            blogViewModel.BlogGetDto= await _blogService.GetAsync(id);

            //blogViewModel.Comments = await _commentService.GetAllCommentsAsync();
            //blogViewModel.Replies = await _commentService.GetAllRepliesAsync();
            return View(blogViewModel);
        }
    
        [HttpPost]
        public async Task<IActionResult> AddComment(ParentCommentDto dto)
        {
            if (!ModelState.IsValid)
            {
                if (dto.Replies == null)
                {
                    dto.Replies = new List<ReplyDto>();
                    return BadRequest(ModelState);
                }
     
            }
            var response = await _commentService.CreateCommentAsync(dto);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }
            return RedirectToAction("Detail","Blog" , new { id = dto.BlogId});
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(ReplyDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var response = await _commentService.CreateReplyAsync(dto);
            if (!response.IsSuccess)
            {
                ModelState.AddModelError("", response.Message);
                return View();
            }
            //var blogId = HttpContext.Session.GetInt32("CurrentBlogId");
            return RedirectToAction("Detail", "Blog", new { id = dto.BlogId });
        }
    }
}

