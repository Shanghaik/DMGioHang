using DMGioHang.Models;
using Microsoft.AspNetCore.Mvc;

namespace DMGioHang.Controllers
{
    public class GHCTController : Controller
    {
        AppDbContext context = new AppDbContext();
        public GHCTController()
        {

        }
        public IActionResult Index()
        {
            var alldata = context.GHCTs.ToList();
            return View(alldata);
        }
    }
}
