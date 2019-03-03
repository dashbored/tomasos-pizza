using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos.Models.Identity;

namespace Tomasos.Services
{
    public interface ITomasosService
    {
        Task<Kund> GetCustomerAsync(string id);
        Task<bool> UpdateCustomerAsync(Kund customer);
        Task<bool> AddNewCustomerAsync(Kund customer);
        Task<bool> AddNewOrderAsync(Bestallning order);
        Task<List<AspNetUsers>> GetAllUsersAsync();
        Task<List<Matratt>> GetMattraterAsync();
        Task<Matratt> GetMatrattAsync(int id);
        Task<List<Produkt>> GetIngredientsAsync(int matrattId);
        Task<Produkt> GetIngredientFromNameAsync(string ingredientName);
        Task<bool> AddNewIngredientAsync(string ingredientName);
        Task<bool> AddNewDishAsync(Matratt dish);
        Task<bool> UpdateDishAsync(Matratt dish);
        Task<List<Bestallning>> GetAllOrdersAsync();
        Task<Bestallning> GetOrderFromIdAsync(int bestallningsId);

        Task<bool> SaveChangesAsync();

    }
}
