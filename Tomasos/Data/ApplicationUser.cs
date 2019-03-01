using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Tomasos.Data
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; set; }
        public bool IsPremium { get; set; }
        public bool IsRegular { get; set; }
        public int BonusPoints { get; set; }
    }
}
