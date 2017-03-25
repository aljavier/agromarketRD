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

        public virtual DbSet<cuenta> cuentas { get; set; }
        public virtual DbSet<demanda> demandas { get; set; }
        public virtual DbSet<intencion_compra> intencion_compra { get; set; }
        public virtual DbSet<oferta> ofertas { get; set; }
        public virtual DbSet<producto> productoes { get; set; }
        public virtual DbSet<sesion> sesions { get; set; }
        public virtual DbSet<tipo_unidad> tipo_unidad { get; set; }
        public virtual DbSet<tipo_usuario> tipo_usuario { get; set; }
        public virtual DbSet<usuario> usuarios { get; set; }
        public virtual DbSet<venta> ventas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cuenta>()
                .HasMany(e => e.usuarios)
                .WithRequired(e => e.cuenta)
                .HasForeignKey(e => e.cuenta_id);

            modelBuilder.Entity<demanda>()
                .Property(e => e.activo)
                .IsFixedLength();

            modelBuilder.Entity<demanda>()
                .HasMany(e => e.intencion_compra)
                .WithRequired(e => e.demanda)
                .HasForeignKey(e => e.demanda_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<intencion_compra>()
                .HasMany(e => e.ventas)
                .WithRequired(e => e.intencion_compra)
                .HasForeignKey(e => e.intencion_compra_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<oferta>()
                .HasMany(e => e.intencion_compra)
                .WithRequired(e => e.oferta)
                .HasForeignKey(e => e.oferta_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<producto>()
                .HasMany(e => e.demandas)
                .WithRequired(e => e.producto)
                .HasForeignKey(e => e.producto_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<producto>()
                .HasMany(e => e.ofertas)
                .WithRequired(e => e.producto)
                .HasForeignKey(e => e.producto_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipo_unidad>()
                .HasMany(e => e.demandas)
                .WithRequired(e => e.tipo_unidad)
                .HasForeignKey(e => e.tipo_unidad_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipo_unidad>()
                .HasMany(e => e.ofertas)
                .WithRequired(e => e.tipo_unidad)
                .HasForeignKey(e => e.tipo_unidad_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipo_usuario>()
                .HasMany(e => e.usuarios)
                .WithRequired(e => e.tipo_usuario)
                .HasForeignKey(e => e.tipo_usuario_id);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.demandas)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.ofertas)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<usuario>()
                .HasMany(e => e.sesions)
                .WithRequired(e => e.usuario)
                .HasForeignKey(e => e.usuario_id)
                .WillCascadeOnDelete(false);
        }
    }
}
