﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tomasos.BusinessLayer;
using Tomasos.Data;
using Tomasos.Models.AdminViewModels;
using Tomasos.Services;

namespace Tomasos.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ITomasosService _dbService;
        private readonly IUserRepository _userRepository;

        public AdminController(
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

        public IActionResult Index()
        {
            //var model = new AdminViewModel { Users = new List<UserViewModel>() };
            //var users = await _userManager.Users.ToListAsync();
            //foreach (var applicationUser in users)
            //{
            //    var user = new UserViewModel { User = applicationUser };
            //    var roles = await _userManager.GetRolesAsync(applicationUser);
            //    foreach (var role in roles)
            //    {
            //        if (role == "Admin")
            //        {
            //            applicationUser.IsAdmin = true;
            //           // user.IsAdmin = true;
            //        }
            //        else if (role == "PremiumUser")
            //        {
            //            applicationUser.IsPremium = true;
            //           // user.IsPremium = true;
            //        }
            //        else if (role == "RegularUser")
            //        {
            //            applicationUser.IsRegular = true;
            //           // user.IsRegular = true;
            //        }
            //    }
            //    //user.Roles = roles.ToList();
            //    model.Users.Add(user);

            //}

            //return View(model);
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("Admin"))
            {
                if (roles.Contains("PremiumUser"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "PremiumUser");
                    user.IsPremium = false;
                }

                if (roles.Contains("RegularUser"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "RegularUser");
                    user.IsRegular = false;
                }

                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return View("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkPremium(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains("PremiumUser"))
            {
                if (roles.Contains("Admin"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "Admin");
                    user.IsAdmin = false;
                }


                if (roles.Contains("RegularUser"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "RegularUser");
                    user.IsRegular = false;
                }

                await _userManager.AddToRoleAsync(user, "PremiumUser");

            }

            return View("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkRegular(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);


            if (!roles.Contains("RegularUser"))
            {
                if (roles.Contains("Admin"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "Admin");
                    user.IsAdmin = false;
                }

                if (roles.Contains("PremiumUser"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "PremiumUser");
                    user.IsPremium = false;
                }

                await _userManager.AddToRoleAsync(user, "RegularUser");
            }

            return View("Index");
        }
    }
}