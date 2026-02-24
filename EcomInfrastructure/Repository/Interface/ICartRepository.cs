using Commons.DTO.Cart;
using Commons.GenericResponse;
using EcomDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomInfrastructure.Repository.Interface
{
    public interface ICartRepository
    {
        Task<bool> AddCart(Cart cart, string userId);
        Task<bool> UpdateCart(UpdateCartDto updateCartDto, string userId);
        Task<string>  RemoveCart(string productId, string userId);
    }
}
