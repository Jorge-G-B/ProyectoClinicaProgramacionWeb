using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoClinica.Models;

public partial class DbclinicaContext : DbContext
{
    public DbclinicaContext()
    {
    }

    public DbclinicaContext(DbContextOptions<DbclinicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Caso> Casos { get; set; }

    public virtual DbSet<Consultum> Consulta { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseMySQL(configuration.GetConnectionString("ConexionDB"));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Caso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("caso");

            entity.HasIndex(e => e.UsuarioCrea, "Caso_fk0");

            entity.HasIndex(e => e.Idpaciente, "Caso_fk1");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Antecedentes).HasColumnType("text");
            entity.Property(e => e.Diagnostico).HasColumnType("text");
            entity.Property(e => e.Estado).HasMaxLength(255);
            entity.Property(e => e.FechaDeApertura).HasColumnType("datetime");
            entity.Property(e => e.FechaDeCierre).HasColumnType("datetime");
            entity.Property(e => e.Idpaciente).HasColumnName("IDPaciente");
            entity.Property(e => e.MotivoConsulta)
                .HasColumnType("text")
                .HasColumnName("Motivo_Consulta");
            entity.Property(e => e.MotivoDeCierre).HasColumnType("text");
            entity.Property(e => e.ReferidoPor).HasColumnType("text");

            entity.HasOne(d => d.IdpacienteNavigation).WithMany(p => p.Casos)
                .HasForeignKey(d => d.Idpaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Caso_fk1");

            entity.HasOne(d => d.UsuarioCreaNavigation).WithMany(p => p.Casos)
                .HasForeignKey(d => d.UsuarioCrea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Caso_fk0");
        });

        modelBuilder.Entity<Consultum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("consulta");

            entity.HasIndex(e => e.Idcaso, "Consulta_fk0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DatosObjetivos)
                .HasColumnType("text")
                .HasColumnName("Datos_Objetivos");
            entity.Property(e => e.DatosSubjetivos)
                .HasColumnType("text")
                .HasColumnName("Datos_Subjetivos");
            entity.Property(e => e.Estado).HasColumnType("text");
            entity.Property(e => e.FechaDeConsulta).HasColumnType("datetime");
            entity.Property(e => e.Idcaso).HasColumnName("IDCaso");
            entity.Property(e => e.NuevosDatos)
                .HasColumnType("text")
                .HasColumnName("Nuevos_Datos");
            entity.Property(e => e.PlanTerapuetico)
                .HasColumnType("text")
                .HasColumnName("Plan_Terapuetico");

            entity.HasOne(d => d.IdcasoNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.Idcaso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Consulta_fk0");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("paciente");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FechaDeNacimiento).HasColumnType("datetime");
            entity.Property(e => e.NombreResponsable)
                .HasMaxLength(255)
                .HasColumnName("Nombre_Responsable");
            entity.Property(e => e.Papellido)
                .HasMaxLength(255)
                .HasColumnName("PApellido");
            entity.Property(e => e.Pnombre)
                .HasMaxLength(255)
                .HasColumnName("PNombre");
            entity.Property(e => e.Sapellido)
                .HasMaxLength(255)
                .HasColumnName("SApellido");
            entity.Property(e => e.Sexo).HasMaxLength(255);
            entity.Property(e => e.Snombre)
                .HasMaxLength(255)
                .HasColumnName("SNombre");
            entity.Property(e => e.TelResponsable).HasColumnName("Tel_Responsable");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Rol, "Users_fk0");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Contraseña).HasMaxLength(255);
            entity.Property(e => e.Correo).HasMaxLength(255);
            entity.Property(e => e.User1)
                .HasMaxLength(255)
                .HasColumnName("User");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_fk0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
