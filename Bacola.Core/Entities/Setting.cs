using System;
using Bacola.Core.Entities.BaseEntities;

namespace Bacola.Core.Entities
{
	public class Setting:BaseEntity
	{
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}

