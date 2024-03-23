using System;
using System.Security.Claims;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Core.Repositories;
using Bacola.Data.Migrations;
using Bacola.Data.Repositories;
using Bacola.Service.Responses;
using Bacola.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Bacola.Service.Services.Implementations
{
    public class CommentService : ICommentService
    {
        readonly IComentRepository _commentRepositoy;
        readonly IBlogRepository _blogRepository;
        readonly IHttpContextAccessor _http;

        public CommentService(IComentRepository commentRepositoy,  IHttpContextAccessor http, IBlogRepository blogRepository)
        {
            _commentRepositoy = commentRepositoy;

            _http = http;
            _blogRepository = blogRepository;
        }
        public async Task<CustomResponse<Coment>> CreateCommentAsync(CommentPostDto dto)
        {
           
  
                if (string.IsNullOrEmpty(dto.Text))
                {
                return new CustomResponse<Coment> { IsSuccess = false, Message = "Comment text cannot be empty", Data = null };
                }

                string userId = _http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                {
                return new CustomResponse<Coment> { IsSuccess = false, Message = "User is not found", Data = null };
                } 
                var coment = new Coment
                {
                    Text = dto.Text,
                    BlogId = dto.BlogId,
                    AppUserId = userId,
                    ParentId = dto.ParentId
                };

            await _commentRepositoy.AddAsync(coment);
            await _commentRepositoy.SaveChangesAsync();
            return new CustomResponse<Coment> { IsSuccess = true, Message = "Comment is created successfully", Data = coment };
        }
        public async Task<IEnumerable<CommentGetDto>> GetAllCommentsAsync()
        {

            IEnumerable<CommentGetDto> comments = await _commentRepositoy.GetQuery(x => !x.IsDeleted)
                .AsNoTrackingWithIdentityResolution()
                .Select(x => new CommentGetDto
                {
                    Parent = x.Parent,
                    AppUserId = x.AppUserId,
                    Id = x.Id,
                    CreatedAt = x.CreatedAt,
                    BlogId = x.BlogId,
                    Text = x.Text,
                    ParentId = x.ParentId,
                    Username = x.AppUser.UserName
                })
                .ToListAsync();
            return comments;
        }

        //public Task<CustomResponse<Reply>> CreateReplyAsync(ReplyDto dto)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<CustomResponse<ParentComment>> CreateCommentAsync(ParentCommentDto dto)
        //{
        //    if (dto.Text == null)
        //    {

        //        return new CustomResponse<ParentComment> { IsSuccess = false, Message = "Please fill text area" };
        //    }
        //    dto.Replies = dto.Replies ?? new List<ReplyDto>();
        //    ParentComment comment = new ParentComment
        //    {

        //        Text = dto.Text,
        //        BlogId = dto.BlogId,
        //        //AspNetUserId = dto.AspNetUserId

        //    };
        //    //foreach (var replyDto in dto.Replies)
        //    //{
        //    //    Reply reply = new Reply
        //    //    {
        //    //        Text = replyDto.Text,
        //    //    };
        //    //    comment.Replies.Add();
        //    //}
        //    await _commentRepositoy.AddAsync(comment);
        //    await _commentRepositoy.SaveChangesAsync();
        //    return new CustomResponse<ParentComment> { IsSuccess = true, Message = "Comment is created successfully", Data = comment };
        //}
        //public async Task<CustomResponse<Reply>> CreateReplyAsync(ReplyDto dto)
        //{
        //    if (dto.Text == null)
        //    {
        //        return new CustomResponse<Reply> { IsSuccess = false, Message = "Please fill text area" };
        //    }

        //    ParentComment parentComment = await _commentRepositoy.GetAsync(x => x.Id == dto.ParentCommentId);
        //    if (parentComment == null)
        //    {
        //        return new CustomResponse<Reply> { IsSuccess = false, Message = "Parent comment not found" };
        //    }
        //    var reply = new Reply
        //    {
        //        Text = dto.Text,
        //        BlogId = dto.BlogId,
        //        ParentCommentId = dto.ParentCommentId,
        //        //AspNetUsersId = dto.AspNetUsersId
        //    };
        //    parentComment.Replies.Add(reply);

        //    await _replyRepositoy.AddAsync(reply);
        //    await _replyRepositoy.SaveChangesAsync();

        //    return new CustomResponse<Reply> { IsSuccess = true, Message = "Reply is created successfully", Data = reply };

        //}
        //public async Task<IEnumerable<ReplyDto>> GetAllRepliesAsync()
        //{
        //    IEnumerable<ReplyDto> replies = await _replyRepositoy.GetQuery(x => !x.IsDeleted)
        //       .AsNoTrackingWithIdentityResolution().Select(x => new ReplyDto { AspNetUsersId = x.AspNetUsersId, Id = x.Id, CreatedAt = x.CreatedAt, BlogId = x.BlogId, ParentCommentId =x.ParentCommentId, Text = x.Text }).ToListAsync();
        //    return replies;
        //}
    }
}

