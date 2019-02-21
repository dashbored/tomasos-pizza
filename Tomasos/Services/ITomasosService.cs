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
        Task<List<AspNetUsers>> GetAllUsersAsync();
        Task<List<Matratt>> GetDishesAsync();
        Task<Matratt> GetDishAsync(int id);
        Task<List<Produkt>> GetIngredientsAsync(int id);
    }
}
