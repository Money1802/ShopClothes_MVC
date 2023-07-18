using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace code_web_banhang.Models
{
    public class Giohang
    {
        Models.QLQUANAODataContext db = new QLQUANAODataContext();
        public HangHoa ThongTinAo { get; set; }
        public int SL { get; set; }
        public double ThanhTien { get => (double)(SL * ThongTinAo.DonGia); }
        public Giohang(int idAo)
        {
            ThongTinAo = db.HangHoas.Single(s => s.MaHH == idAo);
            SL = 1;
        }
    }
}