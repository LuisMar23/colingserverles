using Coling.Shared;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Modelo
{
    public class Idioma : IIdioma
    {
        public ObjectId Id { get; set ; }
        public string nombreidioma { get ; set ; }
        public string estado { get ; set; }
    }
}
