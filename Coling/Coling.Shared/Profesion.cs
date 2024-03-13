using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Profesion
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(150)]
        public string? nombreprofesion { get; set; }

        public int GradoId { get; set; }

        public Grado? Grado { get; set; }

        public string? estado { get; set; }
    }
}
