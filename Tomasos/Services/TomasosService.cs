using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Tomasos.Models.Identity;

namespace Tomasos.Services
{
    public class TomasosService : ITomasosService
    {
        private readonly TomasosContext _context;

        public TomasosService(TomasosContext context)
        {
            _context = context;
        }


        public async Task<Kund> GetCustomerAsync(string id)
        {
            return await (from customer in _context.Kund
                          where customer.IdentityId == id
                          select customer).SingleOrDefaultAsync();
        }

        public async Task<bool> UpdateCustomerAsync(Kund customer)
        {
            var getCustResult = await (from cust in _context.Kund
                                       where cust.IdentityId == customer.IdentityId
                                       select cust).SingleOrDefaultAsync();

            if (getCustResult == null)
            {
                return false;
            }


            var result = await _context.SaveChangesAsync();

            return await Task.FromResult(result == 1);
        }

        public async Task<bool> AddNewCustomerAsync(Kund customer)
        {
            _context.Kund.Add(customer);
            var result = await _context.SaveChangesAsync();

            return await Task.FromResult(result == 1);
        }

        public async Task<List<AspNetUsers>> GetAllUsersAsync()
        {
            return await (from u in _context.AspNetUsers
                          select u).ToListAsync();
        }

        public async Task<List<Matratt>> GetDishesAsync()
        {
            return await (from dish in _context.Matratt
                          select dish).ToListAsync();
        }

        public async Task<Matratt> GetDishAsync(int id)
        {
            return await (from dish in _context.Matratt
                          where dish.MatrattId == id
                          select dish).FirstOrDefaultAsync();
        }

        public async Task<List<Produkt>> GetIngredientsAsync(int id)
        {
            var ingredientIds = await (from ingredient in _context.MatrattProdukt
                                       where ingredient.MatrattId == id
                                       select ingredient).ToListAsync();
            if (ingredientIds.Count == 0)
            {
                return new List<Produkt>();
            } 

            var ingredients = new List<Produkt>();
            foreach (var ingredient in ingredientIds)
            {
                var dishIngredient = await (from ing in _context.Produkt
                                            where ing.ProduktId == ingredient.ProduktId
                                            select ing).FirstOrDefaultAsync();

                ingredients.Add(dishIngredient);
            }

            return ingredients;
        }
    }
}
