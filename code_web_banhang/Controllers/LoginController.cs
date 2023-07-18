using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using code_web_banhang.Models;

namespace code_web_banhang.Controllers
{
    public class LoginController : Controller
    {
        QLQUANAODataContext db = new QLQUANAODataContext();
        // GET: Login
        public ActionResult FormLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authen(TaiKhoan tk)
        {
            var check = db.TaiKhoans.Where(s => s.TenTaiKhoan.Equals(tk.TenTaiKhoan) && s.MatKhau.Equals(tk.MatKhau)).FirstOrDefault();
            if (check == null)
            {
                return RedirectToAction("ShowLoi", "Home");
            }
            else
            {
                return RedirectToAction("Chaomung", "HangHoas", new { area = "Admin" });
            }
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }
    }
}