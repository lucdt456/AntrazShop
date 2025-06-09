using AntrazShop.Data;
using AntrazShop.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagerController : ControllerBase
    {
        private readonly IAccountManagerService _accountManagerService;
        public AccountManagerController(IAccountManagerService accountManagerService)
        {
            _accountManagerService = accountManagerService;
        }

        [HttpGet("{page}/{take}")]
        public async Task<IActionResult> GetUsers(int page = 1, int take = 10)
        {
            var (response, pagination) = await _accountManagerService.GetUsers(page, take);
            if (!response.IsSuccess)
            {
                return BadRequest(new { error = response.Errors });
            }
            else
            {
				return Ok(new
				{
					Users = response.Data,
					Pagination = pagination
				});
			}   
        }
    }
}
