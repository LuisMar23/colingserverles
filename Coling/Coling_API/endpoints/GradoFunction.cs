
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
using Coling.API.Afiliados.Contratos;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Coling.API.Afiliados.endpoints
{
    public  class GradoFunction
    {
        private readonly ILogger<GradoFunction> logger;
        private readonly IGradoLogic GradoLogic;

        public GradoFunction(ILogger<GradoFunction> logger, IGradoLogic GradoLogic)
        {
            this.logger = logger;
            this.GradoLogic = GradoLogic;
        }
        [Function("listarGrados")]

        [OpenApiOperation("Listarspec", "modificarGrado", Description = "Sirve para listar los Grado ")]
        [OpenApiRequestBody("application/json", typeof(Grado),
           Description = "Grado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Grado),
            Description = "listara los Grado ")]
        public async Task<HttpResponseData> ListaGrados([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listarGrados")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listaGradoes = GradoLogic.ListarLosGrados();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listaGradoes.Result);
            return respuesta;
        }
        [Function("InsertarGrado")]

        [OpenApiOperation("Listarspec", "modificarGrado", Description = "Sirve para insertar un Grado ")]
        [OpenApiRequestBody("application/json", typeof(Grado),
           Description = "Grado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Grado),
            Description = "insertara un Grado ")]
        public async Task<HttpResponseData> InsertarGrado([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertarGrado")] HttpRequestData req)
        {
            logger.LogInformation("ejecutando para insertar Grados");
            try
            {
                var a = await req.ReadFromJsonAsync<Grado>() ?? throw new Exception("Debe ingresar un Grado con todos sus datos");
                bool r = await GradoLogic.CrearGrado(a);
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


        [Function("modificarGrado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Grado")]
        [OpenApiOperation("Listarspec", "modificarGrado", Description = "Sirve para modificar un Grado ")]
        [OpenApiRequestBody("application/json", typeof(Grado),
           Description = "Grado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Grado),
            Description = "modificara un Grado ")]
        public async Task<HttpResponseData> ModificarGrado([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarGrado/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var Grado = await req.ReadFromJsonAsync<Grado>() ?? throw new Exception("Debe proporcionar los datos del Grado a modificar");
                bool m = await GradoLogic.ModificarGrado(Grado, id);
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


        [Function("eliminarGrado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Grado")]
        [OpenApiOperation("Listarspec", "modificarGrado", Description = "Sirve para eliminar un Grado ")]
        [OpenApiRequestBody("application/json", typeof(Grado),
           Description = "Grado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Grado),
            Description = "eliminara un Grado ")]
        public async Task<HttpResponseData> EliminarGrado([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "eliminarGrado/{id}")] HttpRequestData req, int id)
        {
            try
            {
                bool eliminado = await GradoLogic.EliminarGrado(id);
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

        [Function("seleccionarGrado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Grado")]
        [OpenApiOperation("Listarspec", "modificarGrado", Description = "Sirve para seleccionar un Grado ")]
        [OpenApiRequestBody("application/json", typeof(Grado),
           Description = "Grado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Grado),
            Description = "seleccionara un Grado ")]
        public async Task<HttpResponseData> SeleccionarGrado([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "seleccionarGrado/{id}")] HttpRequestData req,int id)
        {

            logger.LogInformation("Ejecutando para seleccionar un grado");

            try
            {

           

                var Grado = await GradoLogic.ObtenerGradoPorId(id);
                if (Grado == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }


                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(Grado);
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
