using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Afiliado
    {
        [Key]
        public int id { get; set; }

        public int PersonaId { get; set; }
        public Persona? Persona { get; set; }

        [Required]
        public DateTime? fechaafiliacion { get; set; }
        [Required]
        [StringLength(50)]
        public string? codigoafiliado { get; set; }
        [Required]
        [StringLength(50)]
        public string? nrotituloprovisional { get; set; }

        public string? estado { get; set; }
    }
}
