using Algo96.EF;
using Microsoft.AspNetCore.Mvc;

namespace Algo96.Controllers
{
    public class GroupController : Controller
    {
        private ApplicationDbContext db;
        public GroupController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
