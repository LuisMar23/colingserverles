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
    public class AfiliadoIdiomaFunction
    {
        private readonly ILogger<AfiliadoIdiomaFunction> logger;
        private readonly IAfiliadoIdiomaLogic AfiliadoIdiomaLogic;

        public AfiliadoIdiomaFunction(ILogger<AfiliadoIdiomaFunction> logger, IAfiliadoIdiomaLogic AfiliadoIdiomaLogic)
        {
            this.logger = logger;
            this.AfiliadoIdiomaLogic = AfiliadoIdiomaLogic;
        }
        [Function("listarAfiliadoIdiomas")]
   
        [OpenApiOperation("Listarspec", "listarAfiliadoIdiomas", Description = "Sirve para insertar listar los afiliados idiomas")]
        [OpenApiRequestBody("application/json", typeof(AfiliadoIdioma),
           Description = "Afiliado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(AfiliadoIdioma),
            Description = "Insertar  Afiliado Idioma")]
        public async Task<HttpResponseData> ListaAfiliadoIdiomas([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarAfiliadoIdiomas")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listaAfiliadoIdiomaes = AfiliadoIdiomaLogic.ListarAfiliadoIdiomaesTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listaAfiliadoIdiomaes.Result);
            return respuesta;
        }
        [Function("InsertarAfiliadoIdioma")]

        [OpenApiOperation("Listarspec", "InsertarAfiliadoIdioma", Description = "Sirve para insertar afiliado idioma")]
        [OpenApiRequestBody("application/json", typeof(AfiliadoIdioma),
           Description = "Afiliado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(AfiliadoIdioma),
            Description = "Insertar la Afiliado")]
        public async Task<HttpResponseData> InsertarAfiliadoIdioma([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertarAfiliadoIdioma")] HttpRequestData req)
        {
            logger.LogInformation("ejecutando para insertar AfiliadoIdiomas");
            try
            {
                var a = await req.ReadFromJsonAsync<AfiliadoIdioma>() ?? throw new Exception("Debe ingresar un AfiliadoIdioma con todos sus datos");
                bool r = await AfiliadoIdiomaLogic.InsertarAfiliadoIdioma(a);
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


        [Function("modificarAfiliadoIdioma")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Afiliado")]
        [OpenApiOperation("Listarspec", "modificarAfiliadoIdioma", Description = "Sirve para modificar un Afiliado idioma")]
        [OpenApiRequestBody("application/json", typeof(Afiliado),
           Description = "Afiliado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(AfiliadoIdioma),
            Description = "modificara un Afiliado idioma")]
        public async Task<HttpResponseData> ModificarAfiliadoIdioma([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarAfiliadoIdioma/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var AfiliadoIdioma = await req.ReadFromJsonAsync<AfiliadoIdioma>() ?? throw new Exception("Debe proporcionar los datos del AfiliadoIdioma a modificar");
                bool m = await AfiliadoIdiomaLogic.ModificarAfiliadoIdioma(AfiliadoIdioma, id);
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


        [Function("eliminarAfiliadoIdioma")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Afiliado idioma")]
        [OpenApiOperation("Listarspec", "eliminarAfiliadoIdioma", Description = "Sirve para eliminar un Afiliado idioma")]
        [OpenApiRequestBody("application/json", typeof(AfiliadoIdioma),
           Description = "Afiliado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(AfiliadoIdioma),
            Description = "eliminara un Afiliado idioma")]
        public async Task<HttpResponseData> EliminarAfiliadoIdioma([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "eliminarAfiliadoIdioma/{id}")] HttpRequestData req, int id)
        {
            try
            {
                bool eliminado = await AfiliadoIdiomaLogic.EliminarAfiliadoIdioma(id);
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

        [Function("seleccionarAfiliadoIdioma")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Afiliado idioma")]
        [OpenApiOperation("Listarspec", "seleccionarAfiliadoIdioma", Description = "Sirve para seleccionar un Afiliado idioma")]
        [OpenApiRequestBody("application/json", typeof(AfiliadoIdioma),
           Description = "Afiliado idioma modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Afiliado),
            Description = "seleccionara un  Afiliado idioma")]
        public async Task<HttpResponseData> SeleccionarAfiliadoIdioma([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "seleccionarAfiliadoIdioma/{id}")] HttpRequestData req, int id)
        {

            logger.LogInformation("Ejecutando para seleccionar una AfiliadoIdioma");

            try
            {



                var AfiliadoIdioma = await AfiliadoIdiomaLogic.ObtenerAfiliadoIdioma(id);
                if (AfiliadoIdioma == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }


                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(AfiliadoIdioma);
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
