using Coling.API.Bolsatrabajo.Conexion;
using Coling.API.Bolsatrabajo.Contratos.Repositorios;
using Coling.API.Bolsatrabajo.Modelo;
using Coling.Shared;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Bolsatrabajo.Implementacion.Repositorio
{
    public class OfertaLaboralRepositorio:IOfertaLaboralRepositorio
    {
        internal MongoDBConexion conexion = new MongoDBConexion();
        private IMongoCollection<OfertaLaboral> Collection;

        public OfertaLaboralRepositorio()
        {
            Collection = conexion.mongoDatabase.GetCollection<OfertaLaboral>("OfertaLaboral");
        }
        public async Task<bool> Actualizar(OfertaLaboral OfertaLaboral)
        {
            var filtro = Builders<OfertaLaboral>.Filter.Eq(x => x.Id, OfertaLaboral.Id);
            await Collection.ReplaceOneAsync(filtro, OfertaLaboral);
            return true;
        }

        public async Task<bool> Eliminar(string id)
        {

            var elemento = Builders<OfertaLaboral>.Filter.Eq(x => x.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(elemento);
            return true;
        }

        public async Task<bool> Insertar(OfertaLaboral OfertaLaboral)
        {
            await Collection.InsertOneAsync(OfertaLaboral);
            return true;
        }

        public async Task<List<OfertaLaboral>> Listar()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();

        }

        public async Task<OfertaLaboral> Obtener(string id)
        {
            return await Collection.FindAsync(new BsonDocument { { "_Id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }
    }
}
