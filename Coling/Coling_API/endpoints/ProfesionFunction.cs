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
    public class ProfesionFunction
    {
        private readonly ILogger<ProfesionFunction> logger;
        private readonly IProfesionLogic ProfesionLogic;

        public ProfesionFunction(ILogger<ProfesionFunction> logger, IProfesionLogic ProfesionLogic)
        {
            this.logger = logger;
            this.ProfesionLogic = ProfesionLogic;
        }
        [Function("listarProfesions")]
        public async Task<HttpResponseData> ListaProfesions([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarProfesions")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listaProfesiones = ProfesionLogic.ListarProfesionesTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listaProfesiones.Result);
            return respuesta;
        }
        [Function("InsertarProfesion")]
        public async Task<HttpResponseData> InsertarProfesion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarProfesion")] HttpRequestData req)
        {
            logger.LogInformation("ejecutando para insertar Profesions");
            try
            {
                var a = await req.ReadFromJsonAsync<Profesion>() ?? throw new Exception("Debe ingresar un Profesion con todos sus datos");
                bool r = await ProfesionLogic.InsertarProfesion(a);
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


        [Function("modificarProfesion")]
        public async Task<HttpResponseData> ModificarProfesion([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarProfesion/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var Profesion = await req.ReadFromJsonAsync<Profesion>() ?? throw new Exception("Debe proporcionar los datos del Profesion a modificar");
                bool m = await ProfesionLogic.ModificarProfesion(Profesion, id);
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


        [Function("eliminarProfesion")]
        public async Task<HttpResponseData> EliminarProfesion([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarProfesion/{id}")] HttpRequestData req, int id)
        {
            try
            {
                bool eliminado = await ProfesionLogic.EliminarProfesion(id);
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

        [Function("seleccionarProfesion")]
        public async Task<HttpResponseData> SeleccionarProfesion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "seleccionarProfesion/{id}")] HttpRequestData req,int id)
        {

            logger.LogInformation("Ejecutando para seleccionar una profesion");

            try
            {

            
                var Profesion = await ProfesionLogic.ObtenerProfesion(id);
                if (Profesion == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }


                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(Profesion);
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
