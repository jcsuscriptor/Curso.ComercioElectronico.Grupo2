using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;

namespace Curso.ComercioElectronico.Application;


public interface IClienteAppService
{

    ICollection<ClienteDto> GetAll(string buscar,int limit=10,int offset=0);

    Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto clienteDto);

    Task UpdateAsync (int id, ClienteCrearActualizarDto clienteDto);

    Task<bool> DeleteAsync(int clienteId);
}

/**
* TODO: Implementar todos los metodos del servicio de aplicacion de clientes
*/
public class ClienteAppService : IClienteAppService
{
    private readonly IClienteRepository repository;

    public ClienteAppService(IClienteRepository repository)
    {
        this.repository = repository;
    }

    public async Task<ClienteDto> CreateAsync(ClienteCrearActualizarDto clienteDto)
    {
        //TODO: Aplicar validaciones
      
 
        //Mapeo Dto => Entidad
        var cliente = new Cliente();
        cliente.Nombres = clienteDto.Nombres;
 
        //Persistencia objeto
        cliente = await repository.AddAsync(cliente);
        await repository.UnitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var clienteCreado = new ClienteDto();
        clienteCreado.Id = cliente.Id;
        clienteCreado.Nombres = cliente.Nombres; 

        return clienteCreado;
    }

    public Task<bool> DeleteAsync(int clienteId)
    {
        throw new NotImplementedException();
    }

    public ICollection<ClienteDto> GetAll(string buscar, int limit = 10, int offset = 0)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, ClienteCrearActualizarDto clienteDto)
    {
        throw new NotImplementedException();
    }
}


public class ClienteDto {

    [Required]
    public int Id {get;set; }

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombres {get;set;}
 
}

public class ClienteCrearActualizarDto {

 
    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombres {get;set;}
 
}