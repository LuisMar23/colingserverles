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
    public class TipoSocialFunction
    {
        private readonly ILogger<TipoSocialFunction> logger;
        private readonly ITipoSocialLogic TipoSocialLogic;

        public TipoSocialFunction(ILogger<TipoSocialFunction> logger, ITipoSocialLogic TipoSocialLogic)
        {
            this.logger = logger;
            this.TipoSocialLogic = TipoSocialLogic;
        }
        [Function("listarTipoSocials")]
 
        [OpenApiOperation("Listarspec", "listarTipoSocialsl", Description = "Sirve para listar los TipoSocial ")]
        [OpenApiRequestBody("application/json", typeof(TipoSocial),
           Description = "TipoSocial modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(TipoSocial),
            Description = "listara TipoSocial ")]
        public async Task<HttpResponseData> ListaTipoSocials([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listarTipoSocials")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listaTipoSociales = TipoSocialLogic.ListarTipoSocialesTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listaTipoSociales.Result);
            return respuesta;
        }
        [Function("InsertarTipoSocial")]

        [OpenApiOperation("Listarspec", "InsertarTipoSocial", Description = "Sirve para insertar un TipoSocial ")]
        [OpenApiRequestBody("application/json", typeof(TipoSocial),
           Description = "TipoSocial modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(TipoSocial),
            Description = "insertara un TipoSocial ")]
        public async Task<HttpResponseData> InsertarTipoSocial([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertarTipoSocial")] HttpRequestData req)
        {
            logger.LogInformation("ejecutando para insertar TipoSocials");
            try
            {
                var a = await req.ReadFromJsonAsync<TipoSocial>() ?? throw new Exception("Debe ingresar un TipoSocial con todos sus datos");
                bool r = await TipoSocialLogic.InsertarTipoSocial(a);
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


        [Function("modificarTipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del TipoSocial")]
        [OpenApiOperation("Listarspec", "modificarTipoSocial", Description = "Sirve para modificar un TipoSocial ")]
        [OpenApiRequestBody("application/json", typeof(TipoSocial),
           Description = "TipoSocial modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(TipoSocial),
            Description = "modificara una TipoSocial ")]
        public async Task<HttpResponseData> ModificarTipoSocial([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarTipoSocial/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var TipoSocial = await req.ReadFromJsonAsync<TipoSocial>() ?? throw new Exception("Debe proporcionar los datos del TipoSocial a modificar");
                bool m = await TipoSocialLogic.ModificarTipoSocial(TipoSocial, id);
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


        [Function("eliminarTipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del TipoSocial")]
        [OpenApiOperation("Listarspec", "eliminarTipoSocial", Description = "Sirve para eliminar un TipoSocial ")]
        [OpenApiRequestBody("application/json", typeof(TipoSocial),
           Description = "TipoSocial modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(TipoSocial),
            Description = "eliminara un TipoSocial ")]
        public async Task<HttpResponseData> EliminarTipoSocial([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "eliminarTipoSocial/{id}")] HttpRequestData req, int id)
        {
            try
            {
                bool eliminado = await TipoSocialLogic.EliminarTipoSocial(id);
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

        [Function("seleccionarTipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del TipoSocial")]
        [OpenApiOperation("Listarspec", "seleccionarTipoSocial", Description = "Sirve para seleccionar una TipoSocial ")]
        [OpenApiRequestBody("application/json", typeof(TipoSocial),
           Description = "TipoSocial modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(TipoSocial),
            Description = "seleccionara una TipoSocial ")]
        public async Task<HttpResponseData> SeleccionarTipoSocial([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "seleccionarTipoSocial/{id}")] HttpRequestData req,int id)
        {

            logger.LogInformation("Ejecutando para seleccionar un tipo social");

            try
            {
                var TipoSocial = await TipoSocialLogic.ObtenerTipoSocial(id);
                if (TipoSocial == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }


                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(TipoSocial);
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
