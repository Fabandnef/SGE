using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

public class SgeContext : DbContext
{
    public DbSet<Expediente> Expedientes { get; set; }
    public DbSet<Tramite> Tramites { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Permiso> Permisos { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("data source=SGE.sqlite");
    }

    public override int SaveChanges()
    {
        foreach (EntityEntry entry in ChangeTracker.Entries()) {
            if (entry is { State: EntityState.Added, Entity: ITimestampable newTimestampable }) {
                newTimestampable.CreatedAt = DateTime.Now;
                newTimestampable.UpdatedAt = DateTime.Now;
            } else if (entry is { State: EntityState.Modified, Entity: ITimestampable editedTimestampable }) {
                editedTimestampable.UpdatedAt = DateTime.Now;
            }
        }
        
        return base.SaveChanges();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expediente>()
                    .HasOne(expediente => expediente.UsuarioUltimaModificacion)
                    .WithOne()
                    .HasForeignKey<Expediente>(expediente => expediente.IdUsuarioUltimaModificacion);
        
        modelBuilder.Entity<Tramite>()
                    .HasOne(tramite => tramite.UsuarioUltimaModificacion)
                    .WithOne()
                    .HasForeignKey<Tramite>(tramite => tramite.IdUsuarioUltimaModificacion);
    }
}