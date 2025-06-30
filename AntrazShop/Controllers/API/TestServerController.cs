using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestServerController : ControllerBase
    {
        
		[HttpGet]
		public async Task<IActionResult> GetBrands()
		{
			return Ok("Test thành công!");
		}

    }
}
