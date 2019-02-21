using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tomasos.Models.Identity;

namespace Tomasos.Models.CartViewModels
{
    public class MenuViewModel
    {
        public List<Matratt> Pizzas { get; set; }

        public List<Matratt> Sallads { get; set; }

        public List<Matratt> Pasta { get; set; }
    }
}
