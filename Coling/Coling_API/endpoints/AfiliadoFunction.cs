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
        [OpenApiOperation("Listarspec", "listarAfiliados", Description = "Sirve para insertar la Afiliado")]
        [OpenApiRequestBody("application/json", typeof(Afiliado),
           Description = "Afiliado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Afiliado),
            Description = "Insertar la Afiliado")]
        public async Task<HttpResponseData> ListaAfiliados([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "listarafiliados")] HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listaafiliados = afiliadoLogic.ListarAfiliadosTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listaafiliados.Result);
            return respuesta;
        }

        [Function("InsertarAfiliado")]
        [OpenApiOperation("Listarspec", "InsertarAfiliado", Description = "Sirve para insertar la Afiliado")]
        [OpenApiRequestBody("application/json", typeof(Afiliado),
           Description = "Afiliado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Afiliado),
            Description = "Insertar la Afiliado")]
        public async Task<HttpResponseData> InsertarAfiliado([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertarafiliado")] HttpRequestData req)
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

        [OpenApiOperation("Listarspec", "modificarafiliado", Description = "Sirve para modificar el Afiliado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Afiliado")]
        [OpenApiRequestBody("application/json", typeof(Afiliado),
           Description = "Afiliado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Afiliado),
            Description = "Insertar la Afiliado")]
        public async Task<HttpResponseData> ModificarAfiliado([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarafiliado/{id}")] HttpRequestData req, int id)
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
        [OpenApiOperation("Listarspec", "eliminarafiliado", Description = "Sirve para eliminar un Afiliado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Afiliado")]
        [OpenApiRequestBody("application/json", typeof(Afiliado),
           Description = "Afiliado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Afiliado),
            Description = "Insertar la Afiliado")]
        public async Task<HttpResponseData> EliminarAfiliado([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "eliminarafiliado/{id}")] HttpRequestData req, int id)
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

        [OpenApiOperation("Listarspec", "seleccionarafiliado", Description = "Sirve para seleccionar un Afiliado")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Afiliado")]
        [OpenApiRequestBody("application/json", typeof(Afiliado),
           Description = "Afiliado modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Afiliado),
            Description = "Seleccionara un Afiliado")]
        public async Task<HttpResponseData> SeleccionarAfiliado([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "seleccionarafiliado/{id}")] HttpRequestData req,int id)
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
