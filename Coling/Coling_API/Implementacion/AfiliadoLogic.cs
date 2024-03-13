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
    public class AfiliadoLogic:IAfiliadoLogic
    {
        private readonly Contexto contexto;

        public AfiliadoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarAfiliado(int id)
        {
            var afiliado = await contexto.Afiliado.FirstOrDefaultAsync(x => x.id == id);
            if (afiliado != null)
            {
                contexto.Afiliado.Remove(afiliado);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> InsertarAfiliado(Afiliado afiliado)
        {
            bool sw = false;
            contexto.Add(afiliado);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Afiliado>> ListarAfiliadosTodos()
        {
            var lista = await contexto.Afiliado.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarAfiliado(Afiliado afiliado, int id)
        {
            var a = await contexto.Afiliado.FirstOrDefaultAsync(x => x.id == id);
            if (a != null)
            {
                a.PersonaId=afiliado.PersonaId;
                a.fechaafiliacion=afiliado.fechaafiliacion;
                a.codigoafiliado=afiliado.codigoafiliado;
                a.nrotituloprovisional = afiliado.nrotituloprovisional;
                a.estado = afiliado.estado;
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Afiliado> ObtenerAfiliado(int id)
        {
            Afiliado afiliado = await contexto.Afiliado.FirstOrDefaultAsync(x => x.id == id);
            return afiliado;
        }
    }
}
