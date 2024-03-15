using System;
namespace Bacola.Core.DTOS
{
	public class SettingGetDto
	{
        public int Id { get; set; }
        public string Key { get; set; } = null!;
        public string Value { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

