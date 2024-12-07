using FastFood.Models;

namespace FastFood.Services
{
    public interface IProductService
    {
        Product GetProductById(int id);
        // Thêm các phương thức khác nếu cần
    }
}