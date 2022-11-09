using System.ComponentModel.DataAnnotations;
using Curso.ComercioElectronico.Domain;
using Microsoft.Extensions.Logging;

namespace Curso.ComercioElectronico.Application;



public class MarcaAppService : IMarcaAppService
{
    private readonly IMarcaRepository repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<MarcaAppService> logger;

    public MarcaAppService(IMarcaRepository repository, IUnitOfWork unitOfWork,
        ILogger<MarcaAppService> logger)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }

    public async Task<MarcaDto> CreateAsync(MarcaCrearActualizarDto marcaDto)
    {
        logger.LogInformation("Crear Marca");

        //Reglas Validaciones... 
        var existeNombreMarca = await repository.ExisteNombre(marcaDto.Nombre);
        if (existeNombreMarca){
            
            var msg = $"Ya existe una marca con el nombre {marcaDto.Nombre}";
            logger.LogError(msg);

            throw new ArgumentException(msg);
        }
 
        //Mapeo Dto => Entidad
        var marca = new Marca();
        marca.Nombre = marcaDto.Nombre;
 
        //Persistencia objeto
        marca = await repository.AddAsync(marca);
        await unitOfWork.SaveChangesAsync();

        //Mapeo Entidad => Dto
        var marcaCreada = new MarcaDto();
        marcaCreada.Nombre = marca.Nombre;
        marcaCreada.Id = marca.Id;

        return marcaCreada;
    }

    public async Task UpdateAsync(int id, MarcaCrearActualizarDto marcaDto)
    {
        var marca = await repository.GetByIdAsync(id);
        if (marca == null){
            throw new ArgumentException($"La marca con el id: {id}, no existe");
        }
        
        var existeNombreMarca = await repository.ExisteNombre(marcaDto.Nombre,id);
        if (existeNombreMarca){
            throw new ArgumentException($"Ya existe una marca con el nombre {marcaDto.Nombre}");
        }

        //Mapeo Dto => Entidad
        marca.Nombre = marcaDto.Nombre;

        //Persistencia objeto
        await repository.UpdateAsync(marca);
        await repository.UnitOfWork.SaveChangesAsync();

        return;
    }

    public async Task<bool> DeleteAsync(int marcaId)
    {
        //Reglas Validaciones... 
        var marca = await repository.GetByIdAsync(marcaId);
        if (marca == null){
            throw new ArgumentException($"La marca con el id: {marcaId}, no existe");
        }

        repository.Delete(marca);
        await repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    public ICollection<MarcaDto> GetAll()
    {
        var marcaList = repository.GetAll();

        var marcaListDto =  from m in marcaList
                            select new MarcaDto(){
                                Id = m.Id,
                                Nombre = m.Nombre
                            };

        return marcaListDto.ToList();
    }

    
}
 