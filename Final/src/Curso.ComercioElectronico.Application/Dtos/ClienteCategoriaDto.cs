using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class ClienteCategoriaDto
{
    public string Id { get; set; }

    public string Nombre { get; set; }

    public long? Descuento { get; set; }
}

public class ClienteCategoriaCrearDto
{
    [Required]
    [StringLength(DominioConstantes.ID_MAXIMO)]
    [RegularExpression(DominioConstantes.ExpressionRegular.ALFANUMERICOS)]
    public string Id { get; set; }

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre { get; set; }

    [Range(0, 100)]
    public long? Descuento { get; set; }
}

public class ClienteCategoriaActualizarDto
{ 

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre { get; set; }

    [Range(0, 100)]
    public long? Descuento { get; set; }
}


