using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcomPresentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public ProductController()
        {

        }
        [Authorize]
        [HttpGet("products")]
        public async Task<IActionResult> Products()
        {
            return Ok();
        }
    }
}
