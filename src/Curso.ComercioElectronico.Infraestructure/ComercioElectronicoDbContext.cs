using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Curso.ComercioElectronico.Infraestructure;


public class ComercioElectronicoDbContext : DbContext, IUnitOfWork
{

    //Agregar sus entidades
    public DbSet<Marca> Marcas { get; set; }

    public DbSet<TipoProducto> TipoProductos { get; set; }
    
    public DbSet<Producto> Productos { get; set; }
   
    public string DbPath { get; set; }

    public ComercioElectronicoDbContext(DbContextOptions<ComercioElectronicoDbContext> options) : base(options)
    {
    } 

}



