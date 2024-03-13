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
    public class TipoSocialLogic:ITipoSocialLogic
    {
        private readonly Contexto contexto;

        public TipoSocialLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarTipoSocial(int id)
        {
            var tipo = await contexto.TipoSociales.FirstOrDefaultAsync(x => x.Id == id);
            if (tipo != null)
            {
                contexto.TipoSociales.Remove(tipo);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ModificarTipoSocial(TipoSocial tipo, int id)
        {
            var tiex = await contexto.TipoSociales.FirstOrDefaultAsync(x => x.Id == id);
            if (tiex != null)
            {
     

                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> InsertarTipoSocial(TipoSocial tipo)
        {
            bool sw = false;
            contexto.Add(tipo);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<TipoSocial>> ListarTipoSocialesTodos()
        {
            var lista = await contexto.TipoSociales.ToListAsync();
            return lista;
        }



        public async Task<TipoSocial> ObtenerTipoSocial(int id)
        {
            TipoSocial tipo = await contexto.TipoSociales.FirstOrDefaultAsync(x => x.Id == id);
            return tipo;
        }
    }
}
