using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerPlate.Entity.Entities.Concrete.Identity;
namespace BoilerPlate.Entity.ViewModels.AppUserVM
{
    public class ListAppUserViewModel
    {
        public AppUser AppUser { get; set; }
        public string UserRole { get; set; }
    }
}
