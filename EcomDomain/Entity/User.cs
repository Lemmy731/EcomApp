using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomDomain.Entity
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }  
        public ICollection<Cart> Carts { get; set; }        
    }
}
