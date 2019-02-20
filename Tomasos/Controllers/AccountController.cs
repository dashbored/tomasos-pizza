using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tomasos.BusinessLayer;
using Tomasos.Data;
using Tomasos.Models;
using Tomasos.Models.AccountViewModels;
using Tomasos.Models.Identity;
using Tomasos.Services;

namespace Tomasos.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ITomasosService _dbService;
        private readonly IUserRepository _userRepository;

        public AccountController(
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {


                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password");
                    await _signInManager.SignInAsync(user, false);
                    _logger.LogInformation("User logged in a new account with password");
                }
                AddErrors(result);

                result = await _userManager.AddToRoleAsync(user, "RegularUser");
                if (result.Succeeded)
                {
                    _logger.LogInformation("Added regular role to user");
                    return RedirectToAction("Manage", "Account");
                }

            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToAction("Index", "Home");
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account is locked out.");

                    return View();
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");


                return View(model);

            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            var model = new ManageViewModel();
            var customer = await _dbService.GetCustomerAsync(currentUser.Id);

            if (customer == null)
            {
                model.Email = currentUser.Email;
                model.IdentityId = currentUser.Id;
            }
            else
            {
                model = _userRepository.UpdateModel(customer, model);

            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(ManageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return Challenge();
                }

                var customer = await _dbService.GetCustomerAsync(currentUser.Id);
                bool result;
                if (customer == null)
                {
                    customer = new Kund();
                    _userRepository.UpdateCustomer(customer, model);
                    result = await _dbService.AddNewCustomerAsync(customer);
                    if (result == false)
                    {
                        _logger.LogWarning("Could not create new customerinfo");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    _userRepository.UpdateCustomer(customer, model);
                    result = await _dbService.UpdateCustomerAsync(customer);
                }

                if (result == false)
                {
                    ViewData["Success"] = "Failed";
                }
                else
                {
                    ViewData["Success"] = "Account updated";
                }

                var message = result ? "Success" : "Failed";
                return RedirectToAction("Index", "Home", new { result = message });
            }



            return View(model);
        }

        public void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}