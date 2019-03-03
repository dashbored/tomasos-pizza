using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tomasos.BusinessLayer;
using Tomasos.Controllers;
using Tomasos.Data;
using Tomasos.Models.AdminViewModels;
using Tomasos.Services;

namespace Tomasos.Views.ViewComponents
{
    public class ManageUsersViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsersViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new AdminViewModel { Users = new List<ApplicationUser>() };
            var users = await _userManager.Users.ToListAsync();
            foreach (var applicationUser in users)
            {
                //var user = new UserViewModel { User = applicationUser };
                var roles = await _userManager.GetRolesAsync(applicationUser);
                foreach (var role in roles)
                {
                    if (role == "Admin")
                    {
                        applicationUser.IsAdmin = true;
                        //user.IsAdmin = true;
                    }
                    else if (role == "PremiumUser")
                    {
                        applicationUser.IsPremium = true;
                        //user.IsPremium = true;
                    }
                    else if (role == "RegularUser")
                    {
                        applicationUser.IsRegular = true;
                        //user.IsRegular = true;
                    }
                }

                var result = await _userManager.UpdateAsync(applicationUser);
                //user.Roles = roles.ToList();
                model.Users.Add(applicationUser);

            }

            return View(model);
            
        }

    }
}
