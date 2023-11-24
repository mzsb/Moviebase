#region Usings

using Microsoft.Extensions.Configuration;
using Moviebase.API.Extensions;

#endregion

namespace Moviebase.API;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationServices(configuration);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddCors();
        services.AddSwaggerGen();
        services.AddIdentityServices(configuration);
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
        app.UseCors(policy => policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("https://localhost:4200"));
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());

        app.Build();
    }
}
