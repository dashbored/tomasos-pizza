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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ITomasosService _dbService;
        private readonly IUserRepository _userRepository;

        public ManageUsersViewComponent(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            ITomasosService dbService,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _dbService = dbService;
            _userRepository = userRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new AdminViewModel { Users = new List<UserViewModel>() };
            var users = await _userManager.Users.ToListAsync();
            foreach (var applicationUser in users)
            {
                var user = new UserViewModel { User = applicationUser };
                var roles = await _userManager.GetRolesAsync(applicationUser);
                foreach (var role in roles)
                {
                    if (role == "Admin")
                    {
                        user.IsAdmin = true;
                    }
                    else if (role == "PremiumUser")
                    {
                        user.IsPremium = true;
                    }
                    else if (role == "RegularUser")
                    {
                        user.IsRegular = true;
                    }
                }
                user.Roles = roles.ToList();
                model.Users.Add(user);

            }

            return View(model);
        }

    }
}
