namespace Algo96.dto.Product
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
    }
}
