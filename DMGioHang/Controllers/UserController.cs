using DMGioHang.Models;
using Microsoft.AspNetCore.Mvc;

namespace DMGioHang.Controllers
{
    public class UserController : Controller
    {
        AppDbContext context = new AppDbContext();
        public UserController()
        {
        }
        public IActionResult Index()
        {
            var data = context.Users.ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            
            var giohang = new GioHang() {
                Id = user.Id, Status = 1
            };
            context.Users.Add(user);
            context.GioHangs.Add(giohang);  // Khi tạo user ngay lập tức tạo 1 giỏ hàng đi kèm
            context.SaveChanges();
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string name, string password)
        {
            var loginData = context.Users.FirstOrDefault(x => x.Name == name && x.Pass == password);
            if (loginData == null) return Content("Đunk nhập 7 bại");
            else
            {
                HttpContext.Session.SetString("user", loginData.Id.ToString());
                return RedirectToAction("Index", "SanPham");
            }
        }
    }
}
