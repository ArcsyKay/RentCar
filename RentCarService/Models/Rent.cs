using System;

namespace RentCarService.Models
{
    public class Rent
    {
        public int RentId { get; set; }
        public int CarId { get; set; }
        public DateTime DateOfRentFrom { get; set; }
        public DateTime DateOfRentTo { get; set; }
        public double TotalCost { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Car Car { get; set; }
    }
}