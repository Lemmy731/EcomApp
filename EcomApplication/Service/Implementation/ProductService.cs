using Commons.DTO.Product;
using Commons.GenericResponse;
using EcomApplication.Service.Interface;
using EcomDomain.Entity;
using EcomInfrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Timeouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Service.Implementation
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<ApiResponse<List<ProductDto>>> GetProduct()
        {
            try
            {
                var response = await _repository.GetProduct();
                List<ProductDto> products = new List<ProductDto>();
                if (response.StatusCode == StatusCodes.Status200OK)
                {
                    foreach (var item in response.Data)
                    {
                        foreach(var vari in item.Variations)
                        {
                            var prod = new ProductDto
                            {
                                Id = vari.Id,
                                ProductId = vari.ProductId, 
                                Name = item.Name,
                                ImageUrl = item.ImageUrl,   
                                Description = item.Description, 
                                Color = vari.Color, 
                                Size = vari.Size,
                                Quantity = vari.Quantity,
                                Price = vari.Price, 
                                
                            };
                            products.Add(prod); 
                        };
                    }
                    return ApiResponse<List<ProductDto>>.Success("success", products);
                }
                return ApiResponse<List<ProductDto>>.Failure(StatusCodes.Status404NotFound, "not found");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ProductDto>>.Failure(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
