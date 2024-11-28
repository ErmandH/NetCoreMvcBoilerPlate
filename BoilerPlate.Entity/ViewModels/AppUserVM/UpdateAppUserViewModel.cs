using BoilerPlate.Entity.Entities.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoilerPlate.Entity.ViewModels.AppUserVM
{
    public class UpdateAppUserViewModel
    {
        public ListAppUserViewModel? UserViewModel { get; set; }
        public IEnumerable<AppRole>? Roles { get; set; }
    }
}
