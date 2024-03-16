using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.EndPoints
{
    public class GradoFunction
    {
        private readonly ILogger<GradoFunction> _logger;
        private readonly IGradoRepositorio repos;

        public GradoFunction(ILogger<GradoFunction> logger, IGradoRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }
        [Function("InsertarGrado")]
        public async Task<HttpResponseData> InsertarGrado([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData respuetsa;
            try
            {
                var registro = await req.ReadFromJsonAsync<Grado>() ?? throw new Exception("Debe ingresar una Grado con todos sus datos");
                registro.RowKey = Guid.NewGuid().ToString();
                registro.Timestamp = DateTime.UtcNow;

                bool sw = await repos.Insertar(registro);
                if (sw)
                {
                    respuetsa = req.CreateResponse(HttpStatusCode.OK);
                    return respuetsa;
                }
                else
                {
                    respuetsa = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuetsa;
                }
            }
            catch (Exception)
            {

                respuetsa = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuetsa;
            }
        }
        [Function("ListarGrado")]
        public async Task<HttpResponseData> ListarGrado([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData respuetsa;
            try
            {
                var lista = repos.Listar();
                respuetsa = req.CreateResponse(HttpStatusCode.OK);
                await respuetsa.WriteAsJsonAsync(lista.Result);
                return respuetsa;
            }
            catch (Exception)
            {

                respuetsa = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuetsa;
            }
        }
        [Function("obtenerGrado")]
        public async Task<HttpResponseData> ObtenerGrado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerGrado/{id}")] HttpRequestData req, string id)
        {
            HttpResponseData respuetsa;
            try
            {
                var lista = repos.Obtener(id);
                respuetsa = req.CreateResponse(HttpStatusCode.OK);
                await respuetsa.WriteAsJsonAsync(lista.Result);
                return respuetsa;
            }
            catch (Exception)
            {

                respuetsa = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuetsa;
            }
        }

        [Function("ModificarGrado")]
        public async Task<HttpResponseData> ModificarGrado(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarGrado/{id}")] HttpRequestData req,
            string id)
        {
            HttpResponseData respuesta;
            try
            {
                var Grado = await repos.Obtener(id);
                if (Grado == null)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    return respuesta;
                }

                var d = await req.ReadFromJsonAsync<Grado>() ?? throw new Exception("Debe ingresar los datos del grado a modificar");

                Grado.PartitionKey = d.PartitionKey;
                Grado.nombregrado = d.nombregrado;
                Grado.estado = d.estado;
                Grado.Timestamp = DateTime.UtcNow;
                bool resultado = await repos.Actualizar(Grado);
                if (resultado)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }

        [Function("EliminarGrado")]
        public async Task<HttpResponseData> EliminarGrado(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Grado/{partitionKey}/{rowKey}")] HttpRequestData req,
            string partitionKey,
            string rowKey)
        {
            HttpResponseData respuesta;
            try
            {
                bool resultado = await repos.Eliminar(partitionKey, rowKey);
                if (resultado)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return respuesta;
        }
    }
}
