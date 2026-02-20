using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomDomain.Entity
{
    public class Product: BaseEntity
    {
        public string Name { get; set; } 
        public int TotalQuantity { get; set; }   
        public bool InStock { get; set; }   
        public ICollection<ProductVariation> Variations { get; set; }  
        public ICollection<Cart> Carts { get; set; }
    }
}
