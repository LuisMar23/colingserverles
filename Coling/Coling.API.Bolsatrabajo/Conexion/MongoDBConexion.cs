using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Conexion
{
    public class MongoDBConexion
    {
        public MongoClient client;
        public IMongoDatabase mongoDatabase;

        public MongoDBConexion()
        {
            client = new MongoClient("mongodb://127.0.0.1:27017");
            mongoDatabase = client.GetDatabase("BolsaTrabajo");
        }
    }
}
