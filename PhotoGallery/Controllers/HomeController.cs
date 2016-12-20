using System.Web.Mvc;

namespace PhotoGallery.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("List", "Image");
        }
    }
}