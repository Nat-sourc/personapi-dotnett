using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Repository;

public partial class SumaDbContext : DbContext
{
    public SumaDbContext()
    {
    }

    public SumaDbContext(DbContextOptions<SumaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estudio> Estudios { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Profesion> Profesions { get; set; }

    public virtual DbSet<Telefono> Telefonos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=34.125.22.115;Initial Catalog=arq_per_db;Persist Security Info=True;User ID=sa;Password=B4r344V3r1t4s; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudio>(entity =>
        {
            entity.HasOne(d => d.CcPerNavigation).WithMany(p => p.Estudios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudio_persona_fk");

            entity.HasOne(d => d.IdProfNavigation).WithMany(p => p.Estudios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudio_profesion_fk");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.Property(e => e.Cc).ValueGeneratedNever();
            entity.Property(e => e.Genero).IsFixedLength();
        });

        modelBuilder.Entity<Profesion>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.HasOne(d => d.DuenioNavigation).WithMany(p => p.Telefonos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_persona_telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
