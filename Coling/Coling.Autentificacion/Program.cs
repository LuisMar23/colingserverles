using Coling.Repositorio.Contratos;
using Coling.Repositorio.Implementacion;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Coling.Utilitarios.Middlewares;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IUsuarioRepositorio,UsuarioRepositorio>();
 
    })
    //.ConfigureFunctionsWebApplication(x => {
    //    x.UseMiddleware<JwtMiddleware>();
    //})
    .Build();

host.Run();
