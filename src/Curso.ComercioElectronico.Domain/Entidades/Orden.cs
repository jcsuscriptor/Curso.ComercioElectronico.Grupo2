using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Curso.ComercioElectronico.Domain;

public class Orden
{

    public Orden(Guid id){
        this.Id = id;
    }


    [Required]
    public Guid Id {get;set; }
 
    [Required]
    public Guid ClienteId {get;set;}
   
    public virtual Cliente Cliente {get;set;}

    public virtual ICollection<OrdenItem> Items {get;set;} = new List<OrdenItem>();

    [Required]
    public DateTime Fecha {get;set;} = DateTime.Now;

    public DateTime? FechaAnulacion {get;set;}
    

    [Required]
    public decimal Total {get;set;}

    public string? Observaciones { get;set;}

    [Required]
    public OrdenEstado Estado { get; protected set; } = OrdenEstado.Registrada;

    public void AgregarItem(OrdenItem item){
       
        item.Orden = this;
        Items.Add(item); 
    }

    public void EstablecerEstado(OrdenEstado nuevoEstado) {



        //Reglas de estados.
        if (nuevoEstado == OrdenEstado.Anulada) {
            if (Estado != OrdenEstado.Registrada) {
                throw new ArgumentException($"No se puede establecer el estado {nuevoEstado}");
            }
        }

        if (nuevoEstado == OrdenEstado.Entregada ||
            nuevoEstado == OrdenEstado.Procesada) {

            if (Estado != OrdenEstado.Registrada)
            {
                throw new ArgumentException($"No se puede establecer el estado {nuevoEstado}");
            }
        }
        if (nuevoEstado == OrdenEstado.Registrada)
        {
           throw new ArgumentException($"No se puede establecer el estado {nuevoEstado}");
        }


        this.Estado = nuevoEstado;
    }
}

public class OrdenItem {

    public OrdenItem(Guid id)
    {
        this.Id = id;
    }

    [Required]
    public Guid Id {get;set; }

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
