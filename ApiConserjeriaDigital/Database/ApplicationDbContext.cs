using ApiConserjeriaDigital.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace ApiConserjeriaDigital.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Residente> _residente { get; set; }
        public DbSet<Conserje> _conserje { get; set; }
        public DbSet<Administrador> _admin { get; set; }
        public DbSet<Correspondencia> Casilla { get; set; }
        public DbSet<Publicacion> Pizarra { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreator != null)
                {
                    if (!dbCreator.CanConnect())
                        dbCreator.Create();
                    if (!dbCreator.HasTables())
                        dbCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
