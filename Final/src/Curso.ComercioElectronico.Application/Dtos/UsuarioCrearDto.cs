using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class UsuarioCrearDto
{


    [Required]
    public Guid Id { get; protected set; }

    [Required]
    [StringLength(UsuarioConstantes.USUARIO_MAXIMO)]
    public string User { get; set; }

    [Required]
    public string Clave { get; set; }

    [Required]
    public bool Activo { get; set; }
}

