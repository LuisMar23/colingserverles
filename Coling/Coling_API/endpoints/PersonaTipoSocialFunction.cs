
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
    internal class PersonaTipoSocialTipoSocialFunction
    {
        private readonly ILogger<PersonaTipoSocialTipoSocialFunction> logger;
        private readonly IPersonaTipoSocialLogic PersonaTipoSocialTipoLogic;

        public PersonaTipoSocialTipoSocialFunction(ILogger<PersonaTipoSocialTipoSocialFunction> logger, IPersonaTipoSocialLogic PersonaTipoSocialTipoLogic)
        {
            this.logger = logger;
            this.PersonaTipoSocialTipoLogic = PersonaTipoSocialTipoLogic;
        }
        [Function("listarPersonaTipoSocialTipos")]
    
        [OpenApiOperation("Listarspec", "listarPersonaTipoSocialTipos", Description = "Sirve para seleccionar los PersonaTipoSocial ")]
        [OpenApiRequestBody("application/json", typeof(PersonaTipoSocial),
           Description = "PersonaTipoSocial modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(PersonaTipoSocial),
            Description = "seleccionara los PersonaTipoSocial ")]
        public async Task<HttpResponseData> ListaPersonaTipoSocialTipos([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listarPersonaTipoSocialTipos")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listaPersonaTipoSocialTipoes = PersonaTipoSocialTipoLogic.ListarPersonaTiposTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listaPersonaTipoSocialTipoes.Result);
            return respuesta;
        }
        [Function("InsertarPersonaTipoSocialTipo")]

        [OpenApiOperation("Listarspec", "InsertarPersonaTipoSocialTipo", Description = "Sirve para insertar un PersonaTipoSocial ")]
        [OpenApiRequestBody("application/json", typeof(PersonaTipoSocial),
           Description = "PersonaTipoSocial modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(PersonaTipoSocial),
            Description = "insertara un PersonaTipoSocial ")]
        public async Task<HttpResponseData> InsertarPersonaTipoSocialTipo([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertarPersonaTipoSocialTipo")] HttpRequestData req)
        {
            logger.LogInformation("ejecutando para insertar PersonaTipoSocialTipos");
            try
            {
                var a = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar un PersonaTipoSocialTipo con todos sus datos");
                bool r = await PersonaTipoSocialTipoLogic.InsertarPersonaTipo(a);
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


        [Function("modificarPersonaTipoSocialTipo")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del PersonaTipoSocial")]
        [OpenApiOperation("Listarspec", "modificarPersonaTipoSocialTipo", Description = "Sirve para modificar una PersonaTipoSocial ")]
        [OpenApiRequestBody("application/json", typeof(PersonaTipoSocial),
           Description = "PersonaTipoSocial modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(PersonaTipoSocial),
            Description = "modificara una PersonaTipoSocial ")]
        public async Task<HttpResponseData> ModificarPersonaTipoSocialTipo([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarPersonaTipoSocialTipo/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var PersonaTipoSocialTipo = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe proporcionar los datos del PersonaTipoSocialTipo a modificar");
                bool m = await PersonaTipoSocialTipoLogic.ModificarPersonaTipo(PersonaTipoSocialTipo, id);
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


        [Function("eliminarPersonaTipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del PersonaTipoSocial")]
        [OpenApiOperation("Listarspec", "eliminarPersonaTipoSocial", Description = "Sirve para eliminar una PersonaTipoSocial ")]
        [OpenApiRequestBody("application/json", typeof(PersonaTipoSocial),
           Description = "PersonaTipoSocial modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(PersonaTipoSocial),
            Description = "eliminara un PersonaTipoSocial ")]
        public async Task<HttpResponseData> EliminarPersonaTipoSocialTipo([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "eliminarPersonaTipoSocialTipo/{id}")] HttpRequestData req, int id)
        {
            try
            {
                bool eliminado = await PersonaTipoSocialTipoLogic.EliminarPersonaTipo(id);
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

        [Function("seleccionarPersonaTipoSocial")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del PersonaTipoSocial")]
        [OpenApiOperation("Listarspec", "seleccionarPersonaTipoSocial", Description = "Sirve para seleccionar un PersonaTipoSocial ")]
        [OpenApiRequestBody("application/json", typeof(PersonaTipoSocial),
           Description = "PersonaTipoSocial modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(PersonaTipoSocial),
            Description = "seleccionara un PersonaTipoSocial ")]
        public async Task<HttpResponseData> SeleccionarPersonaTipoSocialTipo([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "seleccionarPersonaTipoSocialTipo/{id}")] HttpRequestData req,int id)
        {

            logger.LogInformation("Ejecutando para seleccionar una PersonaTipoSocial tipo");

            try
            {


                var PersonaTipoSocialTipo = await PersonaTipoSocialTipoLogic.ObtenerPersonaTipo(id);
                if (PersonaTipoSocialTipo == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }


                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(PersonaTipoSocialTipo);
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
