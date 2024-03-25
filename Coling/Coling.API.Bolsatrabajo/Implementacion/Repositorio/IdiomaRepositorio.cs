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
    public class IdiomaRepositorio : IIdiomaRepositorio
    {
        internal MongoDBConexion conexion=new MongoDBConexion();
        private IMongoCollection<Idioma> Collection;

        public IdiomaRepositorio()
        {
            Collection = conexion.mongoDatabase.GetCollection<Idioma>("Idiomas");
        }
        public async Task<bool> Actualizar(Idioma idioma)
        {
           var filtro=Builders<Idioma>.Filter.Eq(x=>x.Id,idioma.Id);
            await Collection.ReplaceOneAsync(filtro, idioma);
            return true;
        }

        public async Task<bool> Eliminar(string id)
        {

            var elemento = Builders<Idioma>.Filter.Eq(x => x.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(elemento);
            return true;
        }

        public async Task<bool> Insertar(Idioma idioma)
        {
           await Collection.InsertOneAsync(idioma);
           return true;
        }

        public async Task<List<Idioma>> Listar()
        {
           return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();

        }

        public async Task<Idioma> Obtener(string id)
        {
          return await Collection.FindAsync(new BsonDocument { { "_Id",new ObjectId(id)} }).Result.FirstOrDefaultAsync();
        }
    }
}
