using Coling.Autentificacion.Model;
using Coling.Repositorio.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Coling.Autentificacion
{
    public class AccountFunction
    {
        private readonly ILogger<AccountFunction> _logger;
        private readonly IUsuarioRepositorio usuarioRepositorio;

        public AccountFunction(ILogger<AccountFunction> logger,IUsuarioRepositorio usuarioRepositorio)
        {
            _logger = logger;
            this.usuarioRepositorio = usuarioRepositorio;
        }

        [Function("Login")]
        [OpenApiOperation("Listarspec", "ModificarExperienciaLaboral", Description = "Introduzca las credenciales")]
        [OpenApiRequestBody("application/json", typeof(Credenciales), Description = "Introduzca los datos de credenciales")]
        [OpenApiResponseWithBody(statusCode:HttpStatusCode.OK,contentType:"application/json",bodyType:typeof(ITokenData),Description ="El token es ")]

        public async Task<HttpResponseData> Login([HttpTrigger(AuthorizationLevel.Anonymous,  "post")] HttpRequestData req)
        {
           HttpResponseData? response = null;
            var login = await req.ReadFromJsonAsync<Credenciales>() ?? throw new ValidationException("Sus credenciales deben ser completas");
            var tokenFinal = await usuarioRepositorio.VerificarCredenciales(login.UserName, login.Password);
            if (tokenFinal!=null)
            {
                response = req.CreateResponse(HttpStatusCode.OK);
                 await response.WriteStringAsync(tokenFinal.Token);
                
            }
            else
            {
                response = req.CreateResponse(HttpStatusCode.Unauthorized);

            }
            return response;
        }
    }
}
