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
    public class IdiomaLogic:IIdiomaLogic
    {
        private readonly Contexto contexto;

        public IdiomaLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarIdioma(int id)
        {
            var Idioma = await contexto.Idioma.FirstOrDefaultAsync(x => x.id == id);
            if (Idioma != null)
            {
                contexto.Idioma.Remove(Idioma);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> InsertarIdioma(Idioma Idioma)
        {
            bool sw = false;
            contexto.Add(Idioma);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Idioma>> ListarIdiomaesTodos()
        {
            var lista = await contexto.Idioma.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarIdioma(Idioma Idioma, int id)
        {
            var di = await contexto.Idioma.FirstOrDefaultAsync(x => x.id == id);
            if (di != null)
            {
                di.nombreidioma=Idioma.nombreidioma;
                di.estado = Idioma.estado;
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Idioma> ObtenerIdioma(int id)
        {
            Idioma d = await contexto.Idioma.FirstOrDefaultAsync(x => x.id == id);
            return d;
        }
    }
}
