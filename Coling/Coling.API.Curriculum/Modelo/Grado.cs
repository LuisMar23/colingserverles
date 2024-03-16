using Azure;
using Azure.Data.Tables;
using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Modelo
{
    public class Grado : IGradoAcademico,ITableEntity
    {
        public string nombregrado { get ; set; }
        public string estado { get  ;set ; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

    }
}
