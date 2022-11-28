using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain;

public class TipoProducto
{
    public TipoProducto(string id,string nombre)
    {
        this.Id = id;
        this.Nombre = nombre;
    }


    [Required]
    [StringLength(DominioConstantes.ID_MAXIMO)]
    [RegularExpression(DominioConstantes.ExpressionRegular.ALFANUMERICOS)]
    public string Id {get; protected set; }

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre {get;set;}

}




