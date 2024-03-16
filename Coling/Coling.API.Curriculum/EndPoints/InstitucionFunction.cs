using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
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
using System.Xml;

namespace Coling.API.Curriculum.EndPoints
{
    public class InstitucionFunction
    {
        private readonly ILogger<InstitucionFunction> _logger;
        private readonly IInstitucionRepositorio repos;

        public InstitucionFunction(ILogger<InstitucionFunction> logger, IInstitucionRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }
        [Function("InsertarInstitucion")]
        public async Task<HttpResponseData> InsertarInstitucion([HttpTrigger(AuthorizationLevel.Function,"post")]HttpRequestData req)
        {
            HttpResponseData respuetsa;
            try
            {
                var registro = await req.ReadFromJsonAsync<Institucion>() ?? throw new Exception("Debe ingresar una institucion con todos sus datos");
                registro.RowKey=Guid.NewGuid().ToString();
                registro.Timestamp=DateTime.UtcNow;

                bool sw=await repos.Insertar(registro);
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
        [Function("ListarInstitucion")]
        public async Task<HttpResponseData> ListarInstitucion([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData respuetsa;
            try
            {
                var lista = repos.Listar();
                respuetsa= req.CreateResponse(HttpStatusCode.OK);
                await respuetsa.WriteAsJsonAsync(lista.Result);
                return respuetsa;
            }
            catch (Exception)
            {

                respuetsa = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuetsa;
            }
        }
        [Function("obtenerInstitucion")]
        public async Task<HttpResponseData> ObtenerInstitucion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "obtenerinstitucion/{id}")] HttpRequestData req,string id)
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

        [Function("ModificarInstitucion")]
        public async Task<HttpResponseData> ModificarInstitucion(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarinstitucion/{id}")] HttpRequestData req,
            string id)
        {
            HttpResponseData respuesta;
            try
            {
                var institucion = await repos.Obtener(id);
                if (institucion == null)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.NotFound);
                    return respuesta;
                }

                var d = await req.ReadFromJsonAsync<Institucion>() ?? throw new Exception("Debe ingresar los datos de la institución a modificar");

                institucion.Nombre = d.Nombre;
                institucion.Tipo = d.Tipo;
                institucion.Direccion = d.Direccion;
                institucion.Estado = d.Estado;
                institucion.Timestamp = DateTime.UtcNow;
                bool resultado = await repos.Actualizar(institucion);
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

        [Function("EliminarInstitucion")]
        public async Task<HttpResponseData> EliminarInstitucion(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "instituciones/{partitionKey}/{rowKey}")] HttpRequestData req,
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
