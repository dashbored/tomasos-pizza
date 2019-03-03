using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Remotion.Linq.Clauses;
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

        public async Task<bool> AddNewOrderAsync(Bestallning order)
        {
            _context.Bestallning.Add(order);
            var result = await _context.SaveChangesAsync();

            return await Task.FromResult(result > 0);
        }

        public async Task<List<AspNetUsers>> GetAllUsersAsync()
        {
            return await (from u in _context.AspNetUsers
                          select u).ToListAsync();
        }

        public async Task<List<Matratt>> GetMattraterAsync()
        {
            return await (from dish in _context.Matratt
                          select dish)
                .Include(e => e.MatrattTypNavigation)
                .Include(e => e.MatrattProdukt)
                .ThenInclude(e => e.Produkt).ToListAsync();
        }

        public async Task<Matratt> GetMatrattAsync(int matrattId)
        {
            return await (from dish in _context.Matratt
                          where dish.MatrattId == matrattId
                          select dish)
                .Include(e => e.MatrattTypNavigation)
                .Include(e => e.MatrattProdukt)
                .ThenInclude(e => e.Produkt)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Produkt>> GetIngredientsAsync(int matrattId)
        {
            var dish = await _context.Matratt.Where(e => e.MatrattId == matrattId)
                .Include(e => e.MatrattProdukt)
                .ThenInclude(e => e.Produkt)
                .FirstOrDefaultAsync();

            var ingredients = dish.MatrattProdukt.Select(e => e.Produkt).ToList();

            return ingredients;
        }

        public async Task<Produkt> GetIngredientFromNameAsync(string ingredientName)
        {
            var ingredient = await (from ing in _context.Produkt
                                    where ing.ProduktNamn == ingredientName
                                    select ing).SingleOrDefaultAsync();

            if (ingredient == null)
            {
                var result = await AddNewIngredientAsync(ingredientName);
                ingredient = await (from ing in _context.Produkt
                                    where ing.ProduktNamn == ingredientName
                                    select ing).SingleOrDefaultAsync();
            }

            return ingredient;

        }

        public async Task<bool> AddNewIngredientAsync(string ingredientName)
        {
            var ingredient = new Produkt();
            ingredient.ProduktNamn = ingredientName;
            _context.Produkt.Add(ingredient);

            var result = await _context.SaveChangesAsync();
            return (result >= 1);
        }

        public async Task<bool> AddNewDishAsync(Matratt dish)
        {
            _context.Matratt.Add(dish);
            var result = await _context.SaveChangesAsync();

            return (result >= 1);
        }

        public async Task<bool> UpdateDishAsync(Matratt dish)
        {
            var checkDish = await (from d in _context.Matratt
                                   where d.MatrattId == dish.MatrattId
                                   select d).SingleOrDefaultAsync();

            if (checkDish == null)
            {
                return false;
            }
            var result = await _context.SaveChangesAsync();

            return await Task.FromResult(result >= 1);
        }
    }
}
