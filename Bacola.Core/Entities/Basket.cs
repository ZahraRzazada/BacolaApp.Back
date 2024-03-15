using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class Basket:BaseEntity
	{
	    public string AppUserId { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;
		public List<BasketItem> BasketItems { get; set; } = null!;
    }
}

