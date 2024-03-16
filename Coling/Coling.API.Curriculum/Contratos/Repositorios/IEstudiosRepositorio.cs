using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorios
{
    public interface IEstudiosRepositorio
    {
        public Task<List<Estudios>> Listar();
        public Task<Estudios> Obtener(string id);
        public Task<bool> Insertar(Estudios estudios);
        public Task<bool> Actualizar(Estudios estudios);
        public Task<bool> Eliminar(string partitionkey, string rowkey);
    }
}
