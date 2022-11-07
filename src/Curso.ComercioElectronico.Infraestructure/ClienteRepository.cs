
using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class ClienteRepository : EfRepository<Cliente>, IClienteRepository
{
    public ClienteRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }
 

    
}

 