using DMGioHang.Models;
using Microsoft.AspNetCore.Mvc;

namespace DMGioHang.Controllers
{
    public class SanPhamController : Controller
    {
        AppDbContext context = new AppDbContext();
        public SanPhamController()
        {
        }
        public IActionResult Index()
        {
            var data = context.SanPhams.ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SanPham sp)
        {
            context.SanPhams.Add(sp);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult AddToCart(Guid id, int amount) // id này là id của sản phẩm
        {
            // Thực hiện việc kiểm tra xem đã đunk nhặp chưa
            var logindata = HttpContext.Session.GetString("user");
            if (logindata == null) return Content("Đã đặng nhập đâu mà đòi thêm thắt cái gì?");
            else
            {
                // Lấy ra tất cả sản phẩm trong giỏ hàng của khách hàng vừa đăng nhập
                var userCart = context.GHCTs.Where(p => p.IdGH == Guid.Parse(logindata)).ToList();
                bool checkSelected = false; Guid idGHCT = Guid.Empty;
                foreach (var item in userCart){
                    if (item.IdSP == id)
                    {
                        // Nếu ID sản phẩm trong giỏ hàng của User đã trùng với cái đang được trọn
                        checkSelected = true; 
                        idGHCT = item.Id; // Lấy ra cái ID của GHCT để tẹo nữa ta update
                        break;
                    }
                }
                if (!checkSelected) // Nếu sản phẩm chưa từng được chọn
                {
                    // tạo ra 1 GHCT ứng với Sản phẩm đó
                    GHCT ghct = new GHCT() { Id = Guid.NewGuid(), IdSP = id, IdGH = Guid.Parse(logindata), Amount = amount };
                    context.GHCTs.Add(ghct); context.SaveChanges();
                    return RedirectToAction("Index");
                }else // Nếu SP đã được chọn
                {
                    var ghctUpdate = context.GHCTs.Find(idGHCT);
                    ghctUpdate.Amount = ghctUpdate.Amount + amount;
                    context.GHCTs.Update(ghctUpdate); context.SaveChanges(); // Lưu lại
                    return RedirectToAction("Index");
                }
                
            }
        }
    }
}
