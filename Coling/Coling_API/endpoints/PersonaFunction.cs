using Azure;
using Coling.API.Afiliados.Contratos;
using Coling.API.Afiliados.Implementacion;
using Coling.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.endpoints
{
    public class PersonaFunction
    {
        private readonly ILogger<PersonaFunction> logger;
        private readonly IPersonaLogic personaLogic;

        public PersonaFunction(ILogger<PersonaFunction> logger,IPersonaLogic personaLogic)
        {
            this.logger = logger;
            this.personaLogic = personaLogic;
        }
        [Function("listarpersonas")]

        [OpenApiOperation("Listarspec", "listarpersonas", Description = "Sirve para listar las Personas ")]
        [OpenApiRequestBody("application/json", typeof(Persona),
           Description = "Persona modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Persona),
            Description = "listara  las Personas ")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route= "listarpersonas")]HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listapersonas=personaLogic.ListarPersonaTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listapersonas.Result);
            return respuesta;
        }
        [Function("Insertarpersona")]
 
        [OpenApiOperation("Listarspec", "Insertarpersona", Description = "Sirve para insertar un Persona ")]
        [OpenApiRequestBody("application/json", typeof(Persona),
           Description = "Persona modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Persona),
            Description = "insertara un Persona ")]
        public async Task<HttpResponseData> InsertarPersona([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertarpersona")] HttpRequestData req)
        {
            logger.LogInformation("ejecutando para insertar personas");
            try
            {
                var per = await req.ReadFromJsonAsync<Persona>() ?? throw new Exception("Debe ingresar un apersona con todos sus datos");
                bool r=await personaLogic.InsertarPersona(per);
                if (r)
                {
                    var respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex) { 
                var error=req.CreateResponse(HttpStatusCode.BadRequest);
                await error.WriteAsJsonAsync(ex.Message);
                return error;
            }
        
        }
    

        [Function("modificarpersona")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Persona")]
        [OpenApiOperation("Listarspec", "modificarpersona", Description = "Sirve para modificar un Persona ")]
        [OpenApiRequestBody("application/json", typeof(Persona),
           Description = "Persona modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Persona),
            Description = "modificara una Persona ")]
        public async Task<HttpResponseData> ModificarPersona([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "modificarpersona/{id}")] HttpRequestData req,int id)
        {
            try
            {
                var persona = await req.ReadFromJsonAsync<Persona>() ?? throw new Exception("Debe proporcionar los datos de la persona a modificar");
                bool m = await personaLogic.ModificarPersona(persona, id);
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
        
      
        [Function("eliminarpersona")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Persona")]
        [OpenApiOperation("Listarspec", "eliminarpersona", Description = "Sirve para eliminar un Persona ")]
        [OpenApiRequestBody("application/json", typeof(Persona),
           Description = "Persona modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Persona),
            Description = "eliminara un Persona ")]
        public async Task<HttpResponseData> EliminarPersona( [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "eliminarpersona/{id}")] HttpRequestData req,int id)
        {
            try
            {
                bool eliminado = await personaLogic.EliminarPersona(id);
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

        [Function("seleccionarpersona")]
        [OpenApiParameter("id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "ID del Persona")]
        [OpenApiOperation("Listarspec", "seleccionarPersona", Description = "Sirve para seleccionar un Persona ")]
        [OpenApiRequestBody("application/json", typeof(Persona),
           Description = "Persona modelo")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(Persona),
            Description = "seleccionara un Persona ")]
        public async Task<HttpResponseData> SeleccionarPersona([HttpTrigger(AuthorizationLevel.Function, "get", Route = "seleccionarpersona/{id}")] HttpRequestData req,int id)
        {

            logger.LogInformation("Ejecutando para seleccionar una persona");

            try
            {
             
                var persona = await personaLogic.ObtenerPersona(id);
                if (persona == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }

                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(persona);
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
