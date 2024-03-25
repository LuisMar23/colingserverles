using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coling.API.Bolsatrabajo.Modelo;

namespace Coling.API.Bolsatrabajo.Contratos.Repositorios
{
    public interface IInstitucionRepositorio
    {
        public Task<List<Institucion>> Listar();
        public Task<Institucion> Obtener(string id);
        public Task<bool> Insertar(Institucion Institucion);
        public Task<bool> Actualizar(Institucion Institucion);
        public Task<bool> Eliminar(string id);
    }
}
