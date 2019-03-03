using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tomasos.Models.AdminViewModels;
using Tomasos.Services;

namespace Tomasos.Views.ViewComponents
{
    public class ManageOrdersViewComponent : ViewComponent
    {
        private readonly ITomasosService _dbService;

        public ManageOrdersViewComponent(ITomasosService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new OrdersViewModel();
            var orders = await _dbService.GetAllOrdersAsync();
            foreach (var order in orders)
            {
                var o = new Order();
                o.CustomerOrder = order;
                o.Customer = order.Kund;
                o.Delivered = order.Levererad;
                o.KundId = order.KundId;
                o.BestallningsId = order.BestallningId;

                model.Orders.Add(o);
            }
            return View(model);
        }
    }
}
