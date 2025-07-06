using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
		private readonly ICartService _cartService;

		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}

		[Authorize(Policy = "CanAddToCart")]
		[HttpPost("add")]
		public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
		{
			var response = await _cartService.AddToCart(dto);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		[Authorize(Policy = "CanGetCart")]
		[HttpGet("{userId}")]
		public async Task<IActionResult> GetCart(int userId)
		{
			var response = await _cartService.GetCart(userId);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		[Authorize(Policy = "CanUpdateCartItem")]
		[HttpPut("update")]
		public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartDto dto)
		{
			var response = await _cartService.UpdateCartItem(dto);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		[Authorize(Policy = "CanRemoveFromCart")]
		[HttpDelete("{userId}/{colorCapacityId}")]
		public async Task<IActionResult> RemoveFromCart(int userId, int colorCapacityId)
		{
			var response = await _cartService.RemoveFromCart(userId, colorCapacityId);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}

		[Authorize(Policy = "CanCheckOut")]

		[HttpPost("checkout/{userId}")]
		public async Task<IActionResult> CheckOut(int userId, [FromBody] OrderDTO dto)
		{
			var response = await _cartService.CreateOrder(userId, dto);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}
	}
}
