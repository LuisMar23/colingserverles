using Coling.API.Bolsatrabajo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Contratos.Repositorios
{
    public interface IOfertaLaboralRepositorio
    {
        public Task<List<OfertaLaboral>> Listar();
        public Task<OfertaLaboral> Obtener(string id);
        public Task<bool> Insertar(OfertaLaboral OfertaLaboral);
        public Task<bool> Actualizar(OfertaLaboral OfertaLaboral);
        public Task<bool> Eliminar(string id);
    }
}
