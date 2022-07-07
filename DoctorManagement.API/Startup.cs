using DoctorManagement.Domain.Aggregates.DoctorAggregate;
using DoctorManagement.Domain.Interfaces;
using DoctorManagement.Infrastructure.Data.Contexts;
using DoctorManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorManagement.API
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddControllers();
            services.AddDbContext<DoctorManagementContext>(setup => setup.UseSqlServer(connectionString));
            services.AddScoped<IRepository<Consultant>, Repository<Consultant>>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Doctor Management API",
                    Description = "this Api Microservices is allows endpoints for doctor consultant table for post operations",

                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(options =>
            {
                options.AllowAnyOrigin();   //accept request from all client
                options.AllowAnyMethod();   //support all http operations like get, post, put, delete
                options.AllowAnyHeader();   //support all http headers like content-type, accept, etc
            });
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "Doctor Management API"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
