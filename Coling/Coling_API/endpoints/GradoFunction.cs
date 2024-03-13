
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
        public async Task<HttpResponseData> ListaGrados([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarGrados")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listaGradoes = GradoLogic.ListarLosGrados();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listaGradoes.Result);
            return respuesta;
        }
        [Function("InsertarGrado")]
        public async Task<HttpResponseData> InsertarGrado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarGrado")] HttpRequestData req)
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
        public async Task<HttpResponseData> ModificarGrado([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarGrado/{id}")] HttpRequestData req, int id)
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
        public async Task<HttpResponseData> EliminarGrado([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarGrado/{id}")] HttpRequestData req, int id)
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
        public async Task<HttpResponseData> SeleccionarGrado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "seleccionarGrado/{id}")] HttpRequestData req,int id)
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
