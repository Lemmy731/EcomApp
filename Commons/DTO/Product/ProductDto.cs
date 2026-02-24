using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.DTO.Product
{
    public class ProductDto
    {
        public string Id { get; set; } 
        public string ProductId { get; set; }       
        public string Name { get; set; }
        public decimal Size { get; set; }
        public decimal Price { get; set; } 
        public string Color { get; set; }   
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }  
    }
}
