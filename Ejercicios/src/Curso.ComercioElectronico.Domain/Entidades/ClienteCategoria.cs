using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain;

public class ClienteCategoria {

    public ClienteCategoria(string id, string nombre)
    {
        this.Id = id;
        this.Nombre = nombre;
    }

    [Required]
    [StringLength(DominioConstantes.ID_MAXIMO)]
    [RegularExpression(DominioConstantes.ExpressionRegular.ALFANUMERICOS)]
    public string Id { get; set; }

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre { get; set; }

    [Range(0,100)]
    public long? Descuento { get; set; }
}