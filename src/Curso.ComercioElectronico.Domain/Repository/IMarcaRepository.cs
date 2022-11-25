namespace Curso.ComercioElectronico.Domain;

public interface IMarcaRepository :  IRepository<Marca,string> {


    Task<bool> ExisteNombre(string nombre, string? idExcluir=null);

   
}
