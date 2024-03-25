using Coling.API.Afiliados.Contratos;
using Coling.Shared;
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

namespace Coling.API.Afiliados.endpoints
{
    public class IdiomaFunction
    {
        private readonly ILogger<IdiomaFunction> logger;
        private readonly IIdiomaLogic IdiomaLogic;

        public IdiomaFunction(ILogger<IdiomaFunction> logger, IIdiomaLogic IdiomaLogic)
        {
            this.logger = logger;
            this.IdiomaLogic = IdiomaLogic;
        }
        [Function("listarIdiomas")]
        [OpenApiOperation("Listarspec", "listarIdiomas", Description = "Sirve para listar los Idiomas ")]
        [OpenApiRequestBody("application/json", typeof(Idioma),
           Description = "Idioma modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Idioma),
            Description = "listara los Idiomas ")]
        public async Task<HttpResponseData> ListaIdiomas([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listarIdiomas")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listaIdiomaes = IdiomaLogic.ListarIdiomaesTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listaIdiomaes.Result);
            return respuesta;
        }
        [Function("InsertarIdioma")]
        [OpenApiOperation("Listarspec", "InsertarIdioma", Description = "Sirve para insertar un Idioma ")]
        [OpenApiRequestBody("application/json", typeof(Idioma),
           Description = "Idioma modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Idioma),
            Description = "insertara un Idioma ")]
        public async Task<HttpResponseData> InsertarIdioma([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertarIdioma")] HttpRequestData req)
        {
            logger.LogInformation("ejecutando para insertar Idiomas");
            try
            {
                var a = await req.ReadFromJsonAsync<Idioma>() ?? throw new Exception("Debe ingresar un Idioma con todos sus datos");
                bool r = await IdiomaLogic.InsertarIdioma(a);
                if (r)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                var error = req.CreateResponse(HttpStatusCode.BadRequest);
                await error.WriteAsJsonAsync(ex.Message);
                return error;
            }

        }


        [Function("modificarIdioma")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Idioma")]
        [OpenApiOperation("Listarspec", "modificarIdioma", Description = "Sirve para modificar un Idioma ")]
        [OpenApiRequestBody("application/json", typeof(Idioma),
           Description = "Idioma modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Idioma),
            Description = "modificara un Idioma ")]
        public async Task<HttpResponseData> ModificarIdioma([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarIdioma/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var Idioma = await req.ReadFromJsonAsync<Idioma>() ?? throw new Exception("Debe proporcionar los datos del Idioma a modificar");
                bool m = await IdiomaLogic.ModificarIdioma(Idioma, id);
                if (m)
                {
                    return req.CreateResponse(HttpStatusCode.OK);
                }

                return req.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var error = req.CreateResponse(HttpStatusCode.BadRequest);
                await error.WriteAsJsonAsync(ex.Message);
                return error;
            }
        }


        [Function("eliminarIdioma")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Idioma")]
        [OpenApiOperation("Listarspec", "eliminarIdioma", Description = "Sirve para eliminar un Idioma ")]
        [OpenApiRequestBody("application/json", typeof(Idioma),
           Description = "Idioma modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Idioma),
            Description = "eliminara un Idioma ")]
        public async Task<HttpResponseData> EliminarIdioma([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "eliminarIdioma/{id}")] HttpRequestData req, int id)
        {
            try
            {
                bool eliminado = await IdiomaLogic.EliminarIdioma(id);
                if (eliminado)
                {
                    return req.CreateResponse(HttpStatusCode.OK);
                }

                return req.CreateResponse(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                var error = req.CreateResponse(HttpStatusCode.BadRequest);
                await error.WriteAsJsonAsync(ex.Message);
                return error;
            }
        }

        [Function("seleccionarIdioma")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Idioma")]
        [OpenApiOperation("Listarspec", "seleccionarIdioma", Description = "Sirve para seleccionar un Idioma ")]
        [OpenApiRequestBody("application/json", typeof(Idioma),
           Description = "Idioma modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Idioma),
            Description = "seleccionara un Idioma ")]
        public async Task<HttpResponseData> SeleccionarIdioma([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "seleccionarIdioma/{id}")] HttpRequestData req, int id)
        {

            logger.LogInformation("Ejecutando para seleccionar una Idioma");

            try
            {



                var Idioma = await IdiomaLogic.ObtenerIdioma(id);
                if (Idioma == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }


                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(Idioma);
                return respuesta;
            }
            catch (Exception ex)
            {
                var error = req.CreateResponse(HttpStatusCode.BadRequest);
                await error.WriteAsJsonAsync(ex.Message);
                return error;
            }
        }
    }
}
