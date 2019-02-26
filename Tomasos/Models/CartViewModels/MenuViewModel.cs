using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos.Models.Identity;

namespace Tomasos.Models.CartViewModels
{
    public class MenuViewModel
    {
        public List<Dish> Pizzas { get; set; }

        public List<Dish> Sallads { get; set; }

        public List<Dish> Pasta { get; set; }

        public MenuViewModel()
        {
            Pizzas = new List<Dish>();
            Sallads = new List<Dish>();
            Pasta = new List<Dish>();
        }
    }
}
