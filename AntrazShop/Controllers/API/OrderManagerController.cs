using AntrazShop.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderManagerController : ControllerBase
	{
		private readonly IOrderManagerService _orderManagerService;
		public OrderManagerController(IOrderManagerService orderManagerService)
		{
			_orderManagerService = orderManagerService;
		}

		[Authorize(Policy = "CanGetOrders")]
		[HttpGet("Orders/{page}/{take}")]
		public async Task<IActionResult> GetOrders(string ordercode = "", string status = "", int page = 1, int take = 10)
		{
			var (response, pagination) = await _orderManagerService.GetOrders(ordercode, status, page, take);
			if (!response.IsSuccess)
			{
				return BadRequest(new { error = response.Errors });
			}
			return Ok(new
			{
				Orders = response.Data,
				Pagination = pagination
			});
		}

		[Authorize(Policy = "CanUpdateOrderStatus")]
		[HttpPut("Order")]
		public async Task<IActionResult> UpdateOrderStatus(Guid orderCode, string status)
		{
			var response = await _orderManagerService.UpdateOrderStatus(orderCode, status);
			if (!response.IsSuccess)
			{
				return BadRequest(new { error = response.Errors });
			}
			return Ok(response.Data);
		}

		[HttpGet("{userId}")]
		public async Task<IActionResult> GetOrdersByUser(int userId)
		{
			var response = await _orderManagerService.GetOrdersByUser(userId);
			if (!response.IsSuccess)
			{
				return BadRequest(new { error = response.Errors });
			}
			return Ok(response.Data);
		}
	}
}
