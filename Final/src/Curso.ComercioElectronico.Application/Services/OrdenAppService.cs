using Curso.ComercioElectronico.Domain;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Curso.ComercioElectronico.Application;

public class OrdenAppService : IOrdenAppService
{
    private readonly IOrdenRepository repository;
    private readonly IProductoAppService productoAppService;
    private readonly IClienteAppService clienteAppService;
    private readonly ILogger<OrdenAppService> logger;

    public OrdenAppService(
        IOrdenRepository repository,
        IProductoAppService productoAppService,
        IClienteAppService clienteAppService,
        ILogger<OrdenAppService> logger )
    {
        this.repository = repository;
        this.productoAppService = productoAppService;
        this.clienteAppService = clienteAppService;
        this.logger = logger;
    }

    public async Task<OrdenDto> CreateAsync(OrdenCrearDto ordenDto)
    {
        logger.LogInformation("Crear Orden");

         
        var orden = new Orden(Guid.NewGuid());
        orden.ClienteId = ordenDto.ClienteId;
        orden.Fecha = ordenDto.Fecha;
        orden.Observaciones = ordenDto.Observaciones;

        var observaciones = string.Empty;
        foreach (var item in ordenDto.Items)
        {
            //TODO: Depende de negocio, reglas
            //1. Si no existe producto, no se agrega a la orden.
            //2. Si no existe producto, agregar otro producto. (Requiere mayor logica) 
            var productoDto = await productoAppService.GetByIdAsync(item.ProductId);
            if (productoDto != null)
            {
                var ordenItem = new OrdenItem(Guid.NewGuid());
                ordenItem.Cantidad = item.Cantidad;
                ordenItem.Precio = productoDto.Precio;
                ordenItem.ProductId = productoDto.Id;
                ordenItem.Observaciones = item.Observaciones;
                //ordenItem.SubTotal = (Cantidad * Precio) - Descuento ??
                orden.AgregarItem(ordenItem);
            }
            else {
                logger.LogWarning($"No existe el producto con Id: {item.ProductId}");
            }
        }
        orden.Total =  orden.Items.Sum(x => x.Cantidad*x.Precio);
        

        //Logica de negocio
        //Descuentos
        var clientDescuento = await clienteAppService.DescuentoAsync(orden.ClienteId);
        if (clientDescuento.HasValue) {
            orden.AplicarDescuento(clientDescuento.Value);
        }


        //3. Persistencias.
        orden = await repository.AddAsync(orden);
        await repository.UnitOfWork.SaveChangesAsync();
        
        return await GetByIdAsync(orden.Id);
    }

    public async Task<bool> AnularAsync(Guid ordenId)
    {
        var orden = await repository.GetByIdAsync(ordenId);
        if (orden == null)
        {
            throw new NotFoundException($"No existe la orden con el Id {ordenId}");
        }

        orden.EstablecerEstado(OrdenEstado.Anulada);
        await repository.UnitOfWork.SaveChangesAsync();

        return true;
    }

    

    public async Task<OrdenDto?> GetByIdAsync(Guid id)
    {
        var consulta = repository.GetDetails();
        consulta = consulta.Where(x => x.Id == id);

        var consulaListaDto = ConsultaDto(consulta);
         
       
        return await consulaListaDto.ToAsyncEnumerable().SingleOrDefaultAsync(); 
    }

    public Task UpdateAsync(Guid id, OrdenActualizarDto marca)
    {
        throw new NotImplementedException();
    }

    public async Task<ListaPaginada<OrdenDto>> GetListAsync(OrdenListInput input)
    {
        var consulta = repository.GetDetails();

        //Aplicar filtros
        if (input.ClienteId.HasValue)
        {
            consulta = consulta.Where(x => x.ClienteId == input.ClienteId.Value);
        }

        if (input.Estado.HasValue)
        {
            consulta = consulta.Where(x => x.Estado == input.Estado.Value);
        }

        //Ejecuatar linq. Total registros
        var total = consulta.Count();

        //Aplicar paginacion
        consulta = consulta.Skip(input.Offset)
                    .Take(input.Limit);

        //Obtener el listado paginado. (Proyeccion)
        var consulaListaDto = ConsultaDto(consulta);


        var resultado = new ListaPaginada<OrdenDto>();
        resultado.Total = total;
        resultado.Lista = await consulaListaDto.ToAsyncEnumerable().ToListAsync();

        return resultado;
    }

    private IQueryable<OrdenDto> ConsultaDto(IQueryable<Orden> consulta)
    {

        return consulta.Select(x => new OrdenDto()
                        {
                            Id = x.Id, 
                            ClienteId = x.ClienteId,
                            Cliente = $"{x.Cliente.Nombres} {x.Cliente.Apellidos}",
                            Estado = x.Estado,
                            Fecha = x.Fecha,
                            FechaAnulacion = x.FechaAnulacion,
                            Total = x.Total,
                            Observaciones = x.Observaciones,
                            Items = x.Items.Select(item => new OrdenItemDto()
                            {
                                Cantidad = item.Cantidad,
                                Id = item.Id,
                                Observaciones = item.Observaciones,
                                Precio = item.Precio,
                                ProductId = item.ProductId,
                                Product = item.Product.Nombre
                            }).ToList()
        }
                );
    }
}

