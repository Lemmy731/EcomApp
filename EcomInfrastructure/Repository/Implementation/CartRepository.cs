using Commons.DTO.Cart;
using Commons.GenericResponse;
using EcomDomain.Entity;
using EcomInfrastructure.DataContext;
using EcomInfrastructure.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomInfrastructure.Repository.Implementation
{
    public class CartRepository: ICartRepository
    {
        private readonly EcomDbContext _ecomDbContext;
        public CartRepository(EcomDbContext ecomDbContext)
        {
            _ecomDbContext = ecomDbContext;
        }
        public async Task<bool> AddCart(Cart cart, string userId)
        {
            try
            {
                var dbCart = await _ecomDbContext.Carts.Where(x => x.ProductId == cart.ProductId && x.UserId == userId).FirstOrDefaultAsync();
                if (dbCart == null)
                {
                    await _ecomDbContext.Carts.AddAsync(cart);
                }
                else
                {
                    dbCart.Quantity = dbCart.Quantity + 1;
                    _ecomDbContext.Carts.Update(dbCart);
                }
                var response = await _ecomDbContext.SaveChangesAsync();
                if(response > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
        public async Task<bool> UpdateCart(UpdateCartDto updateCartDto, string userId)
        {
            try
            {
               var cart = await _ecomDbContext.Carts.Where(x => x.ProductId == updateCartDto.ProductId && x.UserId == userId).FirstOrDefaultAsync();
                if(!string.IsNullOrEmpty(cart.Id))
                {
                    if(cart.Quantity > 0)
                    {
                        cart.Quantity = cart.Quantity - 1;
                        _ecomDbContext.Carts.Update(cart);
                        var response = await _ecomDbContext.SaveChangesAsync();
                        if (response > 0)
                        {
                            return true;
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<string> RemoveCart(string productId, string userId)
        {
            try
            {
                var vari = _ecomDbContext.ProductVariations.Where(x => x.Id == productId).FirstOrDefault(); 
                var cart = await _ecomDbContext.Carts.Where(x => x.ProductId == vari.ProductId && x.UserId == userId).FirstOrDefaultAsync(); 
                if (cart != null)
                {
                    _ecomDbContext.Remove(cart);
                   var response = await _ecomDbContext.SaveChangesAsync();
                    if(response > 0)
                    {
                        return "remove";
                    }
                }
                return "already removed";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
