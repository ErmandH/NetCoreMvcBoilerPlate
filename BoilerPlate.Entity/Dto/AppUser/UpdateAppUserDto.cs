using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoilerPlate.Entity.Dto.AppUser
{
    public class UpdateAppUserDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required,EmailAddress]
        public string? Email { get; set;}

        [Required]
        public string? PhoneNumber { get; set;}
        
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}