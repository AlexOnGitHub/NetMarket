using BusinessLogic.Data;
using Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build(); //Creaci�n de una instancia del host que va a ejecutar la aplicaci�n (Web Api).
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider; //Instancia del service provider que me permitira ejecutar la migraci�n e instanciar al dbcontext.
            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); //Creaci�n del loggerFactory para imprimir errores en caso de existir.

            try
            {
                var context = services.GetRequiredService<MarketDbContext>(); //Invocaci�n del context para poder ejecutar la migraci�n.
                await context.Database.MigrateAsync(); //Ejecuci�n de la migraci�n, al ser un await obliga a transformar el metodo en async task.
                await MarketDbContextData.CargarDataAsync(context, loggerFactory); //Llamada al metodo que insertara datos solo si la tabla esta vacia

                var userManager = services.GetRequiredService<UserManager<Usuario>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var identityContext = services.GetRequiredService<SeguridadDbContext>();
                await identityContext.Database.MigrateAsync();
                await SeguridadDbContextData.SeedUserAsync(userManager, roleManager);
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(e, "Errores en el proceso de migraci�n.");
            }
        }

        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
}