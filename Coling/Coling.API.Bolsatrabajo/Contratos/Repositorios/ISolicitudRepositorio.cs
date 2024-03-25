using Coling.API.Bolsatrabajo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Contratos.Repositorios
{
    public interface ISolicitudRepositorio
    {
        public Task<List<Solicitud>> Listar();
        public Task<Solicitud> Obtener(string id);
        public Task<bool> Insertar(Solicitud Solicitud);
        public Task<bool> Actualizar(Solicitud Solicitud);
        public Task<bool> Eliminar(string id);
    }
}
