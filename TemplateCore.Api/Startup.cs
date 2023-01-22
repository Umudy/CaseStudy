using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TemplateCore.Api.Infrastructure;

using TemplateCore.Api.Infrastructure.Filters;
using TemplateCore.Business.Abstract;
using TemplateCore.Business.Concrete;
using TemplateCore.DataAccess.Abstract;
using TemplateCore.DataAccess.Concrete;
using TemplateCore.DataAccess.Concrete.Contexts;
using TemplateCore.Domain.AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace TemplateCore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = false,
            //            ValidateAudience = false,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ClockSkew = TimeSpan.Zero,
            //            ValidIssuer = Configuration["Auth:Jwt:Issuer"],
            //            ValidAudience = Configuration["Auth:Jwt:Issuer"],
            //            IssuerSigningKey =
            //                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]))
            //        };
            //    });

            services.AddDbContext<TemplateCoreContext>(options =>
            {
                //options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"));
                //options.EnableSensitiveDataLogging(true);
                options.UseOracle(Configuration.GetConnectionString("OracleConnection"), builder => builder.UseOracleSQLCompatibility("11"));

            });



            services.AddScoped<IBasket, BasketBusiness>();
            services.AddScoped<ICustomer, CustomerBusiness>();
            services.AddScoped<IOrderBusiness, OrderBusiness>();
            services.AddScoped<IOrderDal, OrderDal>();


            services.AddHttpContextAccessor();
            //services.AddScoped<IPrincipal>(provider =>
            //    provider.GetRequiredService<IHttpContextAccessor>().HttpContext.User);

            services.AddCors();

            services.AddMvc();


            services.AddControllers(options => { options.Filters.Add<HttpGlobalExceptionFilter>(); })
                .AddFluentValidation(fv =>
                {
                    fv.AutomaticValidationEnabled = true;

                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = c =>
                    {
                        var errors = c.ModelState.Values.Where(v => v.Errors.Count > 0)
                            .SelectMany(v => v.Errors)
                            .Select(v => v.ErrorMessage);

                        return new BadRequestObjectResult(new JsonErrorResponse
                        {
                            Errors = errors.ToArray(),
                        });
                    };
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddAutoMapper(config =>
            {
                config.AddProfile<ModelToDtoProfile>();
                config.AddProfile<DtoToModelProfile>();
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = " API",
                    Description = "TemplateCore Uygulamasi Api Servisleri",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //dbContext.Database.EnsureCreated();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); 
            }

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TemplateCore");
            });

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            var supportedCultures = new[]
            {
                new CultureInfo("tr-TR"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("tr-TR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseWelcomePage();
        }
    }
}
