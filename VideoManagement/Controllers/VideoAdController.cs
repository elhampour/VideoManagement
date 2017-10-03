using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Linq;
using System.Web.Mvc;
using VideoManagement.Models;

namespace VideoManagement.Controllers
{
    public class VideoAdController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Video Ads Management";
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create video ad";
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateVideoAdVm vm)
        {
            ViewBag.Title = "Create video ad";
            if (!ModelState.IsValid)
            {
                return View();
            }

            var videoAd = new VideoAd();
            videoAd.Path = 

            var guid = Guid.NewGuid();
            var serverPath = Server.MapPath("~/adssample");
            var filePath = string.Format("{0}\\{1}", serverPath, "ads2.xml");

            var vastCreationResult = vm.CreateVast(filePath);

            if (vastCreationResult.VastCreationResultType == VastCreationResultType.Failure)
            {
                ModelState.AddModelError(nameof(vm.VastVersion), vastCreationResult.Message);
                return View();
            }

            // aadd 

            return RedirectToAction("Index");
        }

        public ActionResult Get([DataSourceRequest] DataSourceRequest request)
        {
            var db = new VideoManagementDbContext();
            var data = db.VideoAds.Select(a => new VideoAdVm
            {
                Id = a.Id,
                Title = a.Title
            }).ToDataSourceResult(request);
            return Json(data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Remove([DataSourceRequest] DataSourceRequest request, VideoAdVm videoAdVm)
        {
            if (videoAdVm != null)
            {
                var db = new VideoManagementDbContext();
                var videoad = db.VideoAds.Where(a => a.Id == videoAdVm.Id).FirstOrDefault();
                db.VideoAds.Remove(videoad);
                db.SaveChanges();
            }

            return Json(new[] { videoAdVm }.ToDataSourceResult(request, ModelState));
        }
    }
}