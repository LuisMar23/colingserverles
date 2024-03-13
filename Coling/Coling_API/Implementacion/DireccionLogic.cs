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
    public class DireccionLogic : IDireccionLogic
    {
        private readonly Contexto contexto;

        public DireccionLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarDireccion(int id)
        {
            var direccion = await contexto.Direcciones.FirstOrDefaultAsync(x => x.id == id);
            if (direccion != null)
            {
                contexto.Direcciones.Remove(direccion);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> InsertarDireccion(Direccion direccion)
        {
            bool sw = false;
            contexto.Add(direccion);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Direccion>> ListarDireccionesTodos()
        {
            var lista = await contexto.Direcciones.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarDireccion(Direccion direccion, int id)
        {
            var di = await contexto.Direcciones.FirstOrDefaultAsync(x => x.id == id);
            if (di != null)
            {
                di.PersonaId = direccion.PersonaId;
                di.descripcion=direccion.descripcion;
                di.estado=direccion.estado;
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Direccion> ObtenerDireccion(int id)
        {   
            Direccion d=await contexto.Direcciones.FirstOrDefaultAsync(x=>x.id==id);
            return d;
        }
    }
}
