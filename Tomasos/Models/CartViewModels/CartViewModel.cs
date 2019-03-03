using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tomasos.Models.CartViewModels
{
    public class CartViewModel
    {
        public List<Dish> Dishes { get; set; }

        public int DishId { get; set; }

        public Dish Dish { get; set; }

        public SelectList DishList { get; set; }

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
