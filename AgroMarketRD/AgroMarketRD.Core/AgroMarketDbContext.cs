namespace AgroMarketRD.Core
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Entities;

    public partial class AgroMarketDbContext : DbContext
    {
        public AgroMarketDbContext()
            : base("name=AgroMarket")
        {
        }

        public virtual DbSet<Cuenta> Cuentas { get; set; }
        public virtual DbSet<Oferta> Ofertas { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Sesion> Sesiones { get; set; }
        public virtual DbSet<TipoUnidad> TipoUnidad { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Error> Errores { get; set; }
        public virtual DbSet<Firma> Firmas { get; set; }
        public virtual DbSet<IntencionCompra> IntencionCompra { get; set; }
        public virtual DbSet<IntencionVenta> IntencionVenta { get; set; }
        public virtual DbSet<ProductoIntencionCompra> ProductoIntencionCompra { get; set; }
        public virtual DbSet<ProductoIntencionVenta> ProductoIntencionVenta { get; set; }
        public virtual DbSet<AccesoLog> AccesoLog { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.Cuenta)
                .HasForeignKey(e => e.CuentaId);

            modelBuilder.Entity<TipoUnidad>()
                .HasMany(e => e.Ofertas)
                .WithRequired(e => e.TipoUnidad)
                .HasForeignKey(e => e.TipoUnidadId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoUsuario>()
                .HasMany(e => e.Usuarios)
                .WithRequired(e => e.TipoUsuario)
                .HasForeignKey(e => e.TipoUsuarioId);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.Ofertas)
                .WithRequired(e => e.Producto)
                .HasForeignKey(e => e.ProductoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Ofertas)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.UsuarioId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IntencionCompra>()
               .HasMany(e => e.IntencionesCompra)
               .WithRequired(e => e.IntencionCompra)
               .HasForeignKey(e => e.IntencionCompraId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<IntencionVenta>()
                .HasMany(e => e.ProductoIntencionVenta)
                .WithRequired(e => e.IntencionVenta)
                .HasForeignKey(e => e.IntencionVentaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ErrorLog>()
                .Property(e => e.StackTrace)
                .IsUnicode(false);
        }
    }
}
