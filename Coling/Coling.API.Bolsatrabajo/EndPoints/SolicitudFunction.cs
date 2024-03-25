using Coling.API.Bolsatrabajo.Contratos.Repositorios;
using Coling.API.Bolsatrabajo.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;

namespace Coling.API.Bolsatrabajo.EndPoints
{
    public class SolicitudFunction
    {
        private readonly ILogger<SolicitudFunction> _logger;
        private readonly ISolicitudRepositorio repos;

        public SolicitudFunction(ILogger<SolicitudFunction> logger, ISolicitudRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }
        [Function("InsertarSolicitud")]
        [OpenApiOperation("Listarspec", "InsertarSolicitud", Description = "Sirve para insertar un Solicitud")]
        [OpenApiRequestBody("application/json", typeof(Solicitud),
   Description = "Solicitud modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Solicitud),
            Description = "Insertara un Solicitud ")]
        public async Task<HttpResponseData> InsertarGrado([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            HttpResponseData respuetsa;
            try
            {
                var registro = await req.ReadFromJsonAsync<Solicitud>() ?? throw new Exception("Debe ingresar un Solicitud con todos sus datos");

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


        [Function("ListarSolicituds")]
        [OpenApiOperation("Listarspec", "ListarSolicituds", Description = "Sirve para listar los Solicitud")]
        [OpenApiRequestBody("application/json", typeof(Solicitud),
   Description = "Solicitud modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
    bodyType: typeof(Solicitud),
    Description = "Listará los Solicituds")]
        public async Task<HttpResponseData> ListarGrado([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var lista = repos.Listar();
                respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(lista);
                return respuesta;
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }



        [Function("obtenerSolicitud")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "ID de Solicitud")]
        [OpenApiOperation("Listarspec", "obtenerSolicitud", Description = "Sirve para obtener un Solicitud")]
        [OpenApiRequestBody("application/json", typeof(Solicitud),
   Description = "Solicitud modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Solicitud),
            Description = "Obtendra un Solicitud ")]
        public async Task<HttpResponseData> ObtenerGrado([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "obtenerSolicitud/{id}")] HttpRequestData req, string id)
        {
            HttpResponseData respuetsa;
            try
            {
                var lista = repos.Obtener(id);
                respuetsa = req.CreateResponse(HttpStatusCode.OK);
                await respuetsa.WriteAsJsonAsync(lista);
                return respuetsa;
            }
            catch (Exception)
            {

                respuetsa = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuetsa;
            }
        }

        [Function("ModificarSolicitud")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "ID de Solicitud")]
        [OpenApiOperation("Listarspec", "ModificarSolicitud", Description = "Sirve para modificar un Solicitud")]
        [OpenApiRequestBody("application/json", typeof(Solicitud),
   Description = "Solicitud modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Solicitud),
            Description = "Modificara un Solicitud ")]
        public async Task<HttpResponseData> ModificarGrado(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarSolicitud/{id}")] HttpRequestData req,
            string id)
        {
            HttpResponseData respuesta;
            try
            {
                var d = await req.ReadFromJsonAsync<Solicitud>() ?? throw new Exception("Debe ingresar los datos de la solicitud a modificar");
                d.Id = new MongoDB.Bson.ObjectId(id);
                bool resultado = await repos.Actualizar(d);
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



        [Function("EliminarSolicitud")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "ID de Solicitud")]
        [OpenApiOperation("Listarspec", "EliminarSolicitud", Description = "Sirve para eliminar un Solicitud")]
        [OpenApiRequestBody("application/json", typeof(Solicitud),
   Description = "Solicitud modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Solicitud),
            Description = "Eliminara un Solicitud ")]
        public async Task<HttpResponseData> EliminarGrado(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Solicitud/{id}")] HttpRequestData req,
            string id)
        {
            HttpResponseData respuesta;
            try
            {
                bool resultado = await repos.Eliminar(id);
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
