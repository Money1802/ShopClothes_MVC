using code_web_banhang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace code_web_banhang.Areas.Admin.Controllers
{
    public class HoaDonsController : Controller
    {
        QLQUANAODataContext db = new QLQUANAODataContext();
        // GET: Admin/HoaDons
        public ActionResult Index()
        {
            var lisTimKiem = db.HoaDons.ToList();
            return PartialView(lisTimKiem);
        }
    }
}