
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Coling.API.Bolsatrabajo.Contratos.Repositorios;
using Coling.API.Bolsatrabajo.Modelo;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Coling.API.Bolsatrabajo.EndPoints
{
    public class IdiomaFunction
    {
        private readonly ILogger<IdiomaFunction> _logger;
        private readonly IIdiomaRepositorio repos;

        public IdiomaFunction(ILogger<IdiomaFunction> logger, IIdiomaRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }
        [Function("InsertarIdioma")]
        [OpenApiRequestBody("application/json", typeof(Idioma),
   Description = "Idioma modelo")]
        [OpenApiOperation("Listarspec", "InsertarIdioma", Description = "Sirve para insertar un idioma")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Idioma),
            Description = "Insertara un idioma ")]
        public async Task<HttpResponseData> InsertarGrado([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            HttpResponseData respuetsa;
            try
            {
                var registro = await req.ReadFromJsonAsync<Idioma>() ?? throw new Exception("Debe ingresar un Idioma con todos sus datos");

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
       
        
        [Function("ListarIdiomas")]
        [OpenApiRequestBody("application/json", typeof(Idioma),
   Description = "Idioma modelo")]
        [OpenApiOperation("Listarspec", "ListarIdiomas", Description = "Sirve para listar los idiomas")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
    bodyType: typeof(Idioma),
    Description = "Listará los idiomas")]
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



        [Function("obtenerIdioma")]
        [OpenApiRequestBody("application/json", typeof(Idioma),
   Description = "Idioma modelo")]
        [OpenApiOperation("Listarspec", "obtenerIdioma", Description = "Sirve para obtener un idioma")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Idioma),
            Description = "Obtendra un idioma ")]
        public async Task<HttpResponseData> ObtenerGrado([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "obtenerIdioma/{id}")] HttpRequestData req, string id)
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

        [Function("ModificarIdioma")]
        [OpenApiRequestBody("application/json", typeof(Idioma),
   Description = "Idioma modelo")]
        [OpenApiOperation("Listarspec", "ModificarIdioma", Description = "Sirve para modificar un idioma")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Idioma),
            Description = "Modificara un idioma ")]
        public async Task<HttpResponseData> ModificarGrado(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarIdioma/{id}")] HttpRequestData req,
            string id)
        {
            HttpResponseData respuesta;
            try
            {
                var d = await req.ReadFromJsonAsync<Idioma>() ?? throw new Exception("Debe ingresar los datos del grado a modificar");
                d.Id=new MongoDB.Bson.ObjectId(id);
                bool resultado = await repos.Actualizar(d );
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



        [Function("EliminarIdioma")]
        [OpenApiOperation("Listarspec", "EliminarIdioma", Description = "Sirve para eliminar un idioma")]
        [OpenApiRequestBody("application/json", typeof(Idioma),
   Description = "Idioma modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Idioma),
            Description = "Eliminara un idioma ")]
        public async Task<HttpResponseData> EliminarGrado(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Idioma/{id}")] HttpRequestData req,
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
