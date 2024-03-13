using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Contratos
{
    public interface IGradoLogic
    {
        public Task<List<Grado>> ListarLosGrados();
        public Task<Grado> ObtenerGradoPorId(int id);
        public Task<bool> CrearGrado(Grado grado);
        public Task<bool> ModificarGrado(Grado grado, int id);
        public Task<bool> EliminarGrado(int id);
    }
}
