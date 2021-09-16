using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RemitaBillAndBulkPayments.Constants;
using RemitaBillAndBulkPayments.Models.EFContext;
using RemitaBillAndBulkPayments.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //add the connection string
            services.AddDbContext<DatabaseContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection"), options => options.EnableRetryOnFailure()));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RemitaBillAndBulkPayments", Version = "v1" });
            });

            //add the DI
            services.AddScoped<IUtil, Util>();
            services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
            services.AddSingleton<IRemitaProxy, RemitaProxy>();
            services.AddScoped<IRemitaService, RemitaService>();
            services.Configure<RemitaConstants>(Configuration.GetSection("RemitaContstants"));
            services.Configure<QuestConstants>(Configuration.GetSection("QuestConstants"));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RemitaBillAndBulkPayments v1"));
            }

            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
