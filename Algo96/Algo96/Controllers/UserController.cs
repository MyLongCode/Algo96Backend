using Algo96.EF.DAL;
using Algo96.EF;
using Algo96.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Algo96.dto.User;

namespace Algo96.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db;
        public UserController(ApplicationDbContext context)
        {
            db = context;
        }
        /// <summary>
        /// Зарегать пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/user/register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest dto)
        {
            var userFound = db.Users.FirstOrDefault(u => u.Login == dto.Login);
            if (userFound != null) return BadRequest("user with this login is already register");
            var user = new User
            {
                Login = dto.Login,
                Password = dto.Password,
                FullName = dto.FullName,
            };
            dto.Role = dto.Role.ToLower();
            if (dto.Role == "admin") user.Role = Role.Admin;
            if (dto.Role == "teacher") user.Role = Role.Teacher;
            else user.Role = Role.Student;
            db.Users.Add(user);
            db.SaveChanges();

            return Ok("user created");
        }

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/user")]
        public async Task<IActionResult> GetAllUsers()
        {
            User[] users = db.Users.ToArray();
            if (users == null)
                return BadRequest("users undefined");
            return Ok(users);
        }


        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User? user = db.Users.FirstOrDefault(x => x.Login == username && x.Password == password);
            if ((user == null) == false)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }

        /// <summary>
        /// Получить токен
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("/user/token")]
        public async Task<IActionResult> Token(GetUserTokenRequest dto)
        {
            var identity = GetIdentity(dto.Login, dto.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid email or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return Ok(response);
        }
    }
}
