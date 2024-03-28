using System;
using Bacola.Core.DTOS;
using Bacola.Service.Responses;
using static Bacola.App.Controllers.ShopController;

namespace Bacola.App.ViewModels;
public class ShopViewModel
{
    public PagginatedResponse<ProductGetDto> Products { get; set; } = new();
    public ProductFilterDto Filter { get; set; } = new();

}


