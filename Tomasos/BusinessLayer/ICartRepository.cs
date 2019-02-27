using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos.Models.CartViewModels;
using Tomasos.Models.Identity;

namespace Tomasos.BusinessLayer
{
    public interface ICartRepository
    {
        Task<MenuViewModel> GetMenuAsync();
        Task<Matratt> GetDish(int dishId);
    }
}
