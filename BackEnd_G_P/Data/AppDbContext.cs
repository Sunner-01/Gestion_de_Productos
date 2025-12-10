using BackEnd_G_P.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_G_P.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Proveedor> Proveedores => Set<Proveedor>();
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<OrdenProducto> OrdenesCompra => Set<OrdenProducto>();
        public DbSet<OrdenCompraDetalle> OrdenesCompraDetalles => Set<OrdenCompraDetalle>();
        public DbSet<Movimiento_Inventario> MovimientosInventario => Set<Movimiento_Inventario>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<OrdenProducto> OrdenesProductos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }
    }
}
