using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Services.Implementations;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bacola.App.Controllers
{
    public class ShopController : Controller
    {
        readonly IProductService _productService;
        readonly IOrderService _orderService;
        readonly IWishlistService _wishlistService;
        readonly IBasketService _basketService;
        public ShopController(IProductService productService, IBasketService basketService, IWishlistService wishlistService, IOrderService orderService)
        {
            _productService = productService;
            _basketService = basketService;
            _wishlistService = wishlistService;
            _orderService = orderService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _productService.GetAllAsync());
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
            return RedirectToAction("index", "shop");
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
      
        public async Task<IActionResult> ApplyCoupon(string code,int couponId,BasketGetDto dto)
        {
            var response = await _basketService.ApplyCoupon(code,couponId, dto);
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
    }
}

