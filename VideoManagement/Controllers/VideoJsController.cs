using System.Web.Mvc;

namespace VideoManagement.Controllers
{
    public class VideoJsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "VideoJs Player";
            return View();
        }
    }
}