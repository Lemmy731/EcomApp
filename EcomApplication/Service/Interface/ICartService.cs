using Commons.DTO.Cart;
using Commons.GenericResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Service.Interface
{
    public interface ICartService
    {
        Task<ApiResponse<AddCartDto>> AddCart(AddCartDto addCartDto);
        Task<ApiResponse<UpdateCartDto>> UpdateCart(UpdateCartDto updateCartDto);
        Task<ApiResponse<string>> RemoveCart(string Id);
    }
}
