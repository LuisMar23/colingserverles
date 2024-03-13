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
    public class AfiliadoFunction
    {
        private readonly ILogger<AfiliadoFunction> logger;
        private readonly IAfiliadoLogic afiliadoLogic;

        public AfiliadoFunction(ILogger<AfiliadoFunction> logger, IAfiliadoLogic afiliadoLogic)
        {
            this.logger = logger;
            this.afiliadoLogic = afiliadoLogic;
        }
        [Function("listarAfiliados")]
        public async Task<HttpResponseData> ListaAfiliados([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarafiliados")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listaafiliados = afiliadoLogic.ListarAfiliadosTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listaafiliados.Result);
            return respuesta;
        }
        [Function("Insertarafiliado")]
        public async Task<HttpResponseData> InsertarAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarafiliado")] HttpRequestData req)
        {
            logger.LogInformation("ejecutando para insertar afiliados");
            try
            {
                var a = await req.ReadFromJsonAsync<Afiliado>() ?? throw new Exception("Debe ingresar un afiliado con todos sus datos");
                bool r = await afiliadoLogic.InsertarAfiliado(a);
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


        [Function("modificarafiliado")]
        public async Task<HttpResponseData> ModificarAfiliado([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarafiliado/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var persona = await req.ReadFromJsonAsync<Afiliado>() ?? throw new Exception("Debe proporcionar los datos del afiliado a modificar");
                bool m = await afiliadoLogic.ModificarAfiliado(persona,id);
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


        [Function("eliminarafiliado")]
        public async Task<HttpResponseData> EliminarAfiliado([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarafiliado/{id}")] HttpRequestData req, int id)
        {
            try
            {
                bool eliminado = await afiliadoLogic.EliminarAfiliado(id);
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

        [Function("seleccionarafiliado")]
        public async Task<HttpResponseData> SeleccionarAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "seleccionarafiliado/{id}")] HttpRequestData req,int id)
        {

            logger.LogInformation("Ejecutando para seleccionar un afiliado");

            try
            {


  
                var afiliado = await afiliadoLogic.ObtenerAfiliado(id);
                if (afiliado == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }


                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(afiliado);
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
