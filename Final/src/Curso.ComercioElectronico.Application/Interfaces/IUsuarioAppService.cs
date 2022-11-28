using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public interface IUsuarioAppService
{
    Task<UsuarioDto> GetByIdAsync(Guid id);

    Task<UsuarioDto> GetByUserAsync(string user);
     
    Task<ListaPaginada<UsuarioDto>> GetListAsync(UsuarioListInput input);

    Task<UsuarioDto> CreateAsync(UsuarioCrearDto entidadDto);

    Task ActiveAsync (Guid entidadId, bool active);

    Task ChangePasswordAsync(Guid entidadId, string passwordNew);

    Task<bool> DeleteAsync(Guid entidadId);

    
}
