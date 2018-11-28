using System.Web.Mvc;
using OnionExample.Areas.Cart.ViewModels;

namespace OnionExample.Areas.Cart.MvcControllers
{
    public class CartController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View(new CartViewModel());
        }
    }
}