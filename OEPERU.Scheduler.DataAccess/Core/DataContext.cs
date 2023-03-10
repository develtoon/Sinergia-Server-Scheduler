using Microsoft.EntityFrameworkCore;
using System.IO;
using OEPERU.Scheduler.Common.Entities;
using System;
using OEPERU.Scheduler.Common.Configuration;

namespace OEPERU.Scheduler.DataAccess.Core
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuracion = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(configuracion.GetSection("ConnectionStrings")["DefaultConnection"]);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public virtual void Save()
        {
            base.SaveChanges();
        }

        #region Entities representing Database Objects

        #region Administracion

        public DbSet<CatalogoEstado> CatalogoEstados { get; set; }
        public DbSet<Catalogo> Catalogos { get; set; }
        public DbSet<CatalogoDetalle> CatalogoDetalles { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<PersonaEmpresa> PersonasEmpresas { get; set; }
        public DbSet<Persona> Personas { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoEquipo> PedidosEquipo { get; set; }
        public DbSet<PedidoBitacora> PedidosBitacora { get; set; }
        public DbSet<PedidoInspeccion> PedidosInspeccion { get; set; }
        public DbSet<PedidoInspeccionDetalle> PedidosInspeccionDetalle { get; set; }
        public DbSet<PedidoControl> PedidosControl { get; set; }

        #endregion

        #region Seguridad

        public DbSet<Usuario> Usuarios { get; set; }
        #endregion

        #endregion


    }
}

