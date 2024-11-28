using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace BoilerPlate.Entity.Entities.Concrete.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }


        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
