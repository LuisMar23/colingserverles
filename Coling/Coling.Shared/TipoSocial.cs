using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public class TipoSocial
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string nombresocial { get; set; }

        [Required]
        [StringLength(20)]
        public string estado { get; set; }
    }
}
