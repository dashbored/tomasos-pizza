﻿using System;
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
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "First name must be longer than {2} and less than {1}.", MinimumLength = 1)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Please only use letters for your last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please specify your address.")]
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Please only use letters, numbers and blankspace for your street address")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Please fill in your post number.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please only use letters for your last name")]
        public string PostNumber { get; set; }

        [Required(ErrorMessage = "Please fill in your post area.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Please only use letters for your last name")]
        public string PostArea { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Wrong format of email address.")]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Please fill in your phone number correctly.")]
        public string PhoneNumber { get; set; }

        [Required]
        public Guid IdentityId { get; set; }

    }
}
