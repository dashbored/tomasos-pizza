using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tomasos.BusinessLayer;

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
    }
}