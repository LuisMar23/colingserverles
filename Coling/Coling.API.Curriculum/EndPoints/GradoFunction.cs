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
    public class GradoFunction
    {
        private readonly ILogger<GradoFunction> _logger;
        private readonly IGradoRepositorio repos;

        public GradoFunction(ILogger<GradoFunction> logger, IGradoRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }
        [Function("InsertarGrado")]
        [OpenApiOperation("Listarspec", "InsertarGrado", Description = "Sirve para insertar un grado")]
        [OpenApiRequestBody("application/json", typeof(Grado),
           Description = "Grado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Grado),
            Description = "Insertara un grado ")]
        public async Task<HttpResponseData> InsertarGrado([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            HttpResponseData respuetsa;
            try
            {
                var registro = await req.ReadFromJsonAsync<Grado>() ?? throw new Exception("Debe ingresar una Grado con todos sus datos");
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
        [Function("ListarGrado")]
        [OpenApiOperation("Listarspec", "ListarGrado", Description = "Sirve para listar los grados")]
        [OpenApiRequestBody("application/json", typeof(Grado),
           Description = "Grado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Grado),
            Description = "Listara los grado ")]
        public async Task<HttpResponseData> ListarGrado([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
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
        [Function("obtenerGrado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "ID de Grado")]
        [OpenApiOperation("Listarspec", "obtenerGrado", Description = "Sirve para obtener un grado")]
        [OpenApiRequestBody("application/json", typeof(Grado),
           Description = "Grado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Grado),
            Description = "Obtendra un grado ")]
        public async Task<HttpResponseData> ObtenerGrado([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "obtenerGrado/{id}")] HttpRequestData req, string id)
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

        [Function("ModificarGrado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "ID de Grado")]
        [OpenApiOperation("Listarspec", "ModificarGrado", Description = "Sirve para modificar un grado")]
        [OpenApiRequestBody("application/json", typeof(Grado),
           Description = "Grado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Grado),
            Description = "Modificara un grado ")]
        public async Task<HttpResponseData> ModificarGrado(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarGrado/{id}")] HttpRequestData req,
            string id)
        {
            HttpResponseData respuesta;
            try
            {
                var Grado = await repos.Obtener(id);
                if (Grado == null)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    return respuesta;
                }

                var d = await req.ReadFromJsonAsync<Grado>() ?? throw new Exception("Debe ingresar los datos del grado a modificar");

                Grado.PartitionKey = d.PartitionKey;
                Grado.nombregrado = d.nombregrado;
                Grado.estado = d.estado;
                Grado.Timestamp = DateTime.UtcNow;
                bool resultado = await repos.Actualizar(Grado);
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

        [Function("EliminarGrado")]
        [OpenApiParameter("partitionKey", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Partition Key de Grado")]
        [OpenApiParameter("rowKey", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Row Key de Grado")]
        [OpenApiOperation("Listarspec", "EliminarGrado", Description = "Sirve para eliminar un grado")]
        [OpenApiRequestBody("application/json", typeof(Grado),
           Description = "Grado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Grado),
            Description = "Eliminara un grado ")]
        public async Task<HttpResponseData> EliminarGrado(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Grado/{partitionKey}/{rowKey}")] HttpRequestData req,
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
