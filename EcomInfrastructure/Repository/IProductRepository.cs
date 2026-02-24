using Commons.DTO.Product;
using Commons.GenericResponse;
using EcomDomain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomInfrastructure.Repository
{
    public interface IProductRepository
    {
        Task<ApiResponse<List<Product>>> GetProduct();
    }
}
