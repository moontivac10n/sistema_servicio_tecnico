using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace sistema_servicio_tecnico.Models;

public partial class MercyDeveloperContext : DbContext
{
    public MercyDeveloperContext()
    {
    }

    public MercyDeveloperContext(DbContextOptions<MercyDeveloperContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Apellido).HasMaxLength(45);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(45);
            entity.Property(e => e.Estado).HasMaxLength(45);
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.Telefono).HasMaxLength(13);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Servicio");

            entity.HasIndex(e => e.UsuariosId, "fk_Servicio_Usuario_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("int(11)");
            entity.Property(e => e.Sku).HasMaxLength(45);
            entity.Property(e => e.UsuariosId).HasColumnType("int(11)");

            entity.HasOne(d => d.Usuarios).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.UsuariosId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Servicio_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Apellido).HasMaxLength(45);
            entity.Property(e => e.Correo).HasMaxLength(60);
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
