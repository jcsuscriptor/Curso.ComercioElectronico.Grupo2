using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;

public class OrdenListInput
{

    public int Limit { get; set; } = 10;

    public int Offset { get; set; } = 0;


    public Guid? ClienteId { get; set; }

    public OrdenEstado? Estado { get; set; }      

}

