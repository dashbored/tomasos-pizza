using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos.Models.Identity;

namespace Tomasos.Models.AdminViewModels
{
    public class Order
    {
        public Bestallning CustomerOrder { get; set; }

        public Kund Customer { get; set; }

        public bool Delivered { get; set; }

        public int KundId { get; set; }

        public int BestallningsId { get; set; }

    }
}
