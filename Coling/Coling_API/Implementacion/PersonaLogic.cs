using Coling.API.Afiliados.Contratos;
using Coling.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Implementacion
{
    public class PersonaLogic : IPersonaLogic
    {
        private readonly Contexto contexto;

        public PersonaLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarPersona(int id)
        {
            var persona = await contexto.Personas.FirstOrDefaultAsync(x => x.Id == id);
            if (persona != null)
            {
                contexto.Personas.Remove(persona);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ModificarPersona(Persona persona, int id)
        {
            var personaExistente = await contexto.Personas.FirstOrDefaultAsync(x => x.Id == id);
            if (personaExistente != null)
            {
                // Realizar las modificaciones en la entidad existente
                personaExistente.ci = persona.ci;
                personaExistente.nombre = persona.nombre;
                personaExistente.apellidos = persona.apellidos;
                personaExistente.FechaNacimiento=persona.FechaNacimiento;
                personaExistente.Foto = persona.Foto;
                personaExistente.Estado= persona.Estado;

                // Actualizar otros campos según sea necesario

                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> InsertarPersona(Persona persona)
        {
            bool sw = false;
           contexto.Add(persona);
           int response= await contexto.SaveChangesAsync(); 
            if (response == 1) {
                sw= true;
            }
            return sw;
        }

        public async Task<List<Persona>> ListarPersonaTodos()
        {
            var lista =await  contexto.Personas.ToListAsync();
            return lista;
        }

    

        public async Task<Persona> ObtenerPersona(int id)
        {
            Persona persona = await contexto.Personas.FirstOrDefaultAsync(x => x.Id == id);
            return persona;
        }
    }
}
