using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tomasos.BusinessLayer;
using Tomasos.Models.Identity;

namespace Tomasos.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cart;
        public CartController(ICartRepository cart)
        {
            _cart = cart;

        }
        public async Task<IActionResult> Menu()
        {
            var model = await _cart.GetMenuAsync();
            return View(model);
        }

        public async Task<IActionResult> CartDetails(int id)
        {
            List<Matratt> cart;

            if (HttpContext.Session.GetString("cart") == null)
            {
                cart = new List<Matratt>();
            }
            else
            {
                var sessionCart = HttpContext.Session.GetString("cart");
                cart = JsonConvert.DeserializeObject<List<Matratt>>(sessionCart);
            }

            var dish = await _cart.GetDish(id);
            cart.Add(dish);
            var jsonCart = JsonConvert.SerializeObject(cart, new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                Formatting = Formatting.Indented
            });

            HttpContext.Session.SetString("cart", jsonCart);


            return PartialView("_CartPartial", cart);
        }
    }
}