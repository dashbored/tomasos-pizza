using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tomasos.Models.AdminViewModels
{
    public class OrdersViewModel
    {
        public List<Order> Orders { get; set; }

        public OrdersViewModel()
        {
            Orders = new List<Order>();
        }
    }
}
