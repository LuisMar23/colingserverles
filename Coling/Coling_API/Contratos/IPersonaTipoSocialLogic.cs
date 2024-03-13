using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Contratos
{
    public interface IPersonaTipoSocialLogic
    {
        public Task<bool> InsertarPersonaTipo(PersonaTipoSocial personaTipo);
        public Task<bool> ModificarPersonaTipo(PersonaTipoSocial personaTipo, int id);

        public Task<bool> EliminarPersonaTipo(int id);

        public Task<List<PersonaTipoSocial>> ListarPersonaTiposTodos();

        public Task<PersonaTipoSocial> ObtenerPersonaTipo(int id);
    }
}
