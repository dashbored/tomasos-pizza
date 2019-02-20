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
    }
}
