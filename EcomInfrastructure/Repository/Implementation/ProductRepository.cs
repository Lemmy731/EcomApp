using Commons.DTO.Product;
using Commons.GenericResponse;
using EcomDomain.Entity;
using EcomInfrastructure.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomInfrastructure.Repository.Implementation
{
    public class ProductRepository: IProductRepository
    {
        private readonly EcomDbContext _dataContext;
        public ProductRepository(EcomDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<ApiResponse<List<Product>>> GetProduct()
        {
            try
            {
                var response = await _dataContext.Products.Include(x => x.Variations).ToListAsync();
                if (response.Count > 0)
                {
                    return ApiResponse<List<Product>>.Success("success", response);
                }
                return ApiResponse<List<Product>>.Failure(StatusCodes.Status404NotFound, "not found");
            }
            catch (Exception ex) 
            {
                return ApiResponse<List<Product>>.Failure(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
