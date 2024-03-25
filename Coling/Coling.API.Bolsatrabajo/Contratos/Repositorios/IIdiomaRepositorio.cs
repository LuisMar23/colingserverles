using Coling.API.Bolsatrabajo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Contratos.Repositorios
{
    public interface IIdiomaRepositorio
    {
        public Task<List<Idioma>> Listar();
        public Task<Idioma> Obtener(string id);
        public Task<bool> Insertar(Idioma idioma);
        public Task<bool> Actualizar(Idioma idioma);
        public Task<bool> Eliminar(string id);
    }
}
