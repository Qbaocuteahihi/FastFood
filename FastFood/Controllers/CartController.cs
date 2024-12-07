using Microsoft.AspNetCore.Mvc;
using FastFood.Models;

public class CartController : Controller
{
    private readonly FastFoodContext _context;

    public CartController(FastFoodContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var cart = GetCart();
        return View(cart); // Trả về view giỏ hàng
    }

    public IActionResult AddToCart(int productId, int quantity)
    {
        var product = _context.Products.Find(productId);
        if (product == null)
        {
            return NotFound("Sản phẩm không tồn tại.");
        }

        if (quantity <= 0)
        {
            return BadRequest("Số lượng phải lớn hơn 0.");
        }

        var cart = GetCart();
        cart.AddToCart(product, quantity);
        HttpContext.Session.SetObjectAsJson("ShoppingCart", cart);

        return RedirectToAction("Index");
    }

    public IActionResult RemoveFromCart(int productId)
    {
        var cart = GetCart();
        if (cart == null)
        {
            return NotFound("Giỏ hàng không tồn tại."); // Hoặc xử lý logic khác
        }

        cart.RemoveFromCart(productId);
        HttpContext.Session.SetObjectAsJson("ShoppingCart", cart);

        return RedirectToAction("Index");
    }

    private ShoppingCart GetCart()
    {
        var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart");
        if (cart == null)
        {
            cart = new ShoppingCart(); // Khởi tạo giỏ hàng mới nếu chưa có
            HttpContext.Session.SetObjectAsJson("ShoppingCart", cart); // Lưu vào session
        }
        return cart;
    }

    [HttpPost]
    [HttpPost]
    public IActionResult Checkout(string paymentMethod)
    {
        var cart = GetCart();
        if (cart == null || !cart.GetItems().Any())
        {
            return NotFound("Giỏ hàng không tồn tại hoặc trống.");
        }

        foreach (var item in cart.GetItems())
        {
            var product = _context.Products.Find(item.Product.Id);
            if (product == null)
            {
                continue; // Hoặc thông báo lỗi cho người dùng
            }

            if (product.Stock >= item.Quantity)
            {
                product.Stock -= item.Quantity; // Trừ số lượng
                _context.Products.Update(product); // Cập nhật sản phẩm
            }
            else
            {
                ModelState.AddModelError("", $"Không đủ số lượng cho sản phẩm {product.Name}.");
                return View("ConfirmPayment", cart); // Quay lại view xác nhận với thông báo lỗi
            }
        }

        _context.SaveChanges(); // Lưu thay đổi

        // Xóa giỏ hàng sau khi thanh toán
        HttpContext.Session.Remove("ShoppingCart");

        ViewBag.Message = "Cảm ơn bạn đã thanh toán! Chúng tôi sẽ xử lý đơn hàng của bạn ngay.";
        return RedirectToAction("Index", "Home"); // Chuyển hướng về trang chính
    }
    [HttpPost]
    public IActionResult ProcessPayment(string paymentMethod)
    {
        var cart = GetCart();
        if (cart == null || !cart.GetItems().Any())
        {
            return NotFound("Giỏ hàng không tồn tại hoặc trống.");
        }

        // Hiển thị thông báo xác nhận
        ViewBag.PaymentMethod = paymentMethod;
        return View("ConfirmPayment", cart); // Chuyển đến view xác nhận
    }

    // Phương thức để lấy số lượng sản phẩm trong giỏ hàng
    public int GetCartItemCount()
    {
        var cart = GetCart();
        return cart.GetItems().Sum(item => item.Quantity);
    }
}