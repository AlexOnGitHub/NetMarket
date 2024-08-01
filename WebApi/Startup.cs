using BusinessLogic.Data;
using BusinessLogic.Logic;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using WebApi.Dtos;
using WebApi.Middleware;

namespace WebApi;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IOrdenCompraService, OrdenCompraService>();

        var builder = services.AddIdentityCore<Usuario>();

        builder = new IdentityBuilder(builder.UserType, builder.Services);
        builder.AddRoles<IdentityRole>();
        builder.AddEntityFrameworkStores<SeguridadDbContext>();
        builder.AddSignInManager<SignInManager<Usuario>>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                ValidIssuer = Configuration["Token:Issuer"],
                ValidateIssuer = true,
                ValidateAudience = false //Puede filtrar los clientes que accedan a la aplicación, con esto hacemos que cualquier cliente pueda consumir los end points
            };
        });

        services.AddAutoMapper(typeof(MappingProfiles));

        services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>))); //Aplicación de Repository Generico que ocasióna que cuando arranque el programa "Web Api" se genere un objeto de tipo IGenericRepository cada vez que se envie un request por el cliente 

        services.AddScoped(typeof(IGenericSeguridadRepository<>), (typeof(GenericSeguridadRepository<>)));

        //Se da de alta la cadena de conexión y se inicia el servicio a SQL Server.
        services.AddDbContext<MarketDbContext>(opt =>
        {
            opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddDbContext<SeguridadDbContext>(opt => {
            opt.UseSqlServer(Configuration.GetConnectionString("IdentitySeguridad"));
        });

        //Al agregar "AddSingleton" quiere decir que va a generar solo una instancia de objeto conexión para el Redis.
        services.AddSingleton<IConnectionMultiplexer>(opt =>
        {
            var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
            return ConnectionMultiplexer.Connect(configuration);
        });

        services.TryAddSingleton<ISystemClock, SystemClock>();

        services.AddTransient<IProductoRepository, ProductoRepository>();
        services.AddControllers();

        services.AddScoped<ICarritoCompraRepository, CarritoCompraRepository>();

        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsRule", rule =>
            {
                rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
            });      
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //if(env.IsDevelopment())
        //{
        //    app.UseDeveloperExceptionPage();
        //}

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseStatusCodePagesWithReExecute("/errors", "?code={0}");

        app.UseRouting();
        app.UseCors("CorsRule");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

