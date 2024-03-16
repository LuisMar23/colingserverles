using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface IEstudios
    {
        public string tipoEstudio  { get; set; }
        public string titulorecibido { get; set; }

        public int anio { get; set; }

        public string estado { get; set; }
    }
}
