using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TaxManagement.API.Authentication;
using TaxManagement.Core.ErrorHandler;
using TaxManagement.Core.Interfaces.Repository;
using TaxManagement.Repository;
using TaxManagement.Repository.DataAccess;
using TaxManagement.Repository.Interfaces;
using TaxManagement.Repository.Interfaces.DataAccess;

namespace TaxManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           var dbPath =  Environment.ContentRootPath.Replace("TaxManagement.API", "TaxManagement.Repository\\Database\\TaxDb.mdf");

            services.AddControllers();
            services.AddDbContext<TaxDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection").Replace("DbPath", dbPath)));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            var assembly = AppDomain.CurrentDomain.Load("TaxManagement.Business");
            services.AddMediatR(assembly);
            services.AddMediatR( assembly);
            services.AddScoped<ITaxDbContext, TaxDbContext>();
            services.AddScoped<IMunicipalitiesRepository, MunicipaltiesRepository>();
            services.AddScoped<IDailyTaxRepository, DailyTaxRepository>();
            services.AddScoped<IMonthlyTaxRepository, MonthlyTaxRepository>();
            services.AddScoped<IYearlyTaxRepository, YearlyTaxRepository>();
         
                         

            services.AddAuthentication("BasicAuthentication")
               .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaxManagement", Version = "v1" });
                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "Enter your Api Key below:",
                    Name = "api-key",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                      new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            },
                        },
                        new List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger/ui";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaxManagement(v1)");
            });
        }
    }
}
