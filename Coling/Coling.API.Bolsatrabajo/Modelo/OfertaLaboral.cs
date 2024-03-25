﻿using Coling.Shared;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Modelo
{
    public class OfertaLaboral : IOfertaLaboral
    {
        public ObjectId Id { get ; set ; }
        public ObjectId InstitucionId { get ; set ; }
        public DateTime fechaoferta { get ; set ; }
        public DateTime fechalimite { get ; set ; }
        public string descripcion { get ; set ; }
        public string titulocargo { get ; set ; }
        public string tipoContrato { get ; set ; }
        public string area { get ; set ; }
        public List<string> Caracteristicas { get ; set ; }
        public string estado { get ; set ; }
    }
}
