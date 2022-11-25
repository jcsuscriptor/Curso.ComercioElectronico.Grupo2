using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain;

public class Cliente {

    public Cliente(Guid id)
    {
        this.Id = id;
    }

    [Required]
    public Guid Id {get;set; }

    [Required]
    [StringLength(ClienteConstantes.IDENTIFICACION_MAXIMO)]
    public string Identificacion { get; set; }

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombres {get;set;}

    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Apellidos { get; set; }

    public string? Telefonos { get; set; }

    [Required]
    public string ClienteCategoriaId { get; set; }

    public virtual ClienteCategoria ClienteCategoria { get; set; }

}
