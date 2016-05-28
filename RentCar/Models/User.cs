using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RentCar.Models
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public string Address { get; set; }
        public string City { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext() : base(@"Data Source=W-SEK\THIRD;Initial Catalog=RentCar;Integrated Security=True;MultipleActiveResultSets=True") { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rent> Rents { get; set; }
    }
}