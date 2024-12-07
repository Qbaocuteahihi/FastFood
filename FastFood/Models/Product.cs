using System;
namespace FastFood.Models
{
    public class Product
    {
        public int Id { get; set; }  // Mã sản phẩm
        public string Name { get; set; }  // Tên sản phẩm
        public string Description { get; set; }  // Mô tả sản phẩm
        public decimal Price { get; set; }  // Giá sản phẩm
        public int Stock { get; set; }  // Số lượng tồn kho
        public string Category { get; set; }  // Danh mục sản phẩm
        public string ImageUrl { get; set; }  // Đường dẫn hình ảnh
    }
}
