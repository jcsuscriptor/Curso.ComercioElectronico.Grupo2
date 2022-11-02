namespace Curso.ComercioElectronico.Application;



public interface IProductoAppService
{
    Task<ProductoDto> GetByIdAsync(int id);

    //Permitir filtrar marca,tipo producto, y por texto (nombre,codigo). Paginacion.
    ListaPaginada<ProductoDto> GetAll(int limit=10,int offset=0);

/*     ListaPaginada<ProductoDto> GetList(int limit=10,int offset=0,string? tipoProductoId="",
                        string? marcaId="",string? valorBuscar=""); */

    Task<ListaPaginada<ProductoDto>> GetListAsync(ProductoListInput input);


    Task<ProductoDto> CreateAsync(ProductoCrearActualizarDto marca);

    Task UpdateAsync (int id, ProductoCrearActualizarDto marca);

    Task<bool> DeleteAsync(int marcaId);
}

public class ProductoListInput {

    public int Limit {get;set;} = 10;
    public int Offset {get;set;} = 0;

    public int? TipoProductoId {get;set;}
    
    public int? MarcaId {get;set;}

    public string? ValorBuscar {get;set;}

}