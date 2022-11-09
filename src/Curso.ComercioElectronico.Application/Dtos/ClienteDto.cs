using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class ClienteDto {

    [Required]
    public int Id {get;set; }

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombres {get;set;}
 
}
