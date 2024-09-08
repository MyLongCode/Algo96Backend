using Algo96.dto.Purchase;
using Algo96.EF;
using Algo96.EF.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Algo96.Controllers
{
    public class PurchaseController : Controller
    {
        private ApplicationDbContext db;
        public PurchaseController(ApplicationDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Получить все заказы
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/purchase")]
        public async Task<IActionResult> GetPurchase()
        {
            return Ok(db.Purchase.Include(p => p.User).Include(p => p.Product).ToList());
        }

        /// <summary>
        /// Создать новый заказ по id пользователя и id продукта 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/purchase")]
        public async Task<IActionResult> AddPurchase(CreatePurchaseRequest dto)
        {
            var purchase = new Purchase
            {
                UserId = dto.UserId,
                User = db.Users.Find(dto.UserId),
                Product = db.Products.Find(dto.ProductId),
                ProductId = dto.ProductId,
                CreatedDate = DateTime.Now,
            };
            db.Purchase.Add(purchase);
            db.SaveChanges();
            return Ok(purchase.Id);
        }
    }
}
