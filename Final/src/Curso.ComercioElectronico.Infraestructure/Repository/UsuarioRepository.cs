
using Curso.ComercioElectronico.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.ComercioElectronico.Infraestructure;

public class UsuarioRepository : EfRepository<Usuario,Guid>, IUsuarioRepository
{
    public UsuarioRepository(ComercioElectronicoDbContext context) : base(context)
    {
    }

    public async Task<Usuario> GetByUserAsync(string user)
    {
        return await _context.Set<Usuario>().Where(x => x.User.ToUpper() == user.ToUpper())
            .SingleOrDefaultAsync();
    }
}

 