using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Contratos
{
    public interface IAfiliadoIdiomaLogic
    {
        public Task<bool> InsertarAfiliadoIdioma(AfiliadoIdioma AfiliadoIdioma);
        public Task<bool> ModificarAfiliadoIdioma(AfiliadoIdioma AfiliadoIdioma, int id);

        public Task<bool> EliminarAfiliadoIdioma(int id);

        public Task<List<AfiliadoIdioma>> ListarAfiliadoIdiomaesTodos();

        public Task<AfiliadoIdioma> ObtenerAfiliadoIdioma(int id);
    }
}
