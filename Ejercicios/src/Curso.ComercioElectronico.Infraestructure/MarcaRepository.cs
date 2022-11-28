
using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Curso.ComercioElectronico.Infraestructure;

public class MarcaRepository : EfRepository<Marca,string>, IMarcaRepository
{
    public MarcaRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }

    public async Task<bool> ExisteNombre(string nombre, string? idExcluir = null)
    {

        var consulta = this._context.Set<TipoProducto>()
                        .Where(x => x.Nombre.ToUpper() == nombre.ToUpper());

        if (!string.IsNullOrWhiteSpace(idExcluir))
        {
            consulta = consulta.Where(x => x.Id != idExcluir);
        }

        var resultado = await consulta.AnyAsync();

        return resultado;
    }


}
