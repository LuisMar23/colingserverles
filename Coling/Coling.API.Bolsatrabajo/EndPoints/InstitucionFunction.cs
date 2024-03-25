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
    public class InstitucionFunction
    {
        private readonly ILogger<InstitucionFunction> _logger;
        private readonly IInstitucionRepositorio repos;

        public InstitucionFunction(ILogger<InstitucionFunction> logger, IInstitucionRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }
        [Function("InsertarInstitucion")]
        [OpenApiOperation("Listarspec", "InsertarInstitucion", Description = "Sirve para insertar un Institucion")]
        [OpenApiRequestBody("application/json", typeof(Institucion),
   Description = "Institucion modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Institucion),
            Description = "Insertara un Institucion ")]
        public async Task<HttpResponseData> InsertarGrado([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            HttpResponseData respuetsa;
            try
            {
                var registro = await req.ReadFromJsonAsync<Institucion>() ?? throw new Exception("Debe ingresar un Institucion con todos sus datos");

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


        [Function("ListarInstitucions")]
        [OpenApiOperation("Listarspec", "ListarInstitucions", Description = "Sirve para listar los Institucions")]
        [OpenApiRequestBody("application/json", typeof(Institucion),
   Description = "Institucion modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
    bodyType: typeof(Institucion),
    Description = "Listará los Institucions")]
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



        [Function("obtenerInstitucion")]
         [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "ID de Institucion")]
        [OpenApiOperation("Listarspec", "obtenerInstitucion", Description = "Sirve para obtener un Institucion")]
        [OpenApiRequestBody("application/json", typeof(Institucion),
   Description = "Institucion modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Institucion),
            Description = "Obtendra un Institucion ")]
        public async Task<HttpResponseData> ObtenerGrado([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "obtenerInstitucion/{id}")] HttpRequestData req, string id)
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

        [Function("ModificarInstitucion")]

        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "ID de Institucion")]
        [OpenApiOperation("Listarspec", "ModificarInstitucion", Description = "Sirve para modificar un Institucion")]
        [OpenApiRequestBody("application/json", typeof(Institucion),
   Description = "Institucion modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Institucion),
            Description = "Modificara un Institucion ")]
        public async Task<HttpResponseData> ModificarGrado(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarInstitucion/{id}")] HttpRequestData req,
            string id)
        {
            HttpResponseData respuesta;
            try
            {
                var d = await req.ReadFromJsonAsync<Institucion>() ?? throw new Exception("Debe ingresar los datos de la institucion  modificar");
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



        [Function("EliminarInstitucion")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "ID de Institucion")]
        [OpenApiOperation("Listarspec", "EliminarInstitucion", Description = "Sirve para eliminar un Institucion")]
        [OpenApiRequestBody("application/json", typeof(Institucion),
   Description = "Institucion modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Institucion),
            Description = "Eliminara un Institucion ")]
        public async Task<HttpResponseData> EliminarGrado(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Institucion/{id}")] HttpRequestData req,
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
