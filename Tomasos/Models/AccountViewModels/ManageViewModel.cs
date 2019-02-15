using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Operations;

namespace Tomasos.Models.AccountViewModels
{
    public class ManageViewModel
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, ErrorMessage = "First name must be longer than {2} and less than {1}.", MinimumLength = 1)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Please only use letters for your first name")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "First name must be longer than {2} and less than {1}.", MinimumLength = 1)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Please only use letters for your last name")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please specify your address.")]
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Please only use letters, numbers and blankspace for your street address")]
        [Display(Name = "Street address")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Please fill in your post number.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please only use letters for your last name")]
        [Display(Name = "Post number")]
        public string PostNumber { get; set; }

        [Required(ErrorMessage = "Please fill in your post area.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Please only use letters for your last name")]
        [Display(Name = "Post area")]
        public string PostArea { get; set; }

        
        
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Please fill in your phone number correctly.")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        
        public string IdentityId { get; set; }

    }
}
