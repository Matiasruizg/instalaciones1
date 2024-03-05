using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoIntegrador.Models;

public partial class SportQuitoContext : DbContext
{
    public SportQuitoContext()
    {
    }

    public SportQuitoContext(DbContextOptions<SportQuitoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InstalacionesDeportiva> InstalacionesDeportivas { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Perfil> Perfils { get; set; }

    public virtual DbSet<Recurso> Recursos { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<TipoPago> TipoPagos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioPerfil> UsuarioPerfils { get; set; }

    public virtual DbSet<VistaRecursosActivo> VistaRecursosActivos { get; set; }

    public virtual DbSet<VistaReserva> VistaReservas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //        => optionsBuilder.UseSqlServer("server=localhost; database=SportQuito; integrated security=true;TrustServerCertificate=true");
        }

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<InstalacionesDeportiva>(entity =>
        {
            entity.HasKey(e => e.IdInstalacionDeportiva).HasName("PK__Instalac__2F9642479D2DB506");

            entity.ToTable("Instalaciones_Deportivas");

            entity.Property(e => e.IdInstalacionDeportiva).HasColumnName("Id_Instalacion_Deportiva");
            entity.Property(e => e.DisponibilidadInstalacion).HasColumnName("Disponibilidad_Instalacion");
            entity.Property(e => e.NombreInstalacion)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("Nombre_Instalacion");
            entity.Property(e => e.TipoCanchaInstalacion)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("TipoCancha_Instalacion");
            entity.Property(e => e.UbicacionInstalacion)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("Ubicacion_Instalacion");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pago__3E79AD9AC9B9C194");

            entity.ToTable("Pago");

            entity.Property(e => e.IdPago).HasColumnName("Id_Pago");
            entity.Property(e => e.FechaPago)
                .HasColumnType("date")
                .HasColumnName("Fecha_Pago");
            entity.Property(e => e.HoraPago).HasColumnName("Hora_Pago");
            entity.Property(e => e.IdReserva).HasColumnName("Id_Reserva");
            entity.Property(e => e.IdTipoPago).HasColumnName("Id_TipoPago");
            entity.Property(e => e.MontoPago)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Monto_Pago");
            entity.Property(e => e.TotalPago)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Total_Pago");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK_Id_Reserva_Pago");

            entity.HasOne(d => d.IdTipoPagoNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdTipoPago)
                .HasConstraintName("FK_Id_TipoPago");
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.HasKey(e => e.IdPerfil).HasName("PK__Perfil__2CDD94194DFBF723");

            entity.ToTable("Perfil");

            entity.Property(e => e.IdPerfil).HasColumnName("Id_Perfil");
            entity.Property(e => e.TipoPerfil)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("Tipo_Perfil");
        });

        modelBuilder.Entity<Recurso>(entity =>
        {
            entity.HasKey(e => e.IdRecurso).HasName("PK__Recurso__048D0C4464EB86D2");

            entity.ToTable("Recurso");

            entity.Property(e => e.IdRecurso).HasColumnName("Id_Recurso");
            entity.Property(e => e.CantidadRecurso).HasColumnName("Cantidad_Recurso");
            entity.Property(e => e.CostoRecurso)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Costo_Recurso");
            entity.Property(e => e.DescripcionRecurso)
                .HasMaxLength(200)
                .HasColumnName("Descripcion_Recurso");
            entity.Property(e => e.DisponibilidadRecurso).HasColumnName("Disponibilidad_Recurso");
            entity.Property(e => e.NombreRecurso)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("Nombre_Recurso");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reserva__9E953BE1928DD6EA");

            entity.ToTable("Reserva");

            entity.Property(e => e.IdReserva).HasColumnName("Id_Reserva");
            entity.Property(e => e.CostoReserva)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Costo_Reserva");
            entity.Property(e => e.FechaReserva)
                .HasColumnType("date")
                .HasColumnName("Fecha_Reserva");
            entity.Property(e => e.HoraReserva).HasColumnName("Hora_Reserva");
            entity.Property(e => e.IdInstalacionDeportiva).HasColumnName("Id_Instalacion_Deportiva");
            entity.Property(e => e.IdUperfil).HasColumnName("Id_UPerfil");

            entity.HasOne(d => d.IdInstalacionDeportivaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdInstalacionDeportiva)
                .HasConstraintName("FK_Id_Instalacion_Deportiva");

            entity.HasOne(d => d.IdUperfilNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdUperfil)
                .HasConstraintName("FK_Id_UPerfil");

            entity.HasMany(d => d.IdRecursos).WithMany(p => p.IdReservas)
                .UsingEntity<Dictionary<string, object>>(
                    "ReservaRecurso",
                    r => r.HasOne<Recurso>().WithMany()
                        .HasForeignKey("IdRecurso")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Id_Recurso"),
                    l => l.HasOne<Reserva>().WithMany()
                        .HasForeignKey("IdReserva")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Id_Reserva_RR"),
                    j =>
                    {
                        j.HasKey("IdReserva", "IdRecurso").HasName("PK__Reserva___CEDDEB25FD486599");
                        j.ToTable("Reserva_Recurso");
                        j.IndexerProperty<int>("IdReserva").HasColumnName("Id_Reserva");
                        j.IndexerProperty<int>("IdRecurso").HasColumnName("Id_Recurso");
                    });
        });

        modelBuilder.Entity<TipoPago>(entity =>
        {
            entity.HasKey(e => e.IdTipoPago).HasName("PK__TipoPago__55DB838A61862B02");

            entity.ToTable("TipoPago");

            entity.Property(e => e.IdTipoPago).HasColumnName("Id_TipoPago");
            entity.Property(e => e.TipoPago1)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("Tipo_Pago");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__63C76BE2A6299916");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");
            entity.Property(e => e.ApellidoUsuario)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("Apellido_Usuario");
            entity.Property(e => e.ContrasenaUsuario)
                .HasMaxLength(255)
                .HasColumnName("Contrasena_Usuario");
            entity.Property(e => e.CorreoUsuario)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("Correo_Usuario");
            entity.Property(e => e.DireccionUsuario)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("Direccion_Usuario");
            entity.Property(e => e.EdadUsuario).HasColumnName("Edad_Usuario");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("Nombre_Usuario");
            entity.Property(e => e.TelefonoUsuario)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("Telefono_Usuario");
        });

