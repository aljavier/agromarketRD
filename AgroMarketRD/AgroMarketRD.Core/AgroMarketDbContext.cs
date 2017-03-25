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

        public virtual DbSet<Cuenta> cuentas { get; set; }
        public virtual DbSet<Demanda> demandas { get; set; }
        public virtual DbSet<IntencionCompra> intencion_compra { get; set; }
        public virtual DbSet<Oferta> ofertas { get; set; }
        public virtual DbSet<Producto> productoes { get; set; }
        public virtual DbSet<Sesion> sesions { get; set; }
        public virtual DbSet<TipoUnidad> tipo_unidad { get; set; }
        public virtual DbSet<TipoUsuario> tipo_usuario { get; set; }
        public virtual DbSet<Usuario> usuarios { get; set; }
        public virtual DbSet<Venta> ventas { get; set; }
        public virtual DbSet<Error> errores { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>()
                .HasMany(e => e.usuarios)
                .WithRequired(e => e.cuenta)
                .HasForeignKey(e => e.cuenta_id);

            modelBuilder.Entity<Demanda>()
                .Property(e => e.activo)
                .IsFixedLength();

            modelBuilder.Entity<Demanda>()
                .HasMany(e => e.intencion_compra)
                .WithRequired(e => e.demanda)
                .HasForeignKey(e => e.demanda_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IntencionCompra>()
                .HasMany(e => e.ventas)
                .WithRequired(e => e.intencion_compra)
                .HasForeignKey(e => e.intencion_compra_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Oferta>()
                .HasMany(e => e.intencion_compra)
                .WithRequired(e => e.oferta)
                .HasForeignKey(e => e.oferta_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.demandas)
                .WithRequired(e => e.producto)
                .HasForeignKey(e => e.producto_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .HasMany(e => e.ofertas)
                .WithRequired(e => e.producto)
                .HasForeignKey(e => e.producto_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoUnidad>()
                .HasMany(e => e.demandas)
                .WithRequired(e => e.tipo_unidad)
                .HasForeignKey(e => e.tipo_unidad_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoUnidad>()
                .HasMany(e => e.ofertas)
                .WithRequired(e => e.tipo_unidad)
                .HasForeignKey(e => e.tipo_unidad_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoUsuario>()
                .HasMany(e => e.usuarios)
                .WithRequired(e => e.tipo_usuario)
                .HasForeignKey(e => e.tipo_usuario_id);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.demandas)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.ofertas)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.sesions)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);
        }
    }
}
