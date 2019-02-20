using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos.Data;

namespace Tomasos.Models.AdminViewModels
{
    public class UserViewModel
    {
        public ApplicationUser User { get; set; }
        public List<string> Roles { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsPremium { get; set; }
        public bool IsRegular { get; set; }
    }
}
