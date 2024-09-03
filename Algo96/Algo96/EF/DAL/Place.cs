using System.Collections.Generic;

namespace Algo96.EF.DAL
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Group> Groups { get; set; }
    }
}
