using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using code_web_banhang.Models;

namespace code_web_banhang.Controllers
{
    public class SanphamController : Controller
    {
        // GET: Sanpham
        QLQUANAODataContext db = new QLQUANAODataContext();
        public ActionResult ShowSpAll(int page = 1, string SortColumn = "MaHH", string IconClass= "fa-sort-asc")
        {
            List<HangHoa> hangHoas = db.HangHoas.ToList();

            //sort
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if (SortColumn == "DonGia")
            {
                if(IconClass == "fa-sort-asc")
                {
                    hangHoas = hangHoas.OrderBy(row => row.DonGia).ToList();
                }
                else
                {
                    hangHoas = hangHoas.OrderByDescending(row => row.DonGia).ToList();
                }
            }
            else if(SortColumn == "MaHH")
            {
                if (IconClass == "fa-sort-asc")
                {
                    hangHoas = hangHoas.OrderBy(row => row.MaHH).ToList();
                }
                else
                {
                    hangHoas = hangHoas.OrderByDescending(row => row.MaHH).ToList();
                }
            }

            //paging
            int NoOfRecordPerPage = 9;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(hangHoas.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPage = NoOfPages;
            hangHoas = hangHoas.Skip(NoOfRecordSkip).Take(NoOfRecordPerPage).ToList();

            return View(hangHoas);

        }
        public ActionResult Details(int id)
        {
            var Quanao = db.HangHoas.Single(t => t.MaHH == id);
            if (Quanao == null)
            {
                return HttpNotFound();
            }
            return View(Quanao);
        }


        public ActionResult AotheoLoai(int Maloai)
        {
            var listAo = db.HangHoas.Where(s => s.MaLoai == Maloai).ToList();

            if (listAo.Count == 0)
            {
                ViewBag.TB = "Khong tim thay";
            }
            else
            {
                string ten = db.Loais.Where(s => s.MaLoai == Maloai).ToString();
                ViewBag.LoaiAo = ten;
            }
            return View(listAo);
        }


        public ActionResult TimKiem(String search)
        {
            var model = db.HangHoas.Where(p => p.TenHH.Contains(search));
            if (model == null)
            {
                ViewBag.TB = "khong tim thay";
            }    
            return PartialView(model);
        }
    }
}