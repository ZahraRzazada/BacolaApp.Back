using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Bacola.App.ViewModels;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Service.Responses;
using Bacola.Service.Services.Implementations;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bacola.App.Controllers;

public class ShopController : Controller
{
    readonly IProductService _productService;
    readonly IProductRepository _productRepository;
    readonly IOrderService _orderService;
    readonly IWishlistService _wishlistService;
    readonly IBasketService _basketService;
    public ShopController(IProductService productService, IBasketService basketService, IWishlistService wishlistService, IOrderService orderService, IProductRepository productRepository)
    {
        _productService = productService;
        _basketService = basketService;
        _wishlistService = wishlistService;
        _orderService = orderService;
        _productRepository = productRepository;
    }
    public async Task<IActionResult> Index(ShopViewModel model)
    {
        ShopViewModel vm = new()
        {
            Products = await _productService.GetAllAsync()
        };
        return View(vm);


    }

    [HttpPost]
    public async Task<IActionResult> Filter(ProductFilterDto model)
    {
        List<Product> product = default;

        if (model.brandIds != null && model.categoryIds != null)
        {
            product = await _productRepository.GetQuery(p => model.brandIds.Contains(p.BrandId) && model.categoryIds.Contains(p.CategoryId)).ToListAsync();
        }
        else if (model.brandIds != null)
        {
            product = await _productRepository.GetQuery(p => model.brandIds.Contains(p.BrandId)).ToListAsync();
        }
        else if (model.categoryIds != null)
        {
            product = await _productRepository.GetQuery(p => model.categoryIds.Contains(p.CategoryId )|| model.categoryIds.Contains(p.Id)).ToListAsync();
        }
        if(model.fromPrice != null && model.toPrice != null)
        {
            product = await _productRepository.GetQuery(p => p.Price >= model.fromPrice &&  p.Price <= model.toPrice ).ToListAsync();
        }
        else if (model.fromPrice != null)
        {
            product = await _productRepository.GetQuery(p => p.Price >= model.fromPrice).ToListAsync();
        }
        else if (model.toPrice != null)
        {
            product = await _productRepository.GetQuery(p => p.Price <= model.toPrice).ToListAsync();
        }

        return PartialView("_ShopPartial", product);

    }
   
 

    public async Task<IActionResult> Detail(int id)
    {
        var res = await _productService.GetAsync(id);

        return View(res.Data);
    }

    public async Task<IActionResult> AddBasket(int id, int? count = 1)
    {
       var result = await _basketService.AddBasket(id, count);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("", result.Message);
            return View();
        }
        return RedirectToAction("index", "shop");
    }

    public async Task<IActionResult> Basket()
    {
        var basketResponse = await _basketService.GetBasket();

        if (basketResponse.IsSuccess)
        {
            ViewBag.BasketData = basketResponse.Data;
            return View(basketResponse);
        }
        else
        {

            return View("Error");
        }
    }
    public async Task<IActionResult> RemoveAllBasket()
    {
        var result = await _basketService.RemoveAllBasketItems();
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("", result.Message);
            return RedirectToAction("basket", "shop");

        }
        return RedirectToAction("basket", "shop");
    }

    public async Task<IActionResult> DecreaseCount(int id)
    {
        await _basketService.DecreaseCount(id);
        return RedirectToAction(nameof(Basket));
    }
    public async Task<IActionResult> RemoveBasketItem(int id)
    {
        var result=await _basketService.DecreaseCount(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("", result.Message);
            return View();
        }
        return RedirectToAction("index", "shop");
    }

    public async Task<IActionResult> RemoveAllWishlist()
    {
        var result = await _wishlistService.RemoveAllWishlist();
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("", result.Message);
            return RedirectToAction("wishlist", "shop");

        }
        return RedirectToAction("wishlist", "shop");
    }
    public async Task<IActionResult> RemoveWishlistItem(int id)
    {
        var result = await _wishlistService.RemoveWishlistItemAsync(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("", result.Message);
            return RedirectToAction("wishlist", "shop");

        }
        return RedirectToAction("wishlist", "shop");
    }


    public async Task<IActionResult> AddWishlist(int id)
    {
       var result= await _wishlistService.AddWishlist(id);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("", result.Message);
            return StatusCode(404);

        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Wishlist()
    {
        return View(await _wishlistService.GetWishlist()); ;
    }

    public async Task<IActionResult> IncreaseCount(int id)
    {
        var result = await _basketService.AddBasket(id, null);
        return RedirectToAction(nameof(Basket));
    }

    [Authorize]
    public async Task<IActionResult> Checkout()
    {
        var basketResponse = await _basketService.GetBasket();

        if (basketResponse.IsSuccess)
        {
            ViewBag.BasketData = basketResponse.Data; 
            return View(); 
        }
        else
        {

            return View("Error"); 
        }
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Checkout(OrderPostDto dto)
    {
        await _orderService.CreateAsync(dto);
        return RedirectToAction(nameof(Index));
    }
    [Authorize]
    public async Task<IActionResult> OrderTracking()
    {
        return View();
    }
    [Authorize]
    public async Task<IActionResult> OrderTrackingDetail(int id)
    {
        return View(await _orderService.Get(id));
    }
    [HttpPost]
    public async Task<IActionResult> ApplyCoupon(string code, CustomResponse<BasketGetDto> dto)
    {
        var basketResponse = await _basketService.GetBasket();

        if (!basketResponse.IsSuccess)
        {
            ViewBag.BasketData = basketResponse.Data;
            return View(basketResponse);
        }
        dto.Data.basketItems = basketResponse.Data.basketItems;
        
        var response = await _basketService.ApplyCoupon(code, dto);
        if (!response.IsSuccess)
        {
            TempData["ErrorMessage"] = response.Message;
            return RedirectToAction("Basket", "Shop");
        }
        else
        {
            TempData["Succes"] = response.Message;
            return RedirectToAction("Basket", "Shop");
        }
    }

    //[HttpPost]
    //public async Task<IActionResult> FilterProducts(ProductFilterDto dto)
    //{
    //    var res = await _productService.GetFilteredProducts(dto);
    //    return View(res.Items);

    //}

    public async Task<IActionResult> SearchProduct(string search)
    {
        
  
            var results = await _productService.Search(search);
            return Json(results);
        
    }
    [HttpPost]
    public async Task<IActionResult> CategoryFilter(ShopViewModel categoryId)
    {
        //var a = categoryId.Filter.categoryId;
        //var res = await _productService.FilterByCategory(categoryId);
        return View();
    }

}

