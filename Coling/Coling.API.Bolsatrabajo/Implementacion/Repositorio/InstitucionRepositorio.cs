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
    public class InstitucionRepositorio:IInstitucionRepositorio
    {
        internal MongoDBConexion conexion = new MongoDBConexion();
        private IMongoCollection<Institucion> Collection;

        public InstitucionRepositorio()
        {
            Collection = conexion.mongoDatabase.GetCollection<Institucion>("Institucion");
        }
        public async Task<bool> Actualizar(Institucion Institucion)
        {
            var filtro = Builders<Institucion>.Filter.Eq(x => x.Id, Institucion.Id);
            await Collection.ReplaceOneAsync(filtro, Institucion);
            return true;
        }

        public async Task<bool> Eliminar(string id)
        {

            var elemento = Builders<Institucion>.Filter.Eq(x => x.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(elemento);
            return true;
        }

        public async Task<bool> Insertar(Institucion Institucion)
        {
            await Collection.InsertOneAsync(Institucion);
            return true;
        }

        public async Task<List<Institucion>> Listar()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();

        }

        public async Task<Institucion> Obtener(string id)
        {
            return await Collection.FindAsync(new BsonDocument { { "_Id", new ObjectId(id) } }).Result.FirstOrDefaultAsync();
        }
    }
}
