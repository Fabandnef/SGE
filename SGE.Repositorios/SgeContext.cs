﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SGE.Aplicacion.Entidades;
using SGE.Aplicacion.Interfaces.Repositorios;

namespace SGE.Repositorios;

public sealed class SgeContext : DbContext
{
    #region PROPIEDADES PUBLICAS -----------------------------------------------------------------------
    public DbSet<Expediente> Expedientes { get; set; }
    public DbSet<Permiso>    Permisos    { get; set; }
    public DbSet<Tramite>    Tramites    { get; set; }
    public DbSet<Usuario>    Usuarios    { get; set; }
    #endregion

    #region METODOS PUBLICOS ---------------------------------------------------------------------------
    public override int SaveChanges()
    {
        foreach (EntityEntry entry in ChangeTracker.Entries()) {
            if (entry is { State: EntityState.Added, Entity: ITimestampable newTimestampable, }) {
                DateTime now = DateTime.Now;
                newTimestampable.CreatedAt = now;
                newTimestampable.UpdatedAt = now;
            } else if (entry is { State: EntityState.Modified, Entity: ITimestampable editedTimestampable, }) {
                editedTimestampable.UpdatedAt = DateTime.Now;
            }
        }

        return base.SaveChanges();
    }
    #endregion

    #region METODOS ------------------------------------------------------------------------------------
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("data source=SGE.sqlite");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expediente>()
                    .HasOne(expediente => expediente.UsuarioUltimaModificacion)
                    .WithMany()
                    .HasForeignKey(expediente => expediente.IdUsuarioUltimaModificacion);

        modelBuilder.Entity<Tramite>()
                    .HasOne(tramite => tramite.UsuarioUltimaModificacion)
                    .WithMany()
                    .HasForeignKey(tramite => tramite.IdUsuarioUltimaModificacion);
    }
    #endregion
}