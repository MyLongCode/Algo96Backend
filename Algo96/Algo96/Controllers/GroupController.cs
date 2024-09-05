using Algo96.dto.Group;
using Algo96.EF;
using Algo96.EF.DAL;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [Route("/group")]
        public async Task<IActionResult> GetGroups()
        {
            return Ok(db.Groups.ToList());
        }

        [HttpGet]
        [Route("/group/{id}")]
        public async Task<IActionResult> GetGroupById (int id)
        {
            return Ok(db.Groups.Find(id));
        }

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
            return Ok(group);

        }
    }
}
