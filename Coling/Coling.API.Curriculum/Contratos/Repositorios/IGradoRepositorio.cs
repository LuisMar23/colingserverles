using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorios
{
    public interface IGradoRepositorio
    {
        public Task<List<Grado>> Listar();
        public Task<Grado> Obtener(string id);
        public Task<bool> Insertar(Grado grado);
        public Task<bool> Actualizar(Grado grado);
        public Task<bool> Eliminar(string partitionkey, string rowkey);
    }
}
