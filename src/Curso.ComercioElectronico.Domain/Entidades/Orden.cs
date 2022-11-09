using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Curso.ComercioElectronico.Domain;

public class Orden
{
    [Required]
    public int Id {get;set; }
 
    [Required]
    public int ClienteId {get;set;}
   
    public virtual Cliente Cliente {get;set;}

    public virtual ICollection<OrdenItem> Items {get;set;} = new List<OrdenItem>();

    [Required]
    public DateTime Fecha {get;set;}

    public DateTime? FechaAnulacion {get;set;}
    

    [Required]
    public decimal Total {get;set;}

    public string? Observaciones { get;set;}

    [Required]
    public OrdenEstado Estado {get;set;}

    public void AgregarItem(OrdenItem item){
       
        item.Orden = this;
        Items.Add(item); 
    }
}

public class OrdenItem {

    [Required]
    public int Id {get;set; }

    [Required]
    public int ProductId {get; set;}

    public virtual Producto Product { get; set; }

    [Required]
    public int OrdenId {get; set;}

    public virtual Orden Orden { get; set; }

    [Required]
    public long Cantidad {get;set;}

    public decimal Precio {get;set;}

    public string? Observaciones { get;set;}
}

public enum OrdenEstado{

    Anulada = 0,

    Registrada=1,

    Procesada=2,

    Entregada=3
}
