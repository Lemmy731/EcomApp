using Commons.DTO.Cart;
using Commons.GenericResponse;
using EcomApplication.Service.Interface;
using EcomDomain.Entity;
using EcomInfrastructure.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Service.Implementation
{
    public class CartService: ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILoginUser _loginUser;
        public CartService(ICartRepository cartRepository, ILoginUser loginUser)
        {
            _cartRepository = cartRepository;
            _loginUser = loginUser;
        }
        public async Task<ApiResponse<AddCartDto>> AddCart(AddCartDto addCartDto)
        {
            try
            {
                Cart cart = new Cart
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = addCartDto.ProductId,
                    Quantity = addCartDto.Quantity,
                    UserId = _loginUser.GetUserId()
                };
                var response = await _cartRepository.AddCart(cart, _loginUser.GetUserId());
                if (response)
                {
                    return ApiResponse<AddCartDto>.Success("item added to cart", addCartDto);
                }
                return ApiResponse<AddCartDto>.Failure(StatusCodes.Status400BadRequest, "unable to add");
            }
            catch (Exception ex) 
            {
                return ApiResponse<AddCartDto>.Failure(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        public async Task<ApiResponse<UpdateCartDto>> UpdateCart(UpdateCartDto updateCartDto)
        {
            try
            {
                var response = await _cartRepository.UpdateCart(updateCartDto, _loginUser.GetUserId());
                if (response)
                {
                    return ApiResponse<UpdateCartDto>.Success("item added to cart", updateCartDto);
                }
                return ApiResponse<UpdateCartDto>.Failure(StatusCodes.Status400BadRequest, "unable to add");
            }
            catch (Exception ex)
            {
                return ApiResponse<UpdateCartDto>.Failure(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        public async Task<ApiResponse<string>> RemoveCart(string productId)
        {
            try
            {
                var response = await _cartRepository.RemoveCart(productId, _loginUser.GetUserId());
                if (response == "remove" || response == "already removed")
                {
                    return ApiResponse<string>.Success("cart removed", response);
                }
                return ApiResponse<string>.Failure(StatusCodes.Status400BadRequest, "unable to add");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Failure(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
