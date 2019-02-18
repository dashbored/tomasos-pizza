using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos.Models.AccountViewModels;
using Tomasos.Models.Identity;

namespace Tomasos.BusinessLayer
{
    public interface IUserRepository
    {
        Kund UpdateCustomer(Kund customer, ManageViewModel model);
        ManageViewModel UpdateModel(Kund customer, ManageViewModel model);
    }
}
