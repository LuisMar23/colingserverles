using Coling.API.Afiliados.Contratos;
using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Implementacion
{
    public class GradoLogic : IGradoLogic
    {
        private readonly Contexto contexto;

        public GradoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> CrearGrado(Grado grado)
        {
            bool sw = false;
            contexto.Add(grado);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<bool> EliminarGrado(int id)
        {
            var grado = await contexto.Grados.FirstOrDefaultAsync(x => x.id == id);
            if (grado != null)
            {
                contexto.Grados.Remove(grado);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ModificarGrado(Grado grado, int id)
        {

            var ge = await contexto.Grados.FirstOrDefaultAsync(x => x.id == id);
            if (ge != null)
            {
                ge.nombregrado=grado.nombregrado;
                ge.estado=grado.estado;
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Grado> ObtenerGradoPorId(int id)
        {
            Grado g = await contexto.Grados.FirstOrDefaultAsync(x => x.id == id);
            return g;

        }

        public async Task<List<Grado>> ListarLosGrados()
        {
            var lista = await contexto.Grados.ToListAsync();
            return lista;
        }
    }
}
