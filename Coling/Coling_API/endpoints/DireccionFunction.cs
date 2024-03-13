
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

namespace Coling.API.direccions.endpoints
{
    public class DireccionFunction
    {
        private readonly ILogger<DireccionFunction> logger;
        private readonly IDireccionLogic direccionLogic;

        public DireccionFunction(ILogger<DireccionFunction> logger, IDireccionLogic direccionLogic)
        {
            this.logger = logger;
            this.direccionLogic = direccionLogic;
        }
        [Function("listardireccions")]
        public async Task<HttpResponseData> Listadireccions([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listardireccions")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listadirecciones = direccionLogic.ListarDireccionesTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listadirecciones.Result);
            return respuesta;
        }
        [Function("Insertardireccion")]
        public async Task<HttpResponseData> Insertardireccion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertardireccion")] HttpRequestData req)
        {
            logger.LogInformation("ejecutando para insertar direccions");
            try
            {
                var a = await req.ReadFromJsonAsync<Direccion>() ?? throw new Exception("Debe ingresar un direccion con todos sus datos");
                bool r = await direccionLogic.InsertarDireccion(a);
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


        [Function("modificardireccion")]
        public async Task<HttpResponseData> Modificardireccion([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificardireccion/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var direccion = await req.ReadFromJsonAsync<Direccion>() ?? throw new Exception("Debe proporcionar los datos del direccion a modificar");
                bool m = await direccionLogic.ModificarDireccion(direccion, id);
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


        [Function("eliminardireccion")]
        public async Task<HttpResponseData> Eliminardireccion([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminardireccion/{id}")] HttpRequestData req, int id)
        {
            try
            {
                bool eliminado = await direccionLogic.EliminarDireccion(id);
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

        [Function("seleccionardireccion")]
        public async Task<HttpResponseData> Seleccionardireccion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "seleccionardireccion/{id}")] HttpRequestData req, int id)
        {

            logger.LogInformation("Ejecutando para seleccionar una direccion");

            try
            {

             

                var direccion = await direccionLogic.ObtenerDireccion(id);
                if (direccion == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }


                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(direccion);
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
