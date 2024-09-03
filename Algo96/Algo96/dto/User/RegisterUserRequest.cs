using Algo96.EF.DAL;

namespace Algo96.dto.User
{
    public class RegisterUserRequest
    {
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
