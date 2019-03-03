using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.DotNet;
using Newtonsoft.Json;
using Tomasos.BusinessLayer;
using Tomasos.Data;
using Tomasos.Models.CartViewModels;
using Tomasos.Models.Identity;

namespace Tomasos.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cart;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ICartRepository cart, UserManager<ApplicationUser> userManager)
        {
            _cart = cart;
            _userManager = userManager;

        }
        public async Task<IActionResult> Menu()
        {
            HttpContext.Session.Clear();
            var model = await _cart.GetMenuAsync();

            return View(model);
        }

        public async Task<IActionResult> CartDetails(int id)
        {
            List<Dish> cart;
            var result = _userManager.GetUserAsync(User).Result;
            var roles = await _userManager.GetRolesAsync(result);

            if (HttpContext.Session.GetString("cart") == null)
            {
                cart = new List<Dish>();
            }
            else
            {
                var sessionCart = HttpContext.Session.GetString("cart");
                cart = JsonConvert.DeserializeObject<List<Dish>>(sessionCart);
            }

            var dish = await _cart.GetDishAsync(id);
            if (cart.Any(e => e.DishId == dish.DishId))
            {
                cart.Find(e => e.DishId == dish.DishId).Quantity++;
            }
            else
            {
                dish.Quantity++;
                cart.Add(dish);
            }

            var jsonCart = JsonConvert.SerializeObject(cart, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });

            HttpContext.Session.SetString("cart", jsonCart);

            var model = _cart.CreateViewModel(cart, result);


            return PartialView("_CartPartial", model);
        }

        public async Task<IActionResult> Order()
        {
            if (HttpContext.Session.GetString("cart") != null)
            {

                var sessionCart = HttpContext.Session.GetString("cart");
                var cart = JsonConvert.DeserializeObject<List<Dish>>(sessionCart);
                var user = _userManager.GetUserAsync(User).Result;

                var model = _cart.CreateViewModel(cart, user);
                var result = await _cart.OrderAsync(model, user);
                var updateResult = await _userManager.UpdateAsync(user);

                HttpContext.Session.Remove("cart");
            }
            return View();
        }

        public IActionResult Remove(int id)
        {
            var sessionCart = HttpContext.Session.GetString("cart");
            var cart = JsonConvert.DeserializeObject<List<Dish>>(sessionCart);

            var dish = cart.Find(e => e.DishId == id);

            dish.Quantity--;
            if (dish.Quantity == 0)
            {
                cart.Remove(dish);
            }

            var jsonCart = JsonConvert.SerializeObject(cart, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });

            HttpContext.Session.SetString("cart", jsonCart);

            var model = _cart.CreateViewModel(cart, _userManager.GetUserAsync(User).Result);
            return PartialView("_CartPartial", model);
        }
    }
}