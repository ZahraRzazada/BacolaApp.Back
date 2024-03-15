
using System;
using Bacola.Core.DTOS;

namespace Bacola.Service.Services.Interfaces
{
	public interface IOrderService
	{
        public Task CreateAsync(OrderPostDto dto);
        public Task<IEnumerable<OrderGetDto>> GetAll();
        public Task<OrderGetDto> Get(int id);
        public Task Accept(int id);
        public Task Reject(int id);
        public Task Success(int id);
        public Task PreProduction(int id);
        public Task InProduction(int id);
        public Task Shipped(int id);
        public Task Delivered(int id);
    }
}

