
using Application.Services;
using Domain.Models.FinancialModelingPrep;
using Persistence.BLL;
using Persistence.DAL;
using StackExchange.Redis;

namespace API.Extensions
{
  public static class ApplicationServiceExtenstions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {

      services.AddCors();
      services.AddHttpClient();
      services.AddScoped<IConnections, Connections>();
      services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
      services.AddScoped<IDataLogger, DataLogger>();
      services.AddScoped<Persistence.Logger.ILogger, Persistence.Logger.Logger>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IUserLogic, UserLogic>();
      services.AddScoped<IPositionLogic, PositionLogic>();
      services.AddScoped<IPositionRepository, PositionRepository>();
      services.AddScoped<ISectorRepository, SectorRepository>();
      services.AddScoped<IIndustryRepository, IndustryRepository>();
      services.AddScoped<IRedisClient, RedisClient>();

      services.AddScoped<FinancialModelingPrepApiService>();

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.Configure<FinancialModelingPrepSettings>(config.GetSection("FinancialModelingPrepSettings"));

      return services;

    }
  }
}