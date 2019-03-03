using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tomasos.Models.AdminViewModels;

namespace Tomasos.Views.ViewComponents
{
    public class OrderViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(Order model)
        {
            return View(model);
        }
    }
}
