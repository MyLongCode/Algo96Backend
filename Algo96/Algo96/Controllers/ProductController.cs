using Algo96.EF;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Algo96.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db;
        public ProductController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        [Route("/product")]
        public IActionResult GetProducts()
        {
            return Ok(db.Products.ToList());
        }
    }
}
