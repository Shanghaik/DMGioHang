using System.ComponentModel.DataAnnotations;

namespace DMGioHang.Models
{
    public class SanPham
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public virtual List<GHCT>? GHCT { get; set; }
    }
}
