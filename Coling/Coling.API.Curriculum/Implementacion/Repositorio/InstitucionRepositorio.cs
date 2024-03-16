using Azure.Data.Tables;
using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Implementacion.Repositorio
{
    public class InstitucionRepositorio : IInstitucionRepositorio
    {
        private readonly string cadenaConexion;
        private readonly string tablaNombre;
        private readonly  IConfiguration configuration;
        public InstitucionRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "Institucion";
        }

        public async Task<bool> Actualizar(Institucion institucion)
        {
            try
            {
                var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await TablaCliente.UpdateEntityAsync(institucion, institucion.ETag);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

    
        }

        public async Task<bool> Eliminar(string partitionkey, string rowkey)
        {
            try
            {
                var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await TablaCliente.DeleteEntityAsync(partitionkey, rowkey);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Insertar(Institucion institucion)
        {
            try
            {
                var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await TablaCliente.UpsertEntityAsync(institucion);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<Institucion>> Listar()
        {
            List<Institucion>lista= new List<Institucion>();
            var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion'";
            await foreach (Institucion institucion in  TablaCliente.QueryAsync<Institucion>(filter: filtro))
            {
                lista.Add(institucion);
            }
            return lista;

        }

        public async Task<Institucion> Obtener(string id)
        {
            var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and RowKey eq '{id}'";
            await foreach (Institucion institucion in TablaCliente.QueryAsync<Institucion>(filter: filtro))
            {
              return institucion;
            }
            return null;
        }
    }
}
