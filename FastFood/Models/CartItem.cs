using FastFood.Models;

namespace FastFood.Models
{
    public class CartItem
    {
        public Product Product { get; set; }  // Sản phẩm
        public int Quantity { get; set; }      // Số lượng
    }
}