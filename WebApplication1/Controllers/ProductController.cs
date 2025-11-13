using Microsoft.AspNetCore.Mvc;
using WebApplication1.AppData;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly AppDbContext _context;
        
        public ProductController(ILogger<ProductController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            Product product = new Product();
            product = _context.Products.Find(id);
            return View(product);
        }
       
    }
}