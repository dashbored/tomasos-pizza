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
                var dish = ConvertMatrattToDish(d);
                //var dish = new Dish
                //{
                //    Name = d.MatrattNamn,
                //    Description = d.Beskrivning,
                //    DishId = d.MatrattId,
                //    DishType = d.MatrattTypNavigation.Beskrivning,
                //    Price = d.Pris
                //};

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

        private Dish ConvertMatrattToDish(Matratt matratt)
        {
            var dish = new Dish
            {
                Name = matratt.MatrattNamn,
                Description = matratt.Beskrivning,
                DishId = matratt.MatrattId,
                DishType = matratt.MatrattTypNavigation.Beskrivning,
                Price = matratt.Pris
            };
            return dish;
        }

        public async Task<Dish> GetDishAsync(int dishId)
        {
            var matratt = await _dbService.GetDishAsync(dishId);
            var dish = ConvertMatrattToDish(matratt);

            return dish;
        }

        public async Task<bool> OrderAsync(List<Dish> orderCart, string id)
        {
            var order = new Bestallning();
            var customer = await _dbService.GetCustomerAsync(id);

            order.Kund = customer;
            order.BestallningDatum = DateTime.Now;
            order.KundId = customer.KundId;
            order.Levererad = false;

            foreach (var dish in orderCart)
            {
                var matratt = await _dbService.GetDishAsync(dish.DishId);
                var orderDetail = new BestallningMatratt()
                {
                    Antal = dish.Quantity,
                    Bestallning = order,
                    Matratt = matratt,
                    MatrattId = matratt.MatrattId
                };

                order.BestallningMatratt.Add(orderDetail);
            }

            order.Totalbelopp = orderCart.Sum(e => e.Price * e.Quantity);

            _dbService.AddNewOrderAsync(order);

            return await Task.FromResult<bool>(true);
        }
    }
}
