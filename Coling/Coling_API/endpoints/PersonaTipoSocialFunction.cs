
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

namespace Coling.API.Afiliados.endpoints
{
    internal class PersonaTipoSocialFunction
    {
        private readonly ILogger<PersonaTipoSocialFunction> logger;
        private readonly IPersonaTipoSocialLogic PersonaTipoLogic;

        public PersonaTipoSocialFunction(ILogger<PersonaTipoSocialFunction> logger, IPersonaTipoSocialLogic PersonaTipoLogic)
        {
            this.logger = logger;
            this.PersonaTipoLogic = PersonaTipoLogic;
        }
        [Function("listarPersonaTipos")]
        public async Task<HttpResponseData> ListaPersonaTipos([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarPersonaTipos")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listaPersonaTipoes = PersonaTipoLogic.ListarPersonaTiposTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listaPersonaTipoes.Result);
            return respuesta;
        }
        [Function("InsertarPersonaTipo")]
        public async Task<HttpResponseData> InsertarPersonaTipo([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarPersonaTipo")] HttpRequestData req)
        {
            logger.LogInformation("ejecutando para insertar PersonaTipos");
            try
            {
                var a = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe ingresar un PersonaTipo con todos sus datos");
                bool r = await PersonaTipoLogic.InsertarPersonaTipo(a);
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


        [Function("modificarPersonaTipo")]
        public async Task<HttpResponseData> ModificarPersonaTipo([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarPersonaTipo/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var PersonaTipo = await req.ReadFromJsonAsync<PersonaTipoSocial>() ?? throw new Exception("Debe proporcionar los datos del PersonaTipo a modificar");
                bool m = await PersonaTipoLogic.ModificarPersonaTipo(PersonaTipo, id);
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


        [Function("eliminarPersonaTipo")]
        public async Task<HttpResponseData> EliminarPersonaTipo([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarPersonaTipo/{id}")] HttpRequestData req, int id)
        {
            try
            {
                bool eliminado = await PersonaTipoLogic.EliminarPersonaTipo(id);
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

        [Function("seleccionarPersonaTipo")]
        public async Task<HttpResponseData> SeleccionarPersonaTipo([HttpTrigger(AuthorizationLevel.Function, "get", Route = "seleccionarPersonaTipo/{id}")] HttpRequestData req,int id)
        {

            logger.LogInformation("Ejecutando para seleccionar una persona tipo");

            try
            {


                var PersonaTipo = await PersonaTipoLogic.ObtenerPersonaTipo(id);
                if (PersonaTipo == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }


                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(PersonaTipo);
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
