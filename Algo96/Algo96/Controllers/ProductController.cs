using Algo96.dto.Product;
using Algo96.EF;
using Algo96.EF.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Algo96.Controllers
{
    public class ProductController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        ApplicationDbContext db;
        public ProductController(ApplicationDbContext context, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            db = context;
        }
        /// <summary>
        /// Получить все продукты
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/product")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(db.Products.Include(p => p.Category).ToList());
        }
        /// <summary>
        /// Получить продукт по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/product/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = db.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            return Ok(product);
        }
        
        /// <summary>
        /// Добавить новый продукт
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/product")]
        public IActionResult CreateProduct(CreateProductRequest dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Image = UploadFile(dto.Image).Result,
                CategoryId = dto.CategoryId,
                Description = dto.Description,
            };
            db.Products.Add(product);
            db.SaveChanges();
            return Ok(product.Id);
        }
        /// <summary>
        /// Добавить категорию
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/category")]
        public async Task<IActionResult> CreateCategory(string title)
        {
            var category = new Category { Title = title };
            db.Categories.Add(category);
            db.SaveChanges();
            return Ok(category.Id);
        }


        /// <summary>
        /// Получить названия категорий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/category")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = db.Categories.ToList();
            return Ok(categories);
        }

        /// <summary>
        /// Удалить категорию по id
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("/category/{id}")]
        public async Task<IActionResult> DeleteCategories(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return Ok("category remove");
        }

        public async Task<string> UploadFile(IFormFile dto)
        {
            string path = "";
            IFormFile image = dto;
            if (image != null)
            {
                var uploadPath = $"{Directory.GetCurrentDirectory()}/Files";
                // создаем папку для хранения файлов
                Directory.CreateDirectory(uploadPath);

                string fullPath = $"{uploadPath}/{image.FileName}";

                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream( fullPath, FileMode.Create))
                {
                    image.CopyToAsync(fileStream);
                }
            }
            return image.FileName;
        }
    }
}
