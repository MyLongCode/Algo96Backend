using Algo96.dto.Group;
using Algo96.EF;
using Algo96.EF.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Description;
using System.Xml.Linq;

namespace Algo96.Controllers
{
    public class GroupController : Controller
    {
        private ApplicationDbContext db;
        public GroupController(ApplicationDbContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Получить все группы
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/group")]
        public async Task<IActionResult> GetGroups()
        {
            db.SaveChanges();
            return Ok(db.Groups.ToList());
        }

        /// <summary>
        /// Получить группу по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/group/{id}")]
        [ResponseType(typeof(GetGroupResponse))]
        public async Task<IActionResult> GetGroupById (int id)
        {
            var group = db.Groups.Find(id);

            return Ok(new GetGroupResponse
            {
                Id = id,
                Course = group.Course.ToString(),
                DayOfWeek = group.DayOfWeek.ToString(),
                Place = group.Place.ToString()
            });
        }

        /// <summary>
        /// Создать группу
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/group")]

        public async Task<IActionResult> CreateGroup(CreateGroupRequest dto)
        {
            var group = new Group
            {
                Course = db.Courses.Find(dto.CourseId),
                Place = db.Places.Find(dto.PlaceId),
                DateTime = DateTime.Now,
                DayOfWeek = (EF.DAL.DayOfWeek)Enum.Parse(typeof(EF.DAL.DayOfWeek), dto.DayOfWeek, true)
            };
            db.Groups.Add(group);
            db.SaveChanges();
            return Ok(db.Groups.ToList());

        }
    }
}
