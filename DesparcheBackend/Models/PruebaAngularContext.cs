using System;
using DesparcheBackend.Autorizacion;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DesparcheBackend.Models
{
    public partial class PruebaAngularContext : IdentityDbContext<Usuario>
    {
        //public PruebaAngularContext()
        //{
        //}

        public PruebaAngularContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Proyecto> Proyecto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:fulsstakobra.database.windows.net,1433;Initial Catalog=avanceobra;Persist Security Info=False;User ID=admin_obra;Password=Avance_123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Apellidos).HasMaxLength(50);

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
