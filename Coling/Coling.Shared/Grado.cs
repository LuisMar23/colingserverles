using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class Grado
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string? nombregrado { get; set; }

        [StringLength(20)]
        public string? estado { get; set; }
    }
}
