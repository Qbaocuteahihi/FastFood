using FastFood.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class HomeController : Controller
{
    private readonly FastFoodContext _context;

    public HomeController(FastFoodContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Hiển thị trang chính
    public IActionResult Index()
    {
        return View(); // Trả về view Index
    }

    // Hiển thị sản phẩm theo danh mục
    public IActionResult Category(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
        {
            return BadRequest("Danh mục không hợp lệ.");
        }

        var products = _context.Products
            .Where(p => p.Category == category)
            .ToList();

        return View(products);
    }
}