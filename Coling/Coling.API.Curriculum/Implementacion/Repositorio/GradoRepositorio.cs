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
    public class GradoRepositorio:IGradoRepositorio
    {

        private readonly string cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;
        public GradoRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "Grado";
        }

        public async Task<bool> Actualizar(Grado Grado)
        {
            try
            {
                var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await TablaCliente.UpdateEntityAsync(Grado, Grado.ETag);
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

        public async Task<bool> Insertar(Grado Grado)
        {
            try
            {
                var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await TablaCliente.UpsertEntityAsync(Grado);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<Grado>> Listar()
        {
            List<Grado> lista = new List<Grado>();
            var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Bachiller'";
            await foreach (Grado Grado in TablaCliente.QueryAsync<Grado>(filter: filtro))
            {
                lista.Add(Grado);
            }
            return lista;

        }

        public async Task<Grado> Obtener(string id)
        {
            var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq  RowKey eq '{id}'";
            await foreach (Grado Grado in TablaCliente.QueryAsync<Grado>(filter: filtro))
            {
                return Grado;
            }
            return null;
        }
    }
}
