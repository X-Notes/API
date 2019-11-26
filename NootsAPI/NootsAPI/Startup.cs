using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Firebase.Authentication.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Noots.BusinessLogic.Interfaces;
using Noots.BusinessLogic.Services;
using Noots.DataAccess.InterfacesRepositories;
using Noots.DataAccess.Repositories;
using AutoMapper;
using Noots.Domain.MappingProfiles;

namespace NootsAPI
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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .WithOrigins("http://localhost:4200");
            }));

            var connection = Configuration["Mongo:client"];
            var database = Configuration["Mongo:database"];


            services.AddControllers();

            //Mapper
            services.AddAutoMapper(typeof(UserProfile).Assembly);

            //Authentification
            //services.AddFirebaseAuthentication(Configuration["FirebaseOptions:Issuer"], Configuration["FirebaseOptions:Audience"]);
            services.AddSiteAuthentications(Configuration);

            services.ElasticService(Configuration);
            services.BusinessServices(Configuration);
            services.DatabaseServices(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("MyPolicy");


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
