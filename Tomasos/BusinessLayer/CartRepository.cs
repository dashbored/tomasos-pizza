﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;
using Newtonsoft.Json;
using Tomasos.Data;
using Tomasos.Models.CartViewModels;
using Tomasos.Models.Identity;
using Tomasos.Services;

namespace Tomasos.BusinessLayer
{
    public class CartRepository : ICartRepository
    {
        private readonly ITomasosService _dbService;
        private readonly UserManager<ApplicationUser> _userManager;
        public CartRepository(ITomasosService dbService, UserManager<ApplicationUser> userManager)
        {
            _dbService = dbService;
            _userManager = userManager;
        }

        public async Task<MenuViewModel> GetMenuAsync()
        {
            var model = new MenuViewModel();

            var dishes = await _dbService.GetMattraterAsync();
            foreach (var d in dishes)
            {
                var dish = await ConvertMatrattToDishAsync(d);

                //var ingredients = await _dbService.GetIngredientsAsync(d.MatrattId);
                //foreach (var ingredient in ingredients)
                //{
                //    dish.IngredientsList.Add(ingredient.ProduktNamn);
                //}

                //dish.IngredientsString = string.Join(", ", dish.IngredientsList);

                if (dish.DishType == "Pizza")
                {
                    model.Pizzas.Add(dish);
                }
                else if (dish.DishType == "Sallad")
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

        public async Task<Dish> ConvertMatrattToDishAsync(Matratt matratt)
        {
            var dish = new Dish
            {
                Name = matratt.MatrattNamn,
                Description = matratt.Beskrivning,
                DishId = matratt.MatrattId,
                DishType = matratt.MatrattTypNavigation.Beskrivning,
                Price = matratt.Pris
            };
            var ingredients = await _dbService.GetIngredientsAsync(matratt.MatrattId);
            foreach (var ingredient in ingredients)
            {
                dish.IngredientsList.Add(ingredient.ProduktNamn);
            }

            dish.IngredientsString = string.Join(", ", dish.IngredientsList);

            return dish;
        }

        public async Task<Matratt> ConvertDishToMatrattAsync(Dish dish, Matratt matratt)
        {

            matratt.MatrattNamn = dish.Name;
            matratt.Beskrivning = dish.Description;
            matratt.MatrattTyp = int.Parse(dish.DishType);
            matratt.Pris = dish.Price;

            var ingredients = dish.IngredientsString.Split(",");

            matratt.MatrattProdukt.Clear();
            foreach (var ingredient in ingredients)
            {
                var produkt = await _dbService.GetIngredientFromNameAsync(ingredient.Trim());
                var matrattProdukt = new MatrattProdukt
                {
                    Matratt = matratt,
                    MatrattId = matratt.MatrattId,
                    Produkt = produkt,
                    ProduktId = produkt.ProduktId
                };
                matratt.MatrattProdukt.Add(matrattProdukt);
            }

            return matratt;
        }

        public async Task<bool> CreateDishAsync(Dish dish)
        {
            var matratt = new Matratt();
            matratt = await ConvertDishToMatrattAsync(dish, matratt);
            
            var result = await _dbService.AddNewDishAsync(matratt);

            return (result);
        }

        public async Task<bool> UpdateDishAsync(Dish dish)
        {
            var matratt = await _dbService.GetMatrattAsync(dish.DishId);
            matratt = await ConvertDishToMatrattAsync(dish, matratt);

            var result = await _dbService.UpdateDishAsync(matratt);
            return result;
        }

        public async Task<Dish> GetDishAsync(int dishId)
        {
            var matratt = await _dbService.GetMatrattAsync(dishId);
            var dish = await ConvertMatrattToDishAsync(matratt);

            return dish;
        }

        public async Task<bool> OrderAsync(CartViewModel model, ApplicationUser user)
        {
            var order = new Bestallning();
            var customer = await _dbService.GetCustomerAsync(user.Id);

            order.Kund = customer;
            order.BestallningDatum = DateTime.Now;
            order.KundId = customer.KundId;
            order.Levererad = false;

            if (model.BonusPoints >= 100)
            {
                if (model.Dishes.Any(e => e.DishId == 1104))
                {
                    model.Dishes.Find(e => e.DishId == 1104).Quantity++;
                }
                else
                {
                    var dish = await GetDishAsync(1104);
                    dish.Quantity++;
                    model.Dishes.Add(dish);
                }

                model.BonusPoints -= 100;
            }

            foreach (var dish in model.Dishes)
            {
                var matratt = await _dbService.GetMatrattAsync(dish.DishId);
                var orderDetail = new BestallningMatratt()
                {
                    Antal = dish.Quantity,
                    Bestallning = order,
                    Matratt = matratt,
                    MatrattId = matratt.MatrattId
                };

                order.BestallningMatratt.Add(orderDetail);
            }

            order.Totalbelopp = user.IsPremium ? model.PremiumPrice : model.TotalPrice;

            var result = await _dbService.AddNewOrderAsync(order);
            if (result)
            {
                user.BonusPoints = model.BonusPoints;
            }
            return await Task.FromResult<bool>(true);
        }

        public CartViewModel CreateViewModel(List<Dish> cart, ApplicationUser result)
        {
            var model = new CartViewModel
            {
                Dishes = cart,
                TotalPrice = cart.Sum(e => e.Quantity * e.Price),
                NumberOfItems = cart.Sum(e => e.Quantity)
            };

            if (result.IsPremium)
            {
                model.BonusPoints = result.BonusPoints + cart.Sum(e => e.Quantity) * 10;
                model.PremiumPrice = model.NumberOfItems >= 3
                    ? (int)((model.TotalPrice - 70) * 0.8)
                    : (int)(model.TotalPrice * 0.8);
            }

            return model;
        }
    }
}
