using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface ISolicitud
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string nombreafiliado { get; set; }
        public  string nombrecompleto { get; set; }
        public DateTime fechapsotulacion { get; set; }
        public double pretensionsalarial { get; set; }
        public string acercade { get; set; }
        public ObjectId OfertaLaboralId { get; set; }
    }
}
