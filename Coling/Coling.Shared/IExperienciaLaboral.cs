using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface IExperienciaLaboral
    {
        public string cargoTitulo { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafinal { get; set; }
        public string estado { get; set; }
    }
}
