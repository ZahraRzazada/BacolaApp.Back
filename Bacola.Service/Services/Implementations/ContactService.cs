using System;
using AutoMapper;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Repositories;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;

namespace Bacola.Service.Services.Implementations
{
    public class ContactService : IContactService
    {
        readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        readonly IMapper _mapper;

        public async Task<CustomResponse<Contact>> CreateAsync(ContactPostDto dto)
        {
            Contact contact = _mapper.Map<Contact>(dto);
            await _contactRepository.AddAsync(contact);
            await _contactRepository.SaveChangesAsync();
            return new CustomResponse<Contact> { IsSuccess = true, Message = $"OK", Data = contact };
        }

        public async Task<IEnumerable<ContactGetDto>> GetAllAsync(int page = 1)
        {
            var query = _contactRepository.GetQuery(x => x.IsDeleted == false);
            IEnumerable<ContactGetDto> contacts = _mapper.Map<IEnumerable<ContactGetDto>>(query);
            return contacts;
        }

        public async Task<CustomResponse<Contact>> RemoveAsync(int id)
        {
            Contact? Contact = await _contactRepository.GetAsync(x => !x.IsDeleted && x.Id == id);

            if (Contact == null)
            {
                return new CustomResponse<Contact> { IsSuccess = false, Message = "This contact doesnt exist" };
            }
            Contact.IsDeleted = true;
            await _contactRepository.UpdateAsync(Contact);
            await _contactRepository.SaveChangesAsync();
            return new CustomResponse<Contact> { IsSuccess = true, Message = $"Contact is removed successfully", Data = Contact };
        }
    }
}

