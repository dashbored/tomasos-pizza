using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos.Models.AccountViewModels;
using Tomasos.Models.Identity;

namespace Tomasos.BusinessLayer
{
    public class UserRepository : IUserRepository
    {
        public ManageViewModel UpdateModel(Kund customer, ManageViewModel model)
        {
            model.FirstName = customer.Namn.Split(' ').First();
            model.LastName = customer.Namn.Split(' ').Last();
            model.StreetAddress = customer.Gatuadress;
            model.PostNumber = customer.Postnr;
            model.PostArea = customer.Postort;
            model.PhoneNumber = customer.Telefon;
            model.IdentityId = customer.IdentityId;
            model.Email = customer.Email;

            return model;
        }

        public Kund UpdateCustomer(Kund customer, ManageViewModel model)
        {
            customer.Namn = model.FirstName + " " + model.LastName;
            customer.Gatuadress = model.StreetAddress;
            customer.Postnr = model.PostNumber;
            customer.Postort = model.PostArea;
            customer.Telefon = model.PhoneNumber;
            customer.IdentityId = model.IdentityId;
            customer.Email = model.Email;
            return customer;
        }
    }
}
