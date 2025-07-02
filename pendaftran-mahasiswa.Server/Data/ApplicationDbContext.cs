using Microsoft.EntityFrameworkCore;
using pendaftran_mahasiswa.Server.Models;
namespace pendaftran_mahasiswa.Server.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Mahasiswa> Mahasiswas { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
