using System.Collections.Generic;
using System.Linq;

namespace FastFood.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>(); // Danh sách sản phẩm trong giỏ hàng

        public void AddToCart(Product product, int quantity)
        {
            var cartItem = Items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (cartItem == null)
            {
                Items.Add(new CartItem { Product = product, Quantity = quantity });
            }
            else
            {
                cartItem.Quantity += quantity;
            }
        }

        public void RemoveFromCart(int productId)
        {
            var cartItem = Items.FirstOrDefault(i => i.Product.Id == productId);
            if (cartItem != null)
            {
                Items.Remove(cartItem);
            }
        }

        public decimal TotalPrice()
        {
            return Items.Sum(i => i.Product.Price * i.Quantity);
        }

        public List<CartItem> GetItems()
        {
            return Items;
        }
        public decimal GetTotal()
        {
            return Items.Sum(item => item.Product.Price * item.Quantity); // Tổng tiền bằng VND
        }
        public void ClearCart()
        {
            Items.Clear();
        }
    }
}