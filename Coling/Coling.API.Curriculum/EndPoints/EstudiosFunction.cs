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
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Coling.API.Curriculum.EndPoints
{
    public class EstudiosFunction
    {
        private readonly ILogger<EstudiosFunction> _logger;
        private readonly IEstudiosRepositorio repos;

        public EstudiosFunction(ILogger<EstudiosFunction> logger, IEstudiosRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }
        [Function("InsertarEstudios")]
        [OpenApiOperation("Listarspec", "InsertarEstudios", Description = "Sirve para insertar estudios")]
        [OpenApiRequestBody("application/json", typeof(Estudios),
           Description = "Estudios modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Estudios),
            Description = "Insertara un estudio")]
        public async Task<HttpResponseData> InsertarEstudios([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            HttpResponseData respuetsa;
            try
            {
                var registro = await req.ReadFromJsonAsync<Estudios>() ?? throw new Exception("Debe ingresar una Estudios con todos sus datos");
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
        [Function("ListarEstudios")]
        [OpenApiOperation("Listarspec", "ListarEstudios", Description = "Sirve para listar estudios")]
        [OpenApiRequestBody("application/json", typeof(Estudios),
           Description = "Estudios modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Estudios),
            Description = "Listara los estudios")]
        public async Task<HttpResponseData> ListarEstudios([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
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
        [Function("obtenerEstudios")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "ID de Estudio")]
        [OpenApiOperation("Listarspec", "obtenerEstudios", Description = "Sirve para obtener un estudio")]
        [OpenApiRequestBody("application/json", typeof(Estudios),
           Description = "Estudios modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Estudios),
            Description = "Obtendra un estudio")]
        public async Task<HttpResponseData> ObtenerEstudios([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "obtenerEstudios/{id}")] HttpRequestData req, string id)
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

        [Function("ModificarEstudios")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "ID de Estudio")]
        [OpenApiOperation("Listarspec", "ModificarEstudios", Description = "Sirve para modificar un estudio")]
        [OpenApiRequestBody("application/json", typeof(Estudios),
           Description = "Estudios modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Estudios),
            Description = "Modificara un estudio")]
        public async Task<HttpResponseData> ModificarEstudios(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarEstudios/{id}")] HttpRequestData req,
            string id)
        {
            HttpResponseData respuesta;
            try
            {
                var Estudios = await repos.Obtener(id);
                if (Estudios == null)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    return respuesta;
                }

                var d = await req.ReadFromJsonAsync<Estudios>() ?? throw new Exception("Debe ingresar los datos de la institución a modificar");

                //Estudios.PartitionKey = d.PartitionKey;
                Estudios.tipoEstudio=d.tipoEstudio;
                Estudios.titulorecibido=d.titulorecibido;
                Estudios.anio=d.anio;
                Estudios.estado=d.estado;
                Estudios.Timestamp = DateTime.UtcNow;
                bool resultado = await repos.Actualizar(Estudios);
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

        [Function("EliminarEstudios")]
        [OpenApiParameter("partitionKey", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Partition Key de Estudios")]
        [OpenApiParameter("rowKey", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Row Key de Estudios")]
        [OpenApiOperation("Listarspec", "EliminarEstudios", Description = "Sirve para eliminar un estudio")]
        [OpenApiRequestBody("application/json", typeof(Estudios),
           Description = "Estudios modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Estudios),
            Description = "Eliminara un estudio")]
        public async Task<HttpResponseData> EliminarEstudios(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Estudioses/{partitionKey}/{rowKey}")] HttpRequestData req,
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
