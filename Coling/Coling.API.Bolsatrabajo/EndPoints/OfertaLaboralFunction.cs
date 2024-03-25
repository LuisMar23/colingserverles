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
    public class OfertaLaboralFunction
    {
        private readonly ILogger<OfertaLaboralFunction> _logger;
        private readonly IOfertaLaboralRepositorio repos;

        public OfertaLaboralFunction(ILogger<OfertaLaboralFunction> logger, IOfertaLaboralRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }
        [Function("InsertarOfertaLaboral")]
        [OpenApiOperation("Listarspec", "InsertarOfertaLaboral", Description = "Sirve para insertar un OfertaLaboral")]
        [OpenApiRequestBody("application/json", typeof(OfertaLaboral),
   Description = "Oferta Laboral modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(OfertaLaboral),
            Description = "Insertara un OfertaLaboral ")]
        public async Task<HttpResponseData> InsertarGrado([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            HttpResponseData respuetsa;
            try
            {
                var registro = await req.ReadFromJsonAsync<OfertaLaboral>() ?? throw new Exception("Debe ingresar un OfertaLaboral con todos sus datos");

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


        [Function("ListarOfertaLaborals")]
        [OpenApiOperation("Listarspec", "ListarOfertaLaborals", Description = "Sirve para listar los OfertaLaborals")]
        [OpenApiRequestBody("application/json", typeof(OfertaLaboral),
   Description = "Oferta Laboral modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
    bodyType: typeof(OfertaLaboral),
    Description = "Listará los OfertaLaborals")]
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



        [Function("obtenerOfertaLaboral")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "ID de OfertaLaboral")]
        [OpenApiOperation("Listarspec", "obtenerOfertaLaboral", Description = "Sirve para obtener un OfertaLaboral")]
        [OpenApiRequestBody("application/json", typeof(OfertaLaboral),
   Description = "Oferta Laboral modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(OfertaLaboral),
            Description = "Obtendra un OfertaLaboral ")]
        public async Task<HttpResponseData> ObtenerGrado([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "obtenerOfertaLaboral/{id}")] HttpRequestData req, string id)
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

        [Function("ModificarOfertaLaboral")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "ID de OfertaLaboral")]
        [OpenApiOperation("Listarspec", "ModificarOfertaLaboral", Description = "Sirve para modificar un OfertaLaboral")]
        [OpenApiRequestBody("application/json", typeof(OfertaLaboral),
   Description = "Oferta Laboral modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(OfertaLaboral),
            Description = "Modificara un OfertaLaboral ")]
        public async Task<HttpResponseData> ModificarGrado(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarOfertaLaboral/{id}")] HttpRequestData req,
            string id)
        {
            HttpResponseData respuesta;
            try
            {
                var d = await req.ReadFromJsonAsync<OfertaLaboral>() ?? throw new Exception("Debe ingresar los datos de  la oferta laboral modificar");
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



        [Function("EliminarOfertaLaboral")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "ID de OfertaLaboral")]
        [OpenApiOperation("Listarspec", "EliminarOfertaLaboral", Description = "Sirve para eliminar un OfertaLaboral")]
        [OpenApiRequestBody("application/json", typeof(OfertaLaboral),
   Description = "Oferta Laboral modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(OfertaLaboral),
            Description = "Eliminara un OfertaLaboral ")]
        public async Task<HttpResponseData> EliminarGrado(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "OfertaLaboral/{id}")] HttpRequestData req,
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
