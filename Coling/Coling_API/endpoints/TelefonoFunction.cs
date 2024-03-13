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
        public async Task<HttpResponseData> ListaTelefonos([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarTelefonos")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listaTelefonoes = TelefonoLogic.ListarTelefonosTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listaTelefonoes.Result);
            return respuesta;
        }
        [Function("InsertarTelefono")]
        public async Task<HttpResponseData> InsertarTelefono([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarTelefono")] HttpRequestData req)
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
        public async Task<HttpResponseData> ModificarTelefono([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarTelefono/{id}")] HttpRequestData req, int id)
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
        public async Task<HttpResponseData> EliminarTelefono([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarTelefono/{id}")] HttpRequestData req, int id)
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
        public async Task<HttpResponseData> SeleccionarTelefono([HttpTrigger(AuthorizationLevel.Function, "get", Route = "seleccionarTelefono/{id}")] HttpRequestData req,int id)
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
