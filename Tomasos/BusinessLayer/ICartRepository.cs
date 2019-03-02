using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos.Data;
using Tomasos.Models.CartViewModels;
using Tomasos.Models.Identity;

namespace Tomasos.BusinessLayer
{
    public interface ICartRepository
    {
        Task<MenuViewModel> GetMenuAsync();
        Task<Dish> GetDishAsync(int dishId);
        Task<bool> OrderAsync(CartViewModel model, ApplicationUser user);
        CartViewModel CreateViewModel(List<Dish> cart, ApplicationUser result);
    }
}
