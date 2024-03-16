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
    public class ExperienciaLaboralRepositorio:IExperienciaLaboralRepositorio
    {

        private readonly string cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;
        public ExperienciaLaboralRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "ExperienciaLaboral";
        }

        public async Task<bool> Actualizar(ExperienciaLaboral ExperienciaLaboral)
        {
            try
            {
                var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await TablaCliente.UpdateEntityAsync(ExperienciaLaboral, ExperienciaLaboral.ETag);
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

        public async Task<bool> Insertar(ExperienciaLaboral ExperienciaLaboral)
        {
            try
            {
                var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await TablaCliente.UpsertEntityAsync(ExperienciaLaboral);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<ExperienciaLaboral>> Listar()
        {
            List<ExperienciaLaboral> lista = new List<ExperienciaLaboral>();
            var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'General'";
            await foreach (ExperienciaLaboral ExperienciaLaboral in TablaCliente.QueryAsync<ExperienciaLaboral>(filter: filtro))
            {
                lista.Add(ExperienciaLaboral);
            }
            return lista;

        }

        public async Task<ExperienciaLaboral> Obtener(string id)
        {
            var TablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey  RowKey eq '{id}'";
            await foreach (ExperienciaLaboral ExperienciaLaboral in TablaCliente.QueryAsync<ExperienciaLaboral>(filter: filtro))
            {
                return ExperienciaLaboral;
            }
            return null;
        }
    }
}
