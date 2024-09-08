using Algo96.EF.DAL;
using System.Text.Json.Serialization;

namespace Algo96.dto.Purchase
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserDTO User { get; set; }
        public EF.DAL.Product Product { get; set; }
        public string Status { get; set; }
    }
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }

}
