using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.EndPoints
{
    public class ExperienciaLaboralFunction
    {
        private readonly ILogger<ExperienciaLaboralFunction> _logger;
        private readonly IExperienciaLaboralRepositorio repos;

        public ExperienciaLaboralFunction(ILogger<ExperienciaLaboralFunction> logger, IExperienciaLaboralRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }
        [Function("InsertarExperienciaLaboral")]
        public async Task<HttpResponseData> InsertarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData respuetsa;
            try
            {
                var registro = await req.ReadFromJsonAsync<ExperienciaLaboral>() ?? throw new Exception("Debe ingresar una ExperienciaLaboral con todos sus datos");
                registro.RowKey = Guid.NewGuid().ToString();
                registro.Timestamp = DateTime.UtcNow;

                bool sw = await repos.Insertar(registro);
                if (sw)
                {
                    respuetsa = req.CreateResponse(HttpStatusCode.OK);
                    return respuetsa;
                }
                else
                {
                    respuetsa = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuetsa;
                }
            }
            catch (Exception)
            {

                respuetsa = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuetsa;
            }
        }
        [Function("ListarExperienciaLaboral")]
        public async Task<HttpResponseData> ListarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData respuetsa;
            try
            {
                var lista = repos.Listar();
                respuetsa = req.CreateResponse(HttpStatusCode.OK);
                await respuetsa.WriteAsJsonAsync(lista.Result);
                return respuetsa;
            }
            catch (Exception)
            {

                respuetsa = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuetsa;
            }
        }
        [Function("obtenerExperienciaLaboral")]
        public async Task<HttpResponseData> ObtenerExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerExperienciaLaboral/{id}")] HttpRequestData req, string id)
        {
            HttpResponseData respuetsa;
            try
            {
                var lista = repos.Obtener(id);
                respuetsa = req.CreateResponse(HttpStatusCode.OK);
                await respuetsa.WriteAsJsonAsync(lista.Result);
                return respuetsa;
            }
            catch (Exception)
            {

                respuetsa = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuetsa;
            }
        }

        [Function("ModificarExperienciaLaboral")]
        public async Task<HttpResponseData> ModificarExperienciaLaboral(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarExperienciaLaboral/{id}")] HttpRequestData req,
            string id)
        {
            HttpResponseData respuesta;
            try
            {
                var ExperienciaLaboral = await repos.Obtener(id);
                if (ExperienciaLaboral == null)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    return respuesta;
                }

                var d = await req.ReadFromJsonAsync<ExperienciaLaboral>() ?? throw new Exception("Debe ingresar los datos de la institución a modificar");
        

                ExperienciaLaboral.PartitionKey = d.PartitionKey;
                ExperienciaLaboral.cargoTitulo=d.cargoTitulo;
                ExperienciaLaboral.fechainicio=d.fechainicio;
                ExperienciaLaboral.fechafinal=d.fechafinal;
                ExperienciaLaboral.estado=d.estado;
                ExperienciaLaboral.Timestamp = DateTime.UtcNow;
                bool resultado = await repos.Actualizar(ExperienciaLaboral);
                if (resultado)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }

        [Function("EliminarExperienciaLaboral")]
        public async Task<HttpResponseData> EliminarExperienciaLaboral(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "ExperienciaLaborales/{partitionKey}/{rowKey}")] HttpRequestData req,
            string partitionKey,
            string rowKey)
        {
            HttpResponseData respuesta;
            try
            {
                bool resultado = await repos.Eliminar(partitionKey, rowKey);
                if (resultado)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return respuesta;
        }
    }
}
