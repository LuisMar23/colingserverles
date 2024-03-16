using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface IGradoAcademico
    {
        public string nombregrado { get; set; }

        public string estado { get; set; }
    }
}
