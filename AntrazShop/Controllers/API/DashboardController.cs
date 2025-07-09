using AntrazShop.Interfaces.Services;
using AntrazShop.Models.DTOModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class DashboardController : ControllerBase
	{
		private readonly IDashboardService _dashboardService;
		public DashboardController(IDashboardService dashboardService)
		{
			_dashboardService = dashboardService;
		}


		[HttpGet("DashboardStats")]
		public async Task<IActionResult> GetDashboardStatsThisMonth()
		{
			var response = await _dashboardService.GetDashboardStatsThisMonth();
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}
		[HttpGet("OrderOverView/{day}")]
		public async Task<IActionResult> GetOrderOverview(int day)
		{
			var response = await _dashboardService.GetOrderOverview(day);
			if (!response.IsSuccess)
			{
				return BadRequest(new { errors = response.Errors });
			}
			return Ok(response.Data);
		}
	}
}
