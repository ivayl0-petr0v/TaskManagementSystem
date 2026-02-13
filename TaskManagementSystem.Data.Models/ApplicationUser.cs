using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static TaskManagementSystem.GCommon.ValidationConstants;

namespace TaskManagementSystem.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(AppUserFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(AppUserLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public string UserFullName => FirstName + " " + LastName;
    }
}
