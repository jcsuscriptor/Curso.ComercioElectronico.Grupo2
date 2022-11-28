
using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class ClienteCategoriaRepository : EfRepository<ClienteCategoria,string>, IClienteCategoriaRepository
{
    public ClienteCategoriaRepository(ComercioElectronicoDbContext context) : base(context)
    {
    } 
}
