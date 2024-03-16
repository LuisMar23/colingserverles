using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorios
{
    public interface IInstitucionRepositorio
    {
       public Task<List<Institucion>> Listar();
       public Task<Institucion> Obtener(string id);
       public Task<bool> Insertar(Institucion institucion);
       public  Task<bool> Actualizar(Institucion institucion);
       public Task<bool> Eliminar(string partitionkey,string rowkey);


    }
}
