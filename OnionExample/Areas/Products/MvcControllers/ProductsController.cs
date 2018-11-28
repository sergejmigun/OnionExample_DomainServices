using System.Web.Mvc;
using OnionExample.Areas.Products.ViewModels;

namespace OnionExample.Areas.Products.MvcControllers
{
    public class ProductsController: Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View(new ProductsViewModel());
        }
    }
}