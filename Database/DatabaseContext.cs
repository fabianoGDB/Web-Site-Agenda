using Microsoft.EntityFrameworkCore;
using Web.Site.Agenda.Models;

namespace Web.Site.Agenda.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Empresa> Empresas { get; set; }
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }
    }
}