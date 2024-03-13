using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Telefono
    {
        [Key]
        public int id { get; set; }

        public int PersonaId { get; set; }
        public Persona? Persona { get; set; }


        [Required]
        [StringLength(60)]
        public string nrotelefono { get; set; }

        [StringLength(20)]
        public string? estado { get; set; }
    }
}
