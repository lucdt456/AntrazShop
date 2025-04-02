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

        [HttpGet]
        public async Task<IActionResult> GetUsers(int page = 1, int take = 10)
        {
            var (users, pagination) = await _accountManagerService.GetUsers(page, take);

            return Ok(new
            {
                Users = users,
                Pagination = pagination
            });
        }
    }
}
