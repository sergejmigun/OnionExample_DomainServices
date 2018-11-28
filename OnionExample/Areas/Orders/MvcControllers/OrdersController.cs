using System.Web.Mvc;
using OnionExample.Areas.Orders.ViewModels;

namespace OnionExample.Areas.Orders.MvcControllers
{
    public class OrdersController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View(new OrdersViewModel());
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return this.View(new OrdersDetailsViewModel { OrderId = id });
        }
    }
}