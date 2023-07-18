using code_web_banhang.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace code_web_banhang.Areas.Admin.Controllers
{
    public class HangHoasController : Controller
    {
        // GET: Admin/HangHoas
        QLQUANAODataContext db = new QLQUANAODataContext();
        public ActionResult ShowSP()
        {
            var lisTimKiem = db.HangHoas.ToList();
            return PartialView(lisTimKiem);
        }

        // Chi Tiết chihr sửa thêm xóa Sản phẩm
        public ActionResult DeleteSP(int ma)
        {
            HangHoa Clother = db.HangHoas.Single(s => s.MaHH == ma);
            if (Clother == null)
            {
                return HttpNotFound();
            }
            return PartialView(Clother);
        }
        [HttpPost, ActionName("DeleteSP")]
        public ActionResult DeleteSPTT(int ma)
        {
            HangHoa Clother = db.HangHoas.Single(s => s.MaHH == ma);
            if (Clother == null)
            {
                return HttpNotFound();
            }

            db.HangHoas.DeleteOnSubmit(Clother);
            db.SubmitChanges();
            return RedirectToAction("ShowSP");
        }

        public ActionResult CreateSP()
        {
            ViewBag.MALOAI = new SelectList(db.Loais, "MALOAI", "TENLOAI");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateSP(HangHoa ao, HttpPostedFileBase uploadFile)
        {
            if (ModelState.IsValid)
            {

                if (uploadFile != null && uploadFile.ContentLength > 0)
                {
                    var _FileName = Path.GetFileName(uploadFile.FileName);
                    var _path = Path.Combine(Server.MapPath("~/Images/"), _FileName);

                    uploadFile.SaveAs(_path);
                    ao.Hinh = _FileName;
                }
                db.HangHoas.InsertOnSubmit(ao);
                db.SubmitChanges();
                return RedirectToAction("ShowSP");

            }
            ViewBag.Message = "không thành công!!";
            return View(ao);
        }

        public ActionResult EditSP(int? ma)
        {

            var Ao = db.HangHoas.FirstOrDefault(s => s.MaHH == ma);
            if (ma == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Ao == null)
            {
                ViewBag.SEdit = "Không có Áo!";
            }
            ViewBag.MALOAI = new SelectList(db.Loais, "MALOAI", "TENLOAI", Ao.MaLoai);

            return View(Ao);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditSP(HangHoa ao, HttpPostedFileBase uploadFile, FormCollection form, int? ma)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadFile != null && uploadFile.ContentLength > 0)
                    {
                        var _FileName = Path.GetFileName(uploadFile.FileName);
                        var _path = Path.Combine(Server.MapPath("~/Images/"), _FileName);

                        uploadFile.SaveAs(_path);
                        ao.Hinh = _FileName;
                    }
                    HangHoa S = db.HangHoas.FirstOrDefault(s => s.MaHH == ma);

                    S.TenHH = ao.TenHH;
                    S.MaLoai = ao.MaLoai;
                    S.DonVi = ao.DonVi;
                    S.DonGia = ao.DonGia;
                    S.Hinh = ao.Hinh;
                    S.MoTa = ao.MoTa;
                    S.SoLuong = ao.SoLuong;
                    S.ChatLieu = ao.ChatLieu;

                    db.SubmitChanges();
                    return RedirectToAction("ShowSP");
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }

                return RedirectToAction("ShowSP");
            }

            return View(ao);
        }
        public ActionResult DetailsSP(int? ma)
        {

            var Ao = db.HangHoas.FirstOrDefault(s => s.MaHH == ma);
            if (ma == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Ao == null)
            {
                ViewBag.SEdit = "Không có Áo!";
            }
            ViewBag.MALOAI = new SelectList(db.Loais, "MALOAI", "TENLOAI", Ao.MaLoai);

            return View(Ao);
        }

        public ActionResult TimKiem(String search)
        {
            var model = db.HangHoas.Where(p => p.TenHH.Contains(search));
            return View(model);
        }

        public ActionResult Chaomung()
        {
            return View();
        }
    }
}