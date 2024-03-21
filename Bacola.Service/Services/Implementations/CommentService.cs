using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Migrations;
using Bacola.Data.Repositories;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Bacola.Service.Services.Implementations
{
    public class CommentService : ICommentService
    {
        readonly IParentCommentRepository _commentRepositoy;

        public CommentService(IParentCommentRepository commentRepositoy, IReplyRepository replyRepositoy)
        {
            _commentRepositoy = commentRepositoy;
            _replyRepositoy = replyRepositoy;
        }

        readonly IReplyRepository _replyRepositoy;

        public async Task<CustomResponse<ParentComment>> CreateCommentAsync(ParentCommentDto dto)
        {
            if (dto.Text == null)
            {
                
                return new CustomResponse<ParentComment> { IsSuccess = false, Message = "Please fill text area" };
            }
            dto.Replies = dto.Replies ?? new List<ReplyDto>();
            ParentComment comment = new ParentComment
            {

                Text = dto.Text,
                BlogId = dto.BlogId,
                AspNetUsersId = dto.AspNetUsersId
                 
            };
            //foreach (var replyDto in dto.Replies)
            //{
            //    Reply reply = new Reply
            //    {
            //        Text = replyDto.Text,
            //    };
            //    comment.Replies.Add(reply);
            //}
            await _commentRepositoy.AddAsync(comment);
            await _commentRepositoy.SaveChangesAsync();
            return new CustomResponse<ParentComment> { IsSuccess = true, Message = "Comment is created successfully", Data = comment };
        }
        public async Task<CustomResponse<Reply>> CreateReplyAsync(ReplyDto dto)
        {
            if (dto.Text == null)
            {
                return new CustomResponse<Reply> { IsSuccess = false, Message = "Please fill text area" };
            }

            ParentComment parentComment = await _commentRepositoy.GetAsync(x => x.Id == dto.ParentCommentId);
            if (parentComment == null)
            {
                return new CustomResponse<Reply> { IsSuccess = false, Message = "Parent comment not found" };
            }
            var reply = new Reply
            {
                Text = dto.Text,
                ParentCommentId = dto.ParentCommentId,
                AspNetUsersId = dto.AspNetUsersId
            };
            parentComment.Replies.Add(reply);

            await _replyRepositoy.AddAsync(reply);
            await _replyRepositoy.SaveChangesAsync();

            return new CustomResponse<Reply> { IsSuccess = true, Message = "Reply is created successfully", Data = reply };

        }

        public async Task<IEnumerable<ParentCommentDto>> GetAllCommentsAsync()
        {
          
            IEnumerable<ParentCommentDto> comments = await _commentRepositoy.GetQuery(x => !x.IsDeleted)
                .AsNoTrackingWithIdentityResolution().Select(x => new ParentCommentDto { AspNetUsersId = x.AspNetUsersId, Id = x.Id, CreatedAt=x.CreatedAt,BlogId = x.BlogId, Text = x.Text, Replies = new List<ReplyDto>() }).ToListAsync();
            return comments;
        }
    }
}

