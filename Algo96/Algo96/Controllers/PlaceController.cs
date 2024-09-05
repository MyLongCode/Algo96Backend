using Algo96.EF.DAL;
using Algo96.EF;
using Microsoft.AspNetCore.Mvc;

namespace Algo96.Controllers
{
    public class PlaceController : Controller
    {
        private ApplicationDbContext db;
        public PlaceController(ApplicationDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Получить все площадки
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/place")]
        public async Task<IActionResult> GetPlaces()
        {
            return Ok(db.Places.ToList());
        }

        /// <summary>
        /// Получить площадку по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/place/{id}")]
        public async Task<IActionResult> GetPlaceById(int id)
        {
            return Ok(db.Places.Find(id));
        }

        /// <summary>
        /// Добавить площадку
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/place")]
        public async Task<IActionResult> CreatePlace(string title)
        {
            var place = new Place { Name = title };
            db.Places.Add(place);
            db.SaveChanges();
            return Ok(place.Id);
        }

        /// <summary>
        /// Удалить площадку по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/place/{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            var place = db.Places.Find(id);
            db.Places.Remove(place);
            db.SaveChanges();
            return Ok(place.Id);
        }
    }
}
