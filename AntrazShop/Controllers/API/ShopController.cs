using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
		private readonly IShopService _shopService;
		public ShopController(IShopService shopService)
		{
			_shopService = shopService;
		}

		[HttpPost("Products")]
		public async Task<IActionResult> GetProducts([FromBody] ProductFilter filters,  int page = 1, int size = 12)
		{
			var response = await _shopService.GetProducts(filters, page, size);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}

			var (products, pagination) = response.Data;
			return Ok(new
			{
				Products = products,
				Pagination = pagination
			});

		}

	}
}
