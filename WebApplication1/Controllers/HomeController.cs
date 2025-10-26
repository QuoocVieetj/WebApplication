using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.AppData;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        
        List<Product> products = _context.Products.ToList(); // <-- lay danh sach
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult GetCategories()
    {
        try
        {
            var categories = _context.Categories.ToList();
            return Json(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading categories");
            return Json(new List<Category>());
        }
    }

    public IActionResult SeedCategories()
    {
        try
        {
            // Kiểm tra xem đã có categories chưa
            if (!_context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Áo thun", Description = "Các loại áo thun nam nữ" },
                    new Category { Name = "Quần jean", Description = "Quần jean nam nữ" },
                    new Category { Name = "Áo sơ mi", Description = "Áo sơ mi công sở" },
                    new Category { Name = "Váy", Description = "Váy đầm nữ" },
                    new Category { Name = "Giày dép", Description = "Giày dép nam nữ" }
                };

                _context.Categories.AddRange(categories);
                _context.SaveChanges();
            }

            return Json(new { success = true, message = "Categories seeded successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error seeding categories");
            return Json(new { success = false, message = ex.Message });
        }
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}