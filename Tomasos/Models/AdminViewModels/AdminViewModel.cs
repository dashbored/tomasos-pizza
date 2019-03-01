using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos.Data;
using Tomasos.Models.Identity;

namespace Tomasos.Models.AdminViewModels
{
    public class AdminViewModel
    {
        public List<ApplicationUser> Users { get; set; }
    }
}
