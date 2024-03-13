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
    public class PersonaTipoSocialLogic:IPersonaTipoSocialLogic
    {
        private readonly Contexto contexto;

        public PersonaTipoSocialLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarPersonaTipo(int id)
        {
            var personatipo = await contexto.PersonaTipoSociales.FirstOrDefaultAsync(x => x.id == id);
            if (personatipo != null)
            {
                contexto.PersonaTipoSociales.Remove(personatipo);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ModificarPersonaTipo(PersonaTipoSocial personaTipo, int id)
        {
            var ptipo = await contexto.PersonaTipoSociales.FirstOrDefaultAsync(x => x.id == id);
            if (ptipo != null)
            {
                ptipo.TipoSocialId = personaTipo.TipoSocialId;
                ptipo.PersonaId= personaTipo.PersonaId;
                ptipo.estado=personaTipo.estado;
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> InsertarPersonaTipo(PersonaTipoSocial personaTipo)
        {
            bool sw = false;
            contexto.Add(personaTipo);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<PersonaTipoSocial>> ListarPersonaTiposTodos()
        {
            var lista = await contexto.PersonaTipoSociales.ToListAsync();
            return lista;
        }



        public async Task<PersonaTipoSocial> ObtenerPersonaTipo(int id)
        {
            PersonaTipoSocial personaTipo = await contexto.PersonaTipoSociales.FirstOrDefaultAsync(x => x.id == id);
            return personaTipo;
        }
    }
}
