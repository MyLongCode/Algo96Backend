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
            return Ok(db.Purchase.Include(p => p.User)
                .Include(p => p.Product)
                .Select(p => new PurchaseDTO
                {
                    Id = p.Id,
                    CreatedDate = p.CreatedDate,
                    User = new UserDTO
                    {
                        Id = p.User.Id,
                        FullName = p.User.FullName
                    },
                    Product = p.Product,
                    Status = p.Status.ToString()
                })
                .ToList());
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
                Status = Status.Created
            };
            db.Purchase.Add(purchase);
            db.SaveChanges();
            return Ok(purchase.Id);
        }
    }
}
