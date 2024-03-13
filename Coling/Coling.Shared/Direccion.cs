using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Direccion
    {
        [Key]
        public int id { get; set; }

        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
        public string descripcion { get; set; }

        public string estado { get; set; }

    }
}
