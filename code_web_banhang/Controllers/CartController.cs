using code_web_banhang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace code_web_banhang.Controllers
{
    public class CartController : Controller
    {
        QLQUANAODataContext db = new QLQUANAODataContext();
        public List<Giohang> LayGioHang()
        {
            List<Giohang> lstGH = Session["GioHang"] as List<Giohang>;
            if (lstGH == null)
            {
                lstGH = new List<Giohang>();
                Session["GioHang"] = lstGH;
            }
            return lstGH;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var lst = Session["GioHang"] as List<Giohang>;
            if (lst == null)
            {
                ViewBag.TongCong = 0;
                ViewBag.TongTien = 0;
            }
            else
            {
                ViewBag.TongCong = lst.Sum(x => x.SL);
                ViewBag.TongTien = lst.Sum(x => x.ThanhTien);
            }

            return View(LayGioHang());
        }
        public ActionResult ThemGioHang(int idAo, string strURL)
        {
            List<Giohang> lstGH = LayGioHang();
            Giohang sp = lstGH.Find(s => s.ThongTinAo.MaHH == idAo);
            if (sp == null)
            {
                sp = new Giohang(idAo);
                lstGH.Add(sp);
                return Redirect(strURL);
            }
            else
            {
                sp.SL++;
                return Redirect(strURL);
            }
        }
        public ActionResult GioHangPartial()
        {
            Session["SoLuong"] = LayGioHang().Sum(x => x.SL);
            return PartialView();
        }
        public ActionResult XoaGioHang(int id)
        {
            var lstGioHang = LayGioHang();
            Giohang sp = lstGioHang.Single(s => s.ThongTinAo.MaHH.Equals(id));
            if (sp != null)
            {
                lstGioHang.RemoveAll(x => x.ThongTinAo.MaHH.Equals(id));
                return RedirectToAction("Index", "Cart");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("ShowSpAll", "Sanpham");
            }
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult XoaAllGioHang()
        {
            LayGioHang().Clear();
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult CapNhatGioHang(int id, int sl)
        {
            Giohang sp = LayGioHang().Single(s => s.ThongTinAo.MaHH.Equals(id));
            if (sp != null)
            {
                sp.SL = sl;
            }
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult thongBaoDatHangThanhCong()
        {
            return View();
        }

        public ActionResult thongTinDatHang()
        {
            return View();
        }


        public ActionResult XLthongTinDatHang(HoaDon hd)
        {
            db.HoaDons.InsertOnSubmit(hd);
            db.SubmitChanges();

            return RedirectToAction("Cart");
        }
    }
}