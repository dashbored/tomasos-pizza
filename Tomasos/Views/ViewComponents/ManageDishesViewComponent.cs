using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tomasos.BusinessLayer;
using Tomasos.Models.CartViewModels;
using Tomasos.Services;

namespace Tomasos.Views.ViewComponents
{
    public class ManageDishesViewComponent : ViewComponent
    {
        private readonly ITomasosService _dbService;
        private readonly ICartRepository _cart;

        public ManageDishesViewComponent(ITomasosService dbService, ICartRepository cart)
        {
            _dbService = dbService;
            _cart = cart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new CartViewModel();
            var dishes = await _dbService.GetMattraterAsync();
            foreach (var d in dishes)
            {
                var dish = await _cart.ConvertMatrattToDishAsync(d);

                //var ingredients = await _dbService.GetIngredientsAsync(d.MatrattId);
                //foreach (var ingredient in ingredients)
                //{
                //    dish.IngredientsList.Add(ingredient.ProduktNamn);
                //}

                //dish.IngredientsString = string.Join(", ", dish.IngredientsList);

                model.Dishes.Add(dish);
            }

            model.DishList = new SelectList(model.Dishes, "DishId", "Name");
            return View(model);
        }
    }
}
