using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.App.ViewModels
{
	public class BlogViewModel
	{
       

        public CustomResponse<BlogGetDto> BlogGetDto { get; set; }
        public IEnumerable<CommentGetDto>? Comments { get; set; }
        public Coment? Coment { get; set; }
        //public Reply? Reply { get; set; }
        //public IEnumerable<ReplyDto> Replies { get; set; }
    }
}

