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
            /*modelBuilder.Entity<Usuarios>().HasOne(u => u.Persona)
                                .WithMany()
                                .HasForeignKey(u => u.PersonaId);*/
        }
    }
}
