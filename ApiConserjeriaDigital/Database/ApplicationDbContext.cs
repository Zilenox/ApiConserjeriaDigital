using ApiConserjeriaDigital.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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
        public DbSet<trash> temp { get; set; }
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
                    {
                        dbCreator.CreateTables();
                        Residente demoRes =
                                new Residente
                                {
                                    ID = 0,
                                    RUT = 172223330,
                                    Nombre = "Hugo",
                                    NumeroDepto = 103,
                                    Casilla = 2
                                };
                        Residente demoRes2 =
                                new Residente
                                {
                                    ID = 0,
                                    RUT = 173334440,
                                    Nombre = "Diego",
                                    NumeroDepto = 205,
                                    Casilla = 0
                                };
                        this._residente.Add(demoRes);
                        this._residente.Add(demoRes2);
                        Conserje demoConserje =
                                new Conserje
                                {
                                    ID = 0,
                                    RUT = 180001110,
                                    Nombre = "Carlos"
                                };
                        this._conserje.Add(demoConserje);
                        Administrador demoAdmin =

                                new Administrador
                                {
                                    ID = 0,
                                    RUT = 196665550,
                                    Nombre = "Jose"
                                };

                        this._admin.Add(demoAdmin);
                        //this.SaveChangesAsync();
                        
                         trash demoPass =
                                  new trash
                                  {
                                      pass = "password",
                                      RUT = 172223330
                                  };
                          trash demoPass2 =
                                  new trash
                                  {
                                      pass = "password",
                                      RUT = 173334440
                                  };
                          trash demoPass4 =
                                  new trash
                                  {
                                      pass = "password",
                                      RUT = 180001110
                                  };
                          trash demoPass5 =
                                  new trash
                                  {
                                      pass = "password",
                                      RUT = 196665550
                                  };
                          this.temp.Add(demoPass);
                          this.temp.Add(demoPass2);
                          this.temp.Add(demoPass4);
                          this.temp.Add(demoPass5);
                        Publicacion demopub =
                              new Publicacion
                              {
                                  Id = 0,
                                  Note = "Corte general de agua, mañana a las 9AM"
                              };
                        this.Pizarra.Add(demopub);

                        Correspondencia cartaDemo =
                            new Correspondencia
                            {
                                Id = 0,
                                Destinatario = 103,
                                tipo = "Carta"
                            };
                        Correspondencia cartaDemo2 =
                            new Correspondencia
                            {
                                Id = 0,
                                Destinatario = 103,
                                tipo = "Encomienda"
                            };
                        this.Casilla.Add(cartaDemo);
                        this.Casilla.Add(cartaDemo2);
                        this.SaveChangesAsync();

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
