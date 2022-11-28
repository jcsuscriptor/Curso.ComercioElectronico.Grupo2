using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class ClienteCrearActualizarDto {


    [Required]
    [StringLength(ClienteConstantes.IDENTIFICACION_MAXIMO)]
    public string Identificacion { get; set; }

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombres { get; set; }

    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Apellidos { get; set; }

    public string? Telefonos { get; set; }

    [Required]
    public string ClienteCategoriaId { get; set; }


    public Guid? UsuarioId { get; set; }
}