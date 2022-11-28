namespace Curso.ComercioElectronico.Domain;

public interface IUsuarioRepository :  IRepository<Usuario,Guid> {



    Task<Usuario> GetByUserAsync(string user);

}

 