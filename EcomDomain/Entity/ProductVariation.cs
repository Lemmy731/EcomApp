using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomDomain.Entity
{
    public class ProductVariation: BaseEntity
    {
        public string Color { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Size { get; set; }  
        public int Quantity { get; set; }       
        public string ProductId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }  
        public Product Product { get; set; }    
    }
}
