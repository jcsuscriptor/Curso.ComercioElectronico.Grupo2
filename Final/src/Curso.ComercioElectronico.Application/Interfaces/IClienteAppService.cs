using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public interface IClienteAppService
{
    Task<ClienteDto> GetByIdAsync(Guid id);

    Task<ListaPaginada<ClienteDto>> GetListAsync(string? buscar, int limit = 10, int offset = 0
        , string? categoriaId = null);

    Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto clienteDto);

    Task UpdateAsync (Guid clienteId, ClienteCrearActualizarDto clienteDto);

    Task<bool> DeleteAsync(Guid clienteId);

    Task<long?> DescuentoAsync(Guid clienteId);

    Task<ClienteDto> GetByUserIdAsync(Guid usuarioId);
}
