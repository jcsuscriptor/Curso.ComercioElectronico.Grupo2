using System.ComponentModel.DataAnnotations;

namespace Curso.ComercioElectronico.Domain;

public class Usuario {

    public Usuario(Guid id)
    {
        this.Id = id;
    }

    [Required]
    public Guid Id {get; protected set; }

    [Required]
    [StringLength(UsuarioConstantes.USUARIO_MAXIMO)]
    public string User { get; set; }

    [Required]
    public string Clave { get; set; }

    [Required]
    public bool Activo {get;set;}


}
