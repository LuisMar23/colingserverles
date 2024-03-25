using Coling.API.Bolsatrabajo.Conexion;
using Coling.API.Bolsatrabajo.Contratos.Repositorios;
using Coling.API.Bolsatrabajo.Modelo;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Implementacion.Repositorio
{
    public class SolicitudRepositorio:ISolicitudRepositorio
    {
        internal MongoDBConexion conexion = new MongoDBConexion();
        private IMongoCollection<Solicitud> Collection;

        public SolicitudRepositorio()
        {
            Collection = conexion.mongoDatabase.GetCollection<Solicitud>("Solicitud");
        }
        public async Task<bool> Actualizar(Solicitud Solicitud)
        {
            var filtro = Builders<Solicitud>.Filter.Eq(x => x.Id, Solicitud.Id);
            await Collection.ReplaceOneAsync(filtro, Solicitud);
            return true;
        }

        public async Task<bool> Eliminar(string id)
        {

            var elemento = Builders<Solicitud>.Filter.Eq(x => x.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(elemento);
            return true;
        }

        public async Task<bool> Insertar(Solicitud Solicitud)
        {
            await Collection.InsertOneAsync(Solicitud);
            return true;
        }

        public async Task<List<Solicitud>> Listar()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();

        }

        public async Task<Solicitud> Obtener(string id)
        {
            return await Collection.FindAsync(new BsonDocument { { "_Id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }
    }
}
