using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tomasos.Models.CartViewModels
{
    public class Dish
    {
        public int DishId { get; set; }

        [Required(ErrorMessage = "A name is required.")]
        [StringLength(100, ErrorMessage = "The name must be longer than {2} and less than {1}.", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "A description is required.")]
        [StringLength(100, ErrorMessage = "The description must be longer than {2} and less than {1}.", MinimumLength = 1)]
        public string Description { get; set; }

        [Required(ErrorMessage = "A price is required.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter a value bigger than {1}.")]
        public int Price { get; set; }

        [Display(Name = "Type")]
        public string DishType { get; set; }

        public int Quantity { get; set; }

        public List<string> IngredientsList { get; set; }

        [Display(Name = "Ingredients")]
        [Required(ErrorMessage = "Ingredients are required.")]
        public string IngredientsString { get; set; }

        public Dish()
        {
            IngredientsList = new List<string>();
            Quantity = 0;
        }

    }
}
