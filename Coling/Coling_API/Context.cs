using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados
{
    public class Contexto:DbContext
    {
        public Contexto() { }
        public Contexto(DbContextOptions<Contexto> options) : base(options) 
        { 
        }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Telefono> Telefono { get; set; }
        public DbSet<Afiliado> Afiliado { get; set; }
        public DbSet<Grado> Grados { get; set; }
        public DbSet<Profesion> Profesiones { get;  set; }
        public DbSet<ProfesionAfiliado> profesionAfiliados { get; set; }
        public DbSet<TipoSocial> TipoSociales { get; set;}
        public DbSet<PersonaTipoSocial> PersonaTipoSociales { get;}
    }
}
