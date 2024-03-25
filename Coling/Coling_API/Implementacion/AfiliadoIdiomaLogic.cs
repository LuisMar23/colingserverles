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
    public class AfiliadoIdiomaLogic:IAfiliadoIdiomaLogic
    {
        private readonly Contexto contexto;

        public AfiliadoIdiomaLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarAfiliadoIdioma(int id)
        {
            var AfiliadoIdioma = await contexto.AfiliadoIdioma.FirstOrDefaultAsync(x => x.id == id);
            if (AfiliadoIdioma != null)
            {
                contexto.AfiliadoIdioma.Remove(AfiliadoIdioma);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> InsertarAfiliadoIdioma(AfiliadoIdioma AfiliadoIdioma)
        {
            bool sw = false;
            contexto.Add(AfiliadoIdioma);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<AfiliadoIdioma>> ListarAfiliadoIdiomaesTodos()
        {
            var lista = await contexto.AfiliadoIdioma.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarAfiliadoIdioma(AfiliadoIdioma AfiliadoIdioma, int id)
        {
            var di = await contexto.AfiliadoIdioma.FirstOrDefaultAsync(x => x.id == id);
            if (di != null)
            {
                di.IdiomaId=AfiliadoIdioma.IdiomaId;
                di.AfiliadoId=AfiliadoIdioma.AfiliadoId;
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<AfiliadoIdioma> ObtenerAfiliadoIdioma(int id)
        {
            AfiliadoIdioma d = await contexto.AfiliadoIdioma.FirstOrDefaultAsync(x => x.id == id);
            return d;
        }
    }
}
