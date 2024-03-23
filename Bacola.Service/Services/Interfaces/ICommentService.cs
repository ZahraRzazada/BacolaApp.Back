using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.Service.Services.Interfaces
{
    public interface ICommentService
	{
        Task<CustomResponse<Coment>> CreateCommentAsync(CommentPostDto dto);
        //Task<CustomResponse<Reply>> CreateReplyAsync(ReplyDto dto);
        Task<IEnumerable<CommentGetDto>> GetAllCommentsAsync();
        //Task<IEnumerable<ReplyDto>> GetAllRepliesAsync();
    }
}

