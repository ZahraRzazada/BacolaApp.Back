using System;
using AutoMapper;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Enums;
using Bacola.Core.Repositories;
using Bacola.Service.Helpers;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bacola.Service.Services.Implementations
{
    public class OrderService : IOrderService
    {
        readonly IOrderRepository _orderRepository;
        readonly IHttpContextAccessor _http;
        readonly UserManager<AppUser> _userManager;
        readonly IBasketRepository _basketRepository;
        readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IHttpContextAccessor http, UserManager<AppUser> userManager, IBasketRepository basketRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _http = http;
            _userManager = userManager;
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task Accept(int id)
        {
            Order order = await _orderRepository.GetAsync(x => x.Id == id);
            order.Status = (int)OrderStatus.Accept;
            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();
        }
        public async Task Reject(int id)
        {
            Order order = await _orderRepository.GetAsync(x => x.Id == id);
            order.Status = (int)OrderStatus.Reject;
            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task Success(int id)
        {
            Order order = await _orderRepository.GetAsync(x => x.Id == id);
            order.Status = (int)OrderStatus.Success;
            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(OrderPostDto dto)
        {
            string userName = _http.HttpContext.User.Identity.Name;

            AppUser appUser = await _userManager.FindByNameAsync(userName);
            Order order = new Order
            {
                AppUser = appUser,
                Text=dto.Text,
                Address=dto.Address,
                PhoneNumber=dto.PhoneNumber,
                Status = 0,
                OrderItems = new List<OrderItem>()
            };
            var baskets = await _basketRepository.GetQuery(x => x.IsDeleted == false)
          .Include(x => x.BasketItems)
              .ThenInclude(item => item.Product) 
          .FirstOrDefaultAsync();

            foreach (var item in baskets.BasketItems.Where(x => x.IsDeleted == false))
            {
                order.OrderItems.Add(new OrderItem
                {
                    Product=item.Product,
                    Count = item.Count,
                    Order = order,
                    ProductId = item.ProductId,
                });
            }
            baskets.IsDeleted = true;
            await _basketRepository.UpdateAsync(baskets);
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            string orderItemsInfo = "";
            foreach (var orderItem in order.OrderItems)
            {
                orderItemsInfo += $"Product: {orderItem.Product.Title}, Quantity: {orderItem.Count}, Price: {orderItem.Product.Price}\n";
            }

            Helper.SendMessageToTelegram($"New Order - Id: {order.Id}\n" +
    $"Status: {order.Status}\n" +
    $"OrderItems:\n{orderItemsInfo}");
        }

        public async Task<OrderGetDto> Get(int id)
        {
            var query = await _orderRepository.GetQuery(x => x.IsDeleted == false && x.Id == id)
               .Include(x => x.AppUser)
               .Include(x => x.OrderItems)
               .ThenInclude(x => x.Product)
               .FirstOrDefaultAsync();

            OrderGetDto orderGetDto = new OrderGetDto
            {
                AppUser = query.AppUser,
                Status = query.Status,
                Address=query.Address,
                Text=query.Text,
                PhoneNumber=query.PhoneNumber,
                Id = query.Id,
                OrderItems = query.OrderItems.Select(x => new OrderItemGetDto
                {
                    Count = x.Count,
                    Product = new ProductGetDto { Title = x.Product.Title, Price = x.Product.Price }
                }).ToList()
            };
            return orderGetDto;
        }

        public async Task<IEnumerable<OrderGetDto>> GetAll()
        {
            var query = _orderRepository.GetQuery(x => x.IsDeleted == false)
                                 .Include(o => o.AppUser);
            var orders = await query.ToListAsync();
            var orderDtos = orders.Select(order => new OrderGetDto
            {
                Id = order.Id,
                UserEmail = order.AppUser?.Email,
                Status = order.Status,
                Address=order.Address,
                OrderTracking = order.OrderTracking, 

            });
            return orderDtos;

        }    


        public async Task PreProduction(int id)
        {
            Order order = await _orderRepository.GetAsync(x => x.Id == id);
            order.OrderTracking = (int)OrderTracking.PreProduction;
            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task InProduction(int id)
        {
            Order order = await _orderRepository.GetAsync(x => x.Id == id);
            order.OrderTracking = (int)OrderTracking.InProduction;
            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task Shipped(int id)
        {
            Order order = await _orderRepository.GetAsync(x => x.Id == id);
            order.OrderTracking = (int)OrderTracking.Shipped;
            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task Delivered(int id)
        {
            Order order = await _orderRepository.GetAsync(x => x.Id == id);
            order.OrderTracking = (int)OrderTracking.Delivered;
            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();
        }
    }
}
