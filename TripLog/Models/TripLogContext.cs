using Microsoft.EntityFrameworkCore;

namespace TripLog.Models
{
    public class TripLogContext : DbContext
    {
        public TripLogContext(DbContextOptions<TripLogContext> options)
            : base(options)
        { }

        public DbSet<Trip> Trips { get; set; }
    }
}
