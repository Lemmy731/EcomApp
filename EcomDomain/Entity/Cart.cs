using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomDomain.Entity
{
    public class Cart: BaseEntity
    {
        public string ProductId { get; set; } 
        public Product Product { get; set; }
        public string UserId { get; set; }   
        public User User { get; set; }  
        public int Quantity { get; set; }   
        
    }
}
