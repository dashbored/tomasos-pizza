using System;
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
using Tomasos.Models.CartViewModels;
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
        private readonly ICartRepository _cart;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            ITomasosService dbService,
            IUserRepository userRepository,
            ICartRepository cart)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _dbService = dbService;
            _userRepository = userRepository;
            _cart = cart;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
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
                user.IsAdmin = true;
            }

            return View("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
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
                user.IsPremium = true;

            }

            return View("Index");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
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
                user.IsRegular = true;
            }

            return View("Index");
        }

        //public IActionResult ManageUsersViewComponent()
        //{
        //    return ViewComponent("ManageUsersViewComponent");
        //}


        public IActionResult ManageDishesViewComponent()
        {
            ViewData["test"] = "blah";
            return ViewComponent("ManageDishes");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ManageDishesViewComponent(CartViewModel viewModel)
        {
            if (viewModel.DishId != 0)
            {
                Dish model;
                if (viewModel.DishId == 1)
                {
                    model = new Dish();
                }
                else
                {
                    model = await _cart.GetDishAsync(viewModel.DishId);
                }
                return PartialView("_UpdateDishPartial", model);
            }
            return PartialView("_UpdateDishPartial");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateDish(Dish dish)
        {
            if (dish.DishId == 0)
            {
                var result = await _cart.CreateDishAsync(dish);
            }
            else
            {
                var result = await _cart.UpdateDishAsync(dish);
            }


            return View("Index");
        }
    }
}