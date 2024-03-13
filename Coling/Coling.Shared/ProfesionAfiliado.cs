using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class ProfesionAfiliado
    {
        [Key]
        public int id { get; set; }

        public int AfiliadoId { get; set; }
        public Afiliado? Afiliado { get; set; }

        public int ProfesionId { get; set; }
        public Profesion? Profesion { get; set; }

        [Required]
        public DateTime fechaasignacion { get; set; }
        [Required]
        public string? nrosellosib { get; set; }
        public string? estado { get; set; }
    }
}
