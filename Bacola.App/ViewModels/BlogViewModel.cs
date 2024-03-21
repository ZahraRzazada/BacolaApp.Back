using System;
using Bacola.Core.DTOS;
using Bacola.Core.Entities;
using Bacola.Service.Responses;

namespace Bacola.App.ViewModels
{
	public class BlogViewModel
	{
       

        public CustomResponse<BlogGetDto> BlogGetDto { get; set; }
        public IEnumerable<ParentCommentDto>? Comments { get; set; }
        public ParentComment? Comment { get; set; }
        public Reply? Reply { get; set; }
        public IEnumerable<ReplyDto> Replies { get; set; }
    }
}

