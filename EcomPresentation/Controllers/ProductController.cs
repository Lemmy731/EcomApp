using Commons.GenericResponse;
using EcomApplication.Service.Implementation;
using EcomApplication.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcomPresentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize]
        [HttpGet("products")]
        public async Task<IActionResult> Products()
        {
            try
            {
                var response = await _productService.GetProduct();
                if (response.StatusCode == StatusCodes.Status200OK)
                {
                    return Ok(response.Data);
                }
                return BadRequest(response.Message);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
