using Backend_Gympro.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Backend_Gympro.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
        public DbSet<Clase> Clase { get; set; }
        public DbSet<Inscripcion> Inscripcion { get; set; }
        public DbSet<Membresia> Membresia { get; set; }
        public DbSet<MetodoPago> MetodoPago { get; set; }
        public DbSet<Pago> Pago { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<SuplementoDeportivo> SuplementoDeportivo { get; set; }
        public DbSet<TipoMembresia> TipoMembresia { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<ProgresoFisico> ProgresoFisico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Departamento>().ToTable("departamento");
            modelBuilder.Entity<Municipio>().ToTable("municipio");
            modelBuilder.Entity<Clase>().ToTable("clase");
            modelBuilder.Entity<Inscripcion>().ToTable("inscripcion");
            modelBuilder.Entity<Membresia>().ToTable("membresia");
            modelBuilder.Entity<MetodoPago>().ToTable("metodo_pago");
            modelBuilder.Entity<Pago>().ToTable("pago");
            modelBuilder.Entity<Persona>().ToTable("persona");
            modelBuilder.Entity<Rol>().ToTable("rol");
            modelBuilder.Entity<SuplementoDeportivo>().ToTable("suplemento_deportivo");
            modelBuilder.Entity<TipoMembresia>().ToTable("tipo_membresia");
            modelBuilder.Entity<Usuarios>().ToTable("usuarios");
            modelBuilder.Entity<ProgresoFisico>().ToTable("progresofisico");
            modelBuilder.Entity<Membresia>()
                                .HasOne(m => m.TipoMembresia)
                                .WithMany()  // Si TipoMembresia tiene una relación de "uno a muchos", usa "WithMany"
                                .HasForeignKey(m => m.id_tipo_membresia)
                                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Clase>()
                        .HasOne(c => c.Usuario)
                        .WithMany()
                        .HasForeignKey(c => c.id_usuario) // Especificamos la propiedad que se usará como clave foránea
                        .HasConstraintName("FK_Clase_Usuario");  // Nombre de la restricción (opcional)
            modelBuilder.Entity<Inscripcion>()
                        .HasOne(c => c.Clase)
                        .WithMany()
                        .HasForeignKey(c => c.id_clase) // Especificamos la propiedad que se usará como clave foránea
                        .HasConstraintName("FK_Inscripcion_Clase");  // Nombre de la restricción (opcional)
        }
    }
}
