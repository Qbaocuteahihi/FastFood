using Microsoft.EntityFrameworkCore;

namespace FastFood.Models  // Thay đổi namespace theo dự án của bạn
{
    public class FastFoodContext : DbContext
    {
        public FastFoodContext(DbContextOptions<FastFoodContext> options) : base(options)
        {
        }

        // Định nghĩa DbSet cho bảng Product
        public DbSet<Product> Products { get; set; }
    }
}