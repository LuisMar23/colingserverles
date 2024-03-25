using Coling.Shared;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Modelo
{
    public class Institucion : IInstitucionMongo
    {
        public ObjectId Id { get ; set ; }
        public string Nombre { get ; set ; }
        public string Tipo { get ; set ; }
        public string Direccion { get ; set ; }
        public string Estado { get ; set ; }
    }
}