        modelBuilder.Entity<UsuarioPerfil>(entity =>
        {
            entity.HasKey(e => e.IdUperfil).HasName("PK__Usuario___239ACDE8F5EAEFF8");

            entity.ToTable("Usuario_Perfil");

            entity.Property(e => e.IdUperfil).HasColumnName("Id_UPerfil");
            entity.Property(e => e.IdPerfil).HasColumnName("Id_Perfil");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.UsuarioPerfils)
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("FK_Id_Perfil");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioPerfils)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_Id_Usuario");
        });

        modelBuilder.Entity<VistaRecursosActivo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vista_Recursos_Activos");

            entity.Property(e => e.CantidadRecurso).HasColumnName("Cantidad_Recurso");
            entity.Property(e => e.CostoRecurso)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Costo_Recurso");
            entity.Property(e => e.DescripcionRecurso)
                .HasMaxLength(200)
                .HasColumnName("Descripcion_Recurso");
            entity.Property(e => e.DisponibilidadRecurso).HasColumnName("Disponibilidad_Recurso");
            entity.Property(e => e.NombreRecurso)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("Nombre_Recurso");
        });

        modelBuilder.Entity<VistaReserva>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vista_Reserva");

            entity.Property(e => e.CorreoUsuario)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("Correo_Usuario");
            entity.Property(e => e.DireccionUsuario)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("Direccion_Usuario");
            entity.Property(e => e.EdadUsuario).HasColumnName("Edad_Usuario");
            entity.Property(e => e.FechaReserva)
                .HasColumnType("date")
                .HasColumnName("Fecha_Reserva");
            entity.Property(e => e.HoraReserva).HasColumnName("Hora_Reserva");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(65)
                .IsUnicode(false)
                .HasColumnName("Nombre_Cliente");
            entity.Property(e => e.NombreInstalacion)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("Nombre_Instalacion");
            entity.Property(e => e.RecursosReserva)
                .HasMaxLength(4000)
                .HasColumnName("Recursos_Reserva");
            entity.Property(e => e.TelefonoUsuario)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("Telefono_Usuario");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("Tipo_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
