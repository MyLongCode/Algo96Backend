namespace Algo96.Models
{
    public class Group
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public IEnumerable<User> Users { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime DateTime { get; set; }
        public Place Place { get; set; }
    }
}
