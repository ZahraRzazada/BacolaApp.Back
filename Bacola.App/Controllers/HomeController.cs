using System.Diagnostics;
using Bacola.App.ViewModels;
using Bacola.Core.DTOS;
using Bacola.Data.Contexts;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Bacola.App.Controllers;

public class HomeController : Controller
{
    readonly BacolaDbContext _context;
    readonly IHomeSliderService _sliderService;
    readonly IProductService _productService;
    readonly IBasketService _basketService;
    readonly IWishlistService _wishlistService;


    public HomeController(IHomeSliderService sliderService, IBlogService blogService, IProductService productService, IBasketService basketService, IWishlistService wishlistService, BacolaDbContext context)
    {
        _sliderService = sliderService;
        _blogService = blogService;
        _productService = productService;
        _basketService = basketService;
        _wishlistService = wishlistService;
        _context = context;
    }

    readonly IBlogService _blogService;
    public async Task<IActionResult> Index()
    {
        HomeViewModel homeViewModel = new HomeViewModel();
        homeViewModel.Blogs = await _blogService.GetAllAsync();
        homeViewModel.Sliders = await _sliderService.GetAllAsync();
        var paginatedResponse = await _productService.GetAllAsync();
        IEnumerable<ProductGetDto> products = paginatedResponse.Items;

        homeViewModel.Products = products;

        var basketJson = Request.Cookies["basket"];

        if (User.Identity.IsAuthenticated)
        {
            if (basketJson != null)
            {
                List<BasketPostDto> basketDtos = JsonConvert.DeserializeObject<List<BasketPostDto>>(basketJson);

                foreach (var item in basketDtos)
                {
                    await _basketService.AddBasket(item.Id, item.Count);
                }

                Response.Cookies.Delete("basket");
            }
        }
        var wishlistJson = Request.Cookies["wishlist"];

        if (User.Identity.IsAuthenticated)
        {
            if (wishlistJson != null)
            {
                List<WishlistPostDto> wishlistPostDtos = JsonConvert.DeserializeObject<List<WishlistPostDto>>(wishlistJson);

                foreach (var item in wishlistPostDtos)
                {
                    await _wishlistService.AddWishlist(item.Id);
                }

                Response.Cookies.Delete("wishlist");
            }
        }
        return View(homeViewModel);
    }
    public async Task<IActionResult> Subscribe(string EmailAddress, string? returnUrl)
    {
        var existemail = await _context.Subscribe.AnyAsync(x => x.EmailAddress.ToLower() == EmailAddress.ToLower());
        if(!existemail)
        {
            await _context.Subscribe.AddAsync(new Core.Entities.Subscribe { EmailAddress = EmailAddress });
            await _context.SaveChangesAsync();
        }
        if (returnUrl!=null)
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction(nameof(Index));
    }
    public IActionResult ErrorPage(string error)
    {
        return View(model:error);
    }
}

