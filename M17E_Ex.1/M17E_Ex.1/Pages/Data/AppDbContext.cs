using M17E_Ex._1.Pages.Models;
using Microsoft.EntityFrameworkCore;

namespace M17E_Ex._1.Pages.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
