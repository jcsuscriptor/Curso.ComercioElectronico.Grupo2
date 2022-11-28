using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class UsuarioDto 
{

    [Required]
    public Guid Id { get;  set; }

    [Required]
    [StringLength(UsuarioConstantes.USUARIO_MAXIMO)]
    public string User { get; set; }


    [Required]
    public bool Activo { get; set; }

}
