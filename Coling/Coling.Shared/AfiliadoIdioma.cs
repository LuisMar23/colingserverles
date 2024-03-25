using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class AfiliadoIdioma
    {
        [Key]
        public int id { get; set; }
        public int MyProperty { get; set; }
        public int AfiliadoId { get; set; }
        public Afiliado? Afiliado { get; set; }
        public int IdiomaId { get; set; }
        public Idioma? Idioma { get; set; }
    }
}
