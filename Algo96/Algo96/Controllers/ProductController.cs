using Algo96.EF;
using Microsoft.AspNetCore.Mvc;

namespace Algo96.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db;
        public ProductController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return Ok(db.Products.ToList());
        }
    }
}
