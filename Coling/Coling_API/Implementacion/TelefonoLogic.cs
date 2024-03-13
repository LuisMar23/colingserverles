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
    public class TelefonoLogic:ITelefonoLogic
    {
        private readonly Contexto contexto;

        public TelefonoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarTelefono(int id)
        {
            var telefono = await contexto.Telefono.FirstOrDefaultAsync(x => x.id == id);
            if (telefono != null)
            {
                contexto.Telefono.Remove(telefono);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> InsertarTelefono(Telefono  t)
        {
            bool sw = false;
            contexto.Add(t);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Telefono>> ListarTelefonosTodos()
        {
            var lista = await contexto.Telefono.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarTelefono(Telefono t, int id)
        {
            var te = await contexto.Telefono.FirstOrDefaultAsync(x => x.id == id);
            if (te != null)
            {
                te.PersonaId=t.PersonaId;
                te.nrotelefono=t.nrotelefono;
                te.estado=t.estado;
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Telefono> ObtenerTelefono(int id)
        {
            Telefono telefono = await contexto.Telefono.FirstOrDefaultAsync(x => x.id == id);
            return telefono;
        }
    }
}
