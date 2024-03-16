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
    public class EstudioRepositorio:IEstudiosRepositorio
    {

        private readonly string cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;
        public EstudioRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "Estudios";
        }

        public async Task<bool> Actualizar(Estudios Estudios)
        {
            try
            {
                var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await TablaCliente.UpdateEntityAsync(Estudios, Estudios.ETag);
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

        public async Task<bool> Insertar(Estudios Estudios)
        {
            try
            {
                var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await TablaCliente.UpsertEntityAsync(Estudios);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<Estudios>> Listar()
        {
            List<Estudios> lista = new List<Estudios>();
            var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Superior'";
            await foreach (Estudios Estudios in TablaCliente.QueryAsync<Estudios>(filter: filtro))
            {
                lista.Add(Estudios);
            }
            return lista;

        }

        public async Task<Estudios> Obtener(string id)
        {
            var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq  RowKey eq '{id}'";
            await foreach (Estudios Estudios in TablaCliente.QueryAsync<Estudios>(filter: filtro))
            {
                return Estudios;
            }
            return null;
        }
    }
}
