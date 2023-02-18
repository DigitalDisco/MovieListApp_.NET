using Microsoft.EntityFrameworkCore;
using MovieCrudApp.Models;

namespace MovieCrudApp.Data
{
    public class ShowContext : DbContext
    {
        public DbSet<Show> Shows { get; set; }
        public ShowContext(DbContextOptions<ShowContext> options) : base(options)
        {
        }
    }
}
