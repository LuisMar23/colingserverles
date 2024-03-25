using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Idioma
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string nombreidioma { get; set; }
        [Required]
        public string estado { get; set; }
    }
}
