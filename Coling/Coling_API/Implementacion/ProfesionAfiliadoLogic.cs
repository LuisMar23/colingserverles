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
    public class ProfesionAfiliadoLogic:IProfesionAfiliadoLogic
    {
        private readonly Contexto contexto;

        public ProfesionAfiliadoLogic(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public async Task<bool> EliminarProfesionAfiliado(int id)
        {
            var profesionA = await contexto.profesionAfiliados.FirstOrDefaultAsync(x => x.id == id);
            if (profesionA != null)
            {
                contexto.profesionAfiliados.Remove(profesionA);
                await contexto.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> InsertarProfesionAfiliado(ProfesionAfiliado profesionafiliado)
        {
            bool sw = false;
            contexto.Add(profesionafiliado);
            int response = await contexto.SaveChangesAsync();
            if (response == 1)
            {
                sw = true;
            }
            return sw;
        }

        public async Task<List<ProfesionAfiliado>> ListarProfesionesAfiliadosTodos()
        {
            var lista = await contexto.profesionAfiliados.ToListAsync();
            return lista;
        }

        public async Task<bool> ModificarProfesionAfiliado(ProfesionAfiliado profesionafiliado, int id)
        {
            var pa = await contexto.profesionAfiliados.FirstOrDefaultAsync(x => x.id == id);
            if (pa != null)
            {
                pa.AfiliadoId=profesionafiliado.AfiliadoId;
                pa.ProfesionId=profesionafiliado.ProfesionId;
                pa.fechaasignacion=profesionafiliado.fechaasignacion;
                pa.nrosellosib=profesionafiliado.nrosellosib;
                pa.estado=profesionafiliado.estado;
                return true;
            }
            return false;
        }

        public async Task<ProfesionAfiliado> ObtenerProfesionAfiliado(int id)
        {
            ProfesionAfiliado profesionafiliado= await contexto.profesionAfiliados.FirstOrDefaultAsync(x => x.id == id);
            return profesionafiliado;
        }
    }
}
