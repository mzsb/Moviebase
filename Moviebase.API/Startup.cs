#region Usings

using Moviebase.API.Extensions;
using Moviebase.DAL;

#endregion

namespace Moviebase.API;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSqliteDbContext<MoviebaseDbContext>(configuration.GetDefaultConnectionString());
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.Build();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());

        app.Build();
    }
}
