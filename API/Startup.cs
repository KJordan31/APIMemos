using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Filters;
using Aplicacion.Validadores;
using FluentValidation.AspNetCore;
using Infraestructura;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API
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
            services.AddCors();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddInfraestructure();
            services
                .AddControllers(options =>
                {
                    options.Filters.Add<ValidationFilter>();
                })
                .AddFluentValidation(config =>
                    config
                        .RegisterValidatorsFromAssemblyContaining
                        <AccionDTOValidator>());

            services
                .AddSwaggerGen(c =>
                {
                    c
                        .SwaggerDoc("v1",
                        new OpenApiInfo { Title = "API", Version = "v1" });
                });
            services.AddOptions();

            services
                .AddAuthentication()
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Unathorized/";
                    options.AccessDeniedPath = "/Account/Forbidden";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseCors(options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app
                    .UseSwaggerUI(c =>
                        c
                            .SwaggerEndpoint("/swagger/v1/swagger.json",
                            "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

        }
    }
}
