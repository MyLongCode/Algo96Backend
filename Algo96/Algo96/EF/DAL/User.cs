using System.Collections.Generic;

namespace Algo96.EF.DAL
{
    public enum Role
    {
        Admin,
        Teacher,
        Student
    }
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public List<Group> Groups { get; set; }
        public int Coins { get; set; }
    }
}
