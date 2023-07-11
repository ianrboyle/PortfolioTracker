
using Persistence.BLL;
using Persistence.DAL;
using Persistence.Logger;

namespace API.Extensions
{
  public static class ApplicationServiceExtenstions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {

      services.AddCors();
      services.AddHttpClient();
      services.AddScoped<IConnections, Connections>();
      services.AddScoped<IDataLogger, DataLogger>();
      services.AddScoped<Persistence.Logger.ILogger, Persistence.Logger.Logger>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IUserLogic, UserLogic>();
      services.AddScoped<IPositionLogic, PositionLogic>();
      services.AddScoped<IPositionRepository, PositionRepository>();
      // services.AddScoped<IUserRepository, UserRepository>();
      // services.AddScoped<IPositionService, PositionService>();
      // services.AddScoped<ISectorService, SectorService>();
      // services.AddScoped<IIndustryService, IndustryService>();
      // services.AddScoped<ISectorRepository, SectorRepository>();
      // services.AddScoped<IIndustryRepository, IndustryRepository>();
      // services.AddScoped<IPositionRepository, PositionRepository>();
      // services.AddScoped<IAlphaAdvantageClient, AlphaAdvantageClient>();
      // services.AddScoped<ISectorRepository, SectorRepository>();
      // services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      // services.Configure<AlphaAdvantageApiSettings>(config.GetSection("AlphaAdvantageApiSettings"));

      return services;

    }
  }
}