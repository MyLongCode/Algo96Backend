using System.Collections.Generic;

namespace Algo96.EF.DAL
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Images { get; set; }
        public string Desctription { get; set; }
        public int Price { get; set; }
    }
}
