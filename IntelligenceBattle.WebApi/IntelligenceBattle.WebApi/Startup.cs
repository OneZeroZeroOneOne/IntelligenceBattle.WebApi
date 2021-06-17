using IntelligenceBattle.WebApi.Dal.Contexts;
using IntelligenceBattle.WebApi.Security.Configs;
using IntelligenceBattle.WebApi.Security.Handlers;
using IntelligenceBattle.WebApi.Security.Services;
using IntelligenceBattle.WebApi.Utilities.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.OpenApi.Models;
using AutoMapper;
using IntelligenceBattle.WebApi.Bll.Services;
using IntelligenceBattle.WebApi.Dal;

namespace IntelligenceBattle.WebApi
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
            services.AddControllers();
            Console.WriteLine(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("databaseconnectionstring")));
            services.AddScoped(x =>
            {
                return new PublicContext(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("databaseconnectionstring")));
                //return new PublicContext(Environment.GetEnvironmentVariable("databaseconnectionstring"));
            });
            services.AddSingleton(x => new AuthOptions 
            { 
                Audience = Environment.GetEnvironmentVariable("AUDIENCE"),
                Issuer = Environment.GetEnvironmentVariable("ISSUER"),
                Key = Environment.GetEnvironmentVariable("KEY"),
                Lifetime = 60,
                SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("KEY")))
            });
            services.AddSingleton<JwtTokenService>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            services.AddScoped<GameService>();
            services.AddScoped<UserService>();
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAuthentication("RootProvider").AddScheme<AuthOptions, CustomAuthHandler>("RootProvider", x => new AuthOptions
            {
                Audience = Environment.GetEnvironmentVariable("AUDIENCE"),
                Issuer = Environment.GetEnvironmentVariable("ISSUER"),
                Key = Environment.GetEnvironmentVariable("KEY"),
                Lifetime = 60,
                SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("KEY")))
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IntelligenceBattle.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IntelligenceBattle.WebApi v1"));
            }
            else
            {
                app.UseSwagger(c => c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    var basePath = "/api";
                    swaggerDoc.Servers = new List<OpenApiServer>
                    {
                        new OpenApiServer {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{basePath}"},
                    };
                }));
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "IntelligenceBattle.WebApi v1"));
            }

            app.UseRouting();

            app.ConfigureCustomExceptionMiddleware();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
