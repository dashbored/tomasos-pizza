using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos.Models.CartViewModels;
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
            var dishes =  await _dbService.GetDishesAsync();
            foreach (var dish in dishes)
            {
                var ingredients = await _dbService.GetIngredientsAsync(dish.MatrattId);
                
            }
            return new MenuViewModel();
        }
    }
}
