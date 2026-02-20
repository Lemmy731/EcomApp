using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomDomain.Entity
{
    public class ProductVariation: BaseEntity
    {
        public string Color { get; set; }    
        public double Size { get; set; }  
        public int Quantity { get; set; }       
        public string ProductId { get; set; }   
        public Product Product { get; set; }    

    }
}
