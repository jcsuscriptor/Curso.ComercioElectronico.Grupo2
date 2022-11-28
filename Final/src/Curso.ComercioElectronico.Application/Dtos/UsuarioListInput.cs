namespace Curso.ComercioElectronico.Application;

public class UsuarioListInput {

    public int Limit {get;set;} = 10;
    public int Offset {get;set;} = 0;

    public string? User { get;set;}
    
    public bool? Active {get;set;}
     
}

