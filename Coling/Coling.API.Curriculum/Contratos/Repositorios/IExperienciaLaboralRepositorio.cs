using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorios
{
    public interface IExperienciaLaboralRepositorio
    {
        public Task<List<ExperienciaLaboral>> Listar();
        public Task<ExperienciaLaboral> Obtener(string id);
        public Task<bool> Insertar(ExperienciaLaboral experienciaLaboral);
        public Task<bool> Actualizar(ExperienciaLaboral experienciaLaboral);
        public Task<bool> Eliminar(string partitionkey, string rowkey);
    }
}
