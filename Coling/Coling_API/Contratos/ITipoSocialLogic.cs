using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Contratos
{
    public interface ITipoSocialLogic
    {
        public Task<bool> InsertarTipoSocial(TipoSocial tipo);
        public Task<bool> ModificarTipoSocial(TipoSocial tipo, int id);

        public Task<bool> EliminarTipoSocial(int id);

        public Task<List<TipoSocial>> ListarTipoSocialesTodos();

        public Task<TipoSocial> ObtenerTipoSocial(int id);
    }
}
