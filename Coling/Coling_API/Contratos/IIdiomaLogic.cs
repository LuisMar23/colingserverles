using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Contratos
{
    public interface  IIdiomaLogic
    {
        public Task<bool> InsertarIdioma(Idioma Idioma);
        public Task<bool> ModificarIdioma(Idioma Idioma, int id);

        public Task<bool> EliminarIdioma(int id);

        public Task<List<Idioma>> ListarIdiomaesTodos();

        public Task<Idioma> ObtenerIdioma(int id);
    }
}
