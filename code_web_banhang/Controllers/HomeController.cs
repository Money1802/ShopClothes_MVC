using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using code_web_banhang.Models;

namespace code_web_banhang.Controllers
{
    public class HomeController : Controller
    {
        QLQUANAODataContext db = new QLQUANAODataContext();
        // GET: Home
        public ActionResult Index()
        {
            var model = db.HangHoas.Where(p => p.MAKM.Value != null).Take(6).OrderBy(p => p.MaHH);
            ViewBag.Tasks = model;

            var model2 = db.HangHoas.Take(6).OrderByDescending(p => p.MaHH);
            ViewBag.Tasks2 = model2;

            return View();
        }

        public ActionResult Loai()
        {
            var model = db.Loais.OrderByDescending(p => p.TenLoai);
            return PartialView(model);
        }
        public ActionResult Info()
        {
            return View();
        }
        public ActionResult Contacts()
        {
           
            return View();
        }
    }
}
