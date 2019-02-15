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
        bool AddNewCustomer(Kund customer);
    }
}
