using Coling.API.Bolsatrabajo.Contratos.Repositorios;
using Coling.API.Bolsatrabajo.Implementacion.Repositorio;
using Coling.Utilitarios.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = new HostBuilder()
   
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IIdiomaRepositorio, IdiomaRepositorio>();
        services.AddScoped<IInstitucionRepositorio, InstitucionRepositorio>();
        services.AddScoped<IOfertaLaboralRepositorio,OfertaLaboralRepositorio>();
        services.AddScoped<ISolicitudRepositorio, SolicitudRepositorio>();
    }).ConfigureFunctionsWebApplication(x => {
        x.UseMiddleware<JwtMiddleware>();
    })
    .Build();

host.Run();
