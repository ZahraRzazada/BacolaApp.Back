using System;
namespace Bacola.Service.Responses
{
	public class PagginatedResponse<T>
	{
		public IEnumerable<T> Items { get; set; }
		public int CurrentPage { get; set; }
		public decimal TotalPages {get;set;}
	}
}

