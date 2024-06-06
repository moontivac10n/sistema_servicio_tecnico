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

    public virtual DbSet<DescripcionServicio> DescripcionServicios { get; set; }

    public virtual DbSet<RecepcionEquipo> RecepcionEquipos { get; set; }

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

        modelBuilder.Entity<DescripcionServicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("DescripcionServicio");

            entity.HasIndex(e => e.ServicioId, "fk_DescripcionServicio_Servicio1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.ServicioId).HasColumnType("int(11)");

            entity.HasOne(d => d.Servicio).WithMany(p => p.DescripcionServicios)
                .HasForeignKey(d => d.ServicioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DescripcionServicio_Servicio1");
        });

        modelBuilder.Entity<RecepcionEquipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("RecepcionEquipo");

            entity.HasIndex(e => e.ClienteId, "fk_RecepcionEquipo_Cliente1_idx");

            entity.HasIndex(e => e.ServicioId, "fk_RecepcionEquipo_Servicio1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Accesorio).HasMaxLength(400);
            entity.Property(e => e.CapacidadAlmacenamiento).HasMaxLength(50);
            entity.Property(e => e.CapacidadRam).HasColumnType("int(11)");
            entity.Property(e => e.ClienteId).HasColumnType("int(11)");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Grafico).HasMaxLength(50);
            entity.Property(e => e.MarcaPc).HasMaxLength(60);
            entity.Property(e => e.ModeloPc).HasMaxLength(60);
            entity.Property(e => e.Nserie)
                .HasMaxLength(100)
                .HasColumnName("NSerie");
            entity.Property(e => e.ServicioId).HasColumnType("int(11)");
            entity.Property(e => e.TipoAlmacenamiento).HasMaxLength(40);
            entity.Property(e => e.TipoGpu).HasMaxLength(50);
            entity.Property(e => e.TipoPc).HasMaxLength(45);

            entity.HasOne(d => d.Cliente).WithMany(p => p.RecepcionEquipos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RecepcionEquipo_Cliente1");

            entity.HasOne(d => d.Servicio).WithMany(p => p.RecepcionEquipos)
                .HasForeignKey(d => d.ServicioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RecepcionEquipo_Servicio1");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Servicio");

            entity.HasIndex(e => e.UsuarioId, "fk_Servicio_Usuario_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Estado).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("int(11)");
            entity.Property(e => e.Sku).HasMaxLength(45);
            entity.Property(e => e.UsuarioId).HasColumnType("int(11)");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.UsuarioId)
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
            entity.Property(e => e.Contraseña).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
