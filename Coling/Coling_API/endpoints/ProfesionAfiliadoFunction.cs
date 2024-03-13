using Coling.API.Afiliados.Contratos;
using Coling.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.endpoints
{
    public class ProfesionAfiliadoFunction
    {
        private readonly ILogger<ProfesionAfiliadoFunction> logger;
        private readonly IProfesionAfiliadoLogic ProfesionAfiliadoLogic;

        public ProfesionAfiliadoFunction(ILogger<ProfesionAfiliadoFunction> logger, IProfesionAfiliadoLogic ProfesionAfiliadoLogic)
        {
            this.logger = logger;
            this.ProfesionAfiliadoLogic = ProfesionAfiliadoLogic;
        }
        [Function("listarProfesionAfiliados")]
        public async Task<HttpResponseData> ListaProfesionAfiliados([HttpTrigger(AuthorizationLevel.Function, "get", Route = "listarProfesionAfiliados")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listaProfesionAfiliadoes = ProfesionAfiliadoLogic.ListarProfesionesAfiliadosTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listaProfesionAfiliadoes.Result);
            return respuesta;
        }
        [Function("InsertarProfesionAfiliado")]
        public async Task<HttpResponseData> InsertarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarProfesionAfiliado")] HttpRequestData req)
        {
            logger.LogInformation("ejecutando para insertar ProfesionAfiliados");
            try
            {
                var a = await req.ReadFromJsonAsync<ProfesionAfiliado>() ?? throw new Exception("Debe ingresar un ProfesionAfiliado con todos sus datos");
                bool r = await ProfesionAfiliadoLogic.InsertarProfesionAfiliado(a);
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


        [Function("modificarProfesionAfiliado")]
        public async Task<HttpResponseData> ModificarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarProfesionAfiliado/{id}")] HttpRequestData req, int id)
        {
            try
            {
                var ProfesionAfiliado = await req.ReadFromJsonAsync<ProfesionAfiliado>() ?? throw new Exception("Debe proporcionar los datos del ProfesionAfiliado a modificar");
                bool m = await ProfesionAfiliadoLogic.ModificarProfesionAfiliado(ProfesionAfiliado, id);
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


        [Function("eliminarProfesionAfiliado")]
        public async Task<HttpResponseData> EliminarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarProfesionAfiliado/{id}")] HttpRequestData req, int id)
        {
            try
            {
                bool eliminado = await ProfesionAfiliadoLogic.EliminarProfesionAfiliado(id);
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

        [Function("seleccionarProfesionAfiliado")]
        public async Task<HttpResponseData> SeleccionarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "seleccionarProfesionAfiliado/{id}")] HttpRequestData req,int id)
        {

            logger.LogInformation("Ejecutando para seleccionar una profesion del afiliado");

            try
            {

                var ProfesionAfiliado = await ProfesionAfiliadoLogic.ObtenerProfesionAfiliado(id);
                if (ProfesionAfiliado == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }


                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(ProfesionAfiliado);
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
