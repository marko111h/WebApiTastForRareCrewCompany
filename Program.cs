using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace WebApiTastForRareCrewCompany
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services);
            var app = builder.Build();
            Configure(app);

            app.Run();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAuthorization();
            services.AddHttpClient("ReareCrewApi", client =>
            {
                string kljuc = "vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";
                client.BaseAddress = new Uri("https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=" + kljuc);
            });
        }
        private static void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
           //     app.UseSwagger();
          //      app.UseSwaggerUI();
                app.UseHttpsRedirection();

                app.UseAuthorization();
              


            }
            app.MapControllers();
        }
    }
}