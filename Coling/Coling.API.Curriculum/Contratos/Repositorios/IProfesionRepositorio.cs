using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorios
{
    public interface IProfesionRepositorio
    {
        public Task<List<Profesion>> Listar();
        public Task<Profesion> Obtener(string id);
        public Task<bool> Insertar(Profesion profesion);
        public Task<bool> Actualizar(Profesion profesion);
        public Task<bool> Eliminar(string partitionkey, string rowkey);
    }
}
