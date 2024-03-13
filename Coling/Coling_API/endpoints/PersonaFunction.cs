using Azure;
using Coling.API.Afiliados.Contratos;
using Coling.API.Afiliados.Implementacion;
using Coling.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        [Function("PersonaFunction")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route= "listarpersonas")]HttpRequestData req)
        {
            logger.LogInformation("ejecuatnado");
            var listapersonas=personaLogic.ListarPersonaTodos();
            var respuesta = req.CreateResponse(HttpStatusCode.OK);
            await respuesta.WriteAsJsonAsync(listapersonas.Result);
            return respuesta;
        }
        [Function("Insertarpersona")]
        public async Task<HttpResponseData> InsertarPersona([HttpTrigger(AuthorizationLevel.Function, "post", Route = "insertarpersona")] HttpRequestData req)
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
        public async Task<HttpResponseData> ModificarPersona([HttpTrigger(AuthorizationLevel.Function, "put", Route = "modificarpersona/{id}")] HttpRequestData req,int id)
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
