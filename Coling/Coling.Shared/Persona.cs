using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }
        public string? ci { get; set; }
        public string? nombre { get; set; }

        public string? apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public string? Foto { get; set; }

        public string? Estado { get;  set; }
    }
}
