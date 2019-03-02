using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tomasos.Models.CartViewModels
{
    public class CartViewModel
    {
        public List<Dish> Dishes { get; set; }

        public int BonusPoints { get; set; }

        public int TotalPrice { get; set; }

        public int PremiumPrice { get; set; }

        public int NumberOfItems { get; set; }

        public CartViewModel()
        {
            Dishes = new List<Dish>();
        }
    }
}
