using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Shared
{
    public interface IIdioma
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string nombreidioma { get; set; }
        public string estado { get; set; }
    }
}
