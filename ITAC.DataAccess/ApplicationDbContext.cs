using ITAC.Models;
using Microsoft.EntityFrameworkCore;

namespace ITAC.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Importer> Importers { get; set; } = default!;
    }
}