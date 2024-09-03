using Algo96.EF;
using Algo96.EF.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Algo96.Controllers
{
    public class CourseController : Controller
    {
        private ApplicationDbContext db;
        public CourseController(ApplicationDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Получить все курсы
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/course")]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(db.Courses.ToList());
        }

        /// <summary>
        /// Получить курс по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/course/{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            return Ok(db.Courses.Find(id));
        }

        /// <summary>
        /// Добавить курс
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/course")]
        public async Task<IActionResult> CreateCourse(string title)
        {
            var course = new Course { Name = title };
            db.Courses.Add(course);
            db.SaveChanges();
            return Ok(course.Id);
        }

        /// <summary>
        /// Удалить курс по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/course/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return Ok(course.Id);
        }
    }
}
