using System;

namespace Algo96.EF.DAL
{
    public enum Status
    {
        Created,
        Approved,
        Completed
    }
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Status Status { get; set; }
    }
}
