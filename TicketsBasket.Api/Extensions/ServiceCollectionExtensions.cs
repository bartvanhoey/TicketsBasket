using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using TicketsBasket.Infrastructure.Options;
using TicketsBasket.Models.Data;
using TicketsBasket.Repositories;
using TicketsBasket.Services;

namespace TicketsBasket.Api.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddB2cAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
      // services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      //      .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAdB2C"));

      services.AddMicrosoftIdentityWebApiAuthentication(configuration, "AzureAdB2C");
    }

    public static void AddTicketsBasketDbContext(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<TicketsBasketDbContext>(options =>
        {
          options.UseSqlServer(configuration.GetConnectionString("Default"), sqlOptions =>
          {
            sqlOptions.MigrationsAssembly("TicketsBasket.Api");
          });
        });
    }

    public static void AddCorsPolicy(this IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy", policy =>
        {
          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
      });
    }

    public static void AddSwagger(this IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
          {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketsBasket.Api", Version = "v1" });
          });
    }

    public static void AddUnitOfWork(this IServiceCollection services)
    {
      services.AddScoped<IUnitOfWork, EfUnitOfWork>();
    }

    public static void AddIdentityOptions(this IServiceCollection services)
    {
      services.AddScoped<IdentityOptions>(sp =>
      {
        var httpContext = sp.GetService<IHttpContextAccessor>().HttpContext;
        var identityOptions = new IdentityOptions();
        if (httpContext.User.Identity.IsAuthenticated)
        {
          identityOptions.User = httpContext.User;
        }
        return identityOptions;
      });
    }

    public static void AddBusinessServices(this IServiceCollection services)
    {
      services.AddScoped<IUserProfilesService, UserProfilesService>();

    }

    public static void AddAzureStorageAccountOptions(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddScoped(sp =>configuration.GetSection("AzureStorageAccountSettings").Get<AzureStorageAccountOptions>());
    }
  }
}