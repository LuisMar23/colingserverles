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
    public class ProfesionLogic:IProfesionLogic
    {
        private readonly Contexto contexto;

        public ProfesionLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarProfesion(int id)
        {
            var profesion = await contexto.Profesiones.FirstOrDefaultAsync(x => x.id == id);
            if (profesion != null)
            {
                contexto.Profesiones.Remove(profesion);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> InsertarProfesion(Profesion profesion)
        {
            bool sw = false;
            contexto.Add(profesion);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<Profesion>> ListarProfesionesTodos()
        {
            var lista = await contexto.Profesiones.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarProfesion(Profesion profesion, int id)
        {
            var p = await contexto.Profesiones.FirstOrDefaultAsync(x => x.id == id);
            if (p != null)
            {
                p.nombreprofesion=profesion.nombreprofesion; ;
                p.GradoId=profesion.GradoId;
                p.estado=profesion.estado; 
                return true;
            }
            return false;
        }

        public async Task<Profesion> ObtenerProfesion(int id)
        {
            Profesion profesion = await contexto.Profesiones.FirstOrDefaultAsync(x => x.id == id);
            return profesion;
        }
    }
}
