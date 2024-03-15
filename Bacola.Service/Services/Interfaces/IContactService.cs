using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
	public interface IContactService
	{
        public Task<IEnumerable<ContactGetDto>> GetAllAsync(int page = 1);

        public Task<CustomResponse<Contact>> CreateAsync(ContactPostDto dto);

        public Task<CustomResponse<Contact>> RemoveAsync(int id);
    }
}

