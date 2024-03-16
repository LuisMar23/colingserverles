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
    public class ProfesionRepositorio:IProfesionRepositorio
    {

        private readonly string cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;
        public ProfesionRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "Profesion";
        }

        public async Task<bool> Actualizar(Profesion profesion)
        {
            try
            {
                var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await TablaCliente.UpdateEntityAsync(profesion, profesion.ETag);
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

        public async Task<bool> Insertar(Profesion Profesion)
        {
            try
            {
                var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await TablaCliente.UpsertEntityAsync(Profesion);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<Profesion>> Listar()
        {
            List<Profesion> lista = new List<Profesion>();
            var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'PR01'";
            await foreach (Profesion Profesion in TablaCliente.QueryAsync<Profesion>(filter: filtro))
            {
                lista.Add(Profesion);
            }
            return lista;

        }

        public async Task<Profesion> Obtener(string id)
        {
            var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq RowKey eq '{id}'";
            await foreach (Profesion Profesion in TablaCliente.QueryAsync<Profesion>(filter: filtro))
            {
                return Profesion;
            }
            return null;
        }
    }
}
