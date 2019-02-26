using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tomasos.Models.CartViewModels
{
    public class Dish
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string DishType { get; set; }

        public List<string> IngredientsList { get; set; }
        public string IngredientsString { get; set; }

        public Dish()
        {
            IngredientsList = new List<string>();
        }

    }
}
