using EcomInfrastructure.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcomPresentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public CartController()
        {

        }
        [Authorize(Roles ="customer")]
        [HttpPost("add")]
        public async Task<IActionResult> CartAdd()
        {
            return Ok();
        }
        [Authorize] 
        [HttpPut("update")]
        public async Task<IActionResult> CartUpdate()
        {
            return Ok();
        }
        [Authorize] 
        [HttpDelete("delete")]
        public async Task<IActionResult> CartRemove()
        {
            return Ok();
        }
    }
}
