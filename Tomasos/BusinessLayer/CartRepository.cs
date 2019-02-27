using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;
using Newtonsoft.Json;
using Tomasos.Models.CartViewModels;
using Tomasos.Models.Identity;
using Tomasos.Services;

namespace Tomasos.BusinessLayer
{
    public class CartRepository : ICartRepository
    {
        private readonly ITomasosService _dbService;
        public CartRepository(ITomasosService dbService)
        {
            _dbService = dbService;
        }

        public async Task<MenuViewModel> GetMenuAsync()
        {
            var model = new MenuViewModel();
            
            var dishes =  await _dbService.GetDishesAsync();
            foreach (var d in dishes)
            {
                var dish = new Dish
                {
                    Name = d.MatrattNamn,
                    Description = d.Beskrivning,
                    DishId = d.MatrattId,
                    DishType = d.MatrattTypNavigation.Beskrivning,
                    Price = d.Pris
                };

                var ingredients = await _dbService.GetIngredientsAsync(d.MatrattId);
                foreach (var ingredient in ingredients)
                {
                    dish.IngredientsList.Add(ingredient.ProduktNamn);
                }

                dish.IngredientsString = string.Join(", ", dish.IngredientsList);

                if (dish.DishType == "Pizza")
                {
                    model.Pizzas.Add(dish);
                } else if (dish.DishType == "Sallad")
                {
                    model.Sallads.Add(dish);
                }
                else if (dish.DishType == "Pasta")
                {
                    model.Pasta.Add(dish);
                }
            }
            return model;
        }

        public async Task<Matratt> GetDish(int dishId)
        {
            var dish = await _dbService.GetDishAsync(dishId);

            return dish;
        }

    }
}
