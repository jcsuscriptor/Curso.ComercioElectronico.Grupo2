using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Domain
{
    public class UserManager
    {
        private readonly IUsuarioRepository repository;

        public UserManager(IUsuarioRepository repository)
        {
            this.repository = repository;
        }

        public virtual async Task<bool> CheckPasswordAsync(string userName, string password)
        {
          
            var userEntity = await repository.GetByUserAsync(userName);
            if (userEntity == null)
            {
                return false;
            }

            //TODO: Encriptacion
            return userEntity.Clave == password;
        }
    }
}
