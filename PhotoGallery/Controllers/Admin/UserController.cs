using System.Web.Mvc;

namespace PhotoGallery.Controllers.Admin
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserPage()
        {
            return View();
        }
    }
}