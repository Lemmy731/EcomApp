using Commons.DTO.Cart;
using EcomApplication.Service.Interface;
using EcomDomain.Entity;
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
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> CartAdd([FromBody] AddCartDto addCartDto)
        {
            try
            {
                var response = await _cartService.AddCart(addCartDto);
                if (response.StatusCode == StatusCodes.Status200OK)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [Authorize] 
        [HttpPut("update")]
        public async Task<IActionResult> CartUpdate([FromBody] UpdateCartDto updateCartDto)
        {
            try
            {
                var response = await _cartService.UpdateCart(updateCartDto);
                if (response.StatusCode == StatusCodes.Status200OK)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize] 
        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> CartRemove(string productId)
        {
            try
            {

                var response = await _cartService.RemoveCart(productId);
                if (response.StatusCode == StatusCodes.Status200OK)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
