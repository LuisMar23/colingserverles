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
    public class TelefonoFunction
    {
        private readonly ILogger<TelefonoFunction> logger;
        private readonly ITelefonoLogic TelefonoLogic;

        public TelefonoFunction(ILogger<TelefonoFunction> logger, ITelefonoLogic TelefonoLogic)
        {
            this.logger = logger;
            this.TelefonoLogic = TelefonoLogic;
        }
        [Function("listarTelefonos")]

        [OpenApiOperation("Listarspec", "listarTelefonos", Description = "Sirve para listar los Telefonos ")]
        [OpenApiRequestBody("application/json", typeof(Telefono),
           Description = "Telefono modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Telefono),
            Description = "listara los Telefonos")]
        public async Task<HttpResponseData> ListaTelefonos([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listarTelefonos")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listaTelefonoes = TelefonoLogic.ListarTelefonosTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listaTelefonoes.Result);
            return respuesta;
        }
        [Function("InsertarTelefono")]

        [OpenApiOperation("Listarspec", "InsertarTelefono", Description = "Sirve para insertar una Telefono ")]
        [OpenApiRequestBody("application/json", typeof(Telefono),
           Description = "Telefono modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Telefono),
            Description = "insertara una Telefono ")]
        public async Task<HttpResponseData> InsertarTelefono([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertarTelefono")] HttpRequestData req)
        {
            logger.LogInformation("ejecutando para insertar Telefonos");
            try
            {
                var a = await req.ReadFromJsonAsync<Telefono>() ?? throw new Exception("Debe ingresar un Telefono con todos sus datos");
                bool r = await TelefonoLogic.InsertarTelefono(a);
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


        [Function("modificarTelefono")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Telefono")]
        [OpenApiOperation("Listarspec", "modificarTelefono", Description = "Sirve para modificar un Telefono ")]
        [OpenApiRequestBody("application/json", typeof(Telefono),
           Description = "Telefono modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Telefono),
            Description = "modificara un Telefono ")]
        public async Task<HttpResponseData> ModificarTelefono([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarTelefono/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var Telefono = await req.ReadFromJsonAsync<Telefono>() ?? throw new Exception("Debe proporcionar los datos del Telefono a modificar");
                bool m = await TelefonoLogic.ModificarTelefono(Telefono, id);
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


        [Function("eliminarTelefono")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Telefono")]
        [OpenApiOperation("Listarspec", "eliminarTelefono", Description = "Sirve para eliminar una Telefono ")]
        [OpenApiRequestBody("application/json", typeof(Telefono),
           Description = "Telefono modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Telefono),
            Description = "eliminara una Telefono ")]
        public async Task<HttpResponseData> EliminarTelefono([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "eliminarTelefono/{id}")] HttpRequestData req, int id)
        {
            try
            {
                bool eliminado = await TelefonoLogic.EliminarTelefono(id);
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

        [Function("seleccionarTelefono")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Telefono")]
        [OpenApiOperation("Listarspec", "seleccionarTelefono", Description = "Sirve para seleccionar una Telefono ")]
        [OpenApiRequestBody("application/json", typeof(Telefono),
           Description = "Telefono modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Telefono),
            Description = "seleccionara una Telefono ")]
        public async Task<HttpResponseData> SeleccionarTelefono([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "seleccionarTelefono/{id}")] HttpRequestData req,int id)
        {

            logger.LogInformation("Ejecutando para seleccionar un telefono");

            try
            {

                var Telefono = await TelefonoLogic.ObtenerTelefono(id);
                if (Telefono == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }


                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(Telefono);
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
