using Commons.DTO.Product;
using Commons.GenericResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApplication.Service.Interface
{
    public interface IProductService
    {
        Task <ApiResponse<List<ProductDto>>> GetProduct();    
    }
}
