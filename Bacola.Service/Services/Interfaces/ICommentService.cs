using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
    public interface ICommentService
	{
        Task<CustomResponse<ParentComment>> CreateCommentAsync(ParentCommentDto dto);
        Task<CustomResponse<Reply>> CreateReplyAsync(ReplyDto dto);
        Task<IEnumerable<ParentCommentDto>> GetAllCommentsAsync();
        //Task<IEnumerable<ParentCommentDto>> GetAllRepliesAsync();
    }
}

